using Sitecore;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Rules;
using Sitecore.Rules.Actions;
using Sitecore.SecurityModel;
using System.Runtime.InteropServices;

namespace MasterToWeb.Feature.XA.Extensions.Rules.Actions
{
    [UsedImplicitly]
    [Guid("73594343-DC04-4DC8-8B59-C66725AD86B1")]
    public sealed class AssignAndStartWorkflow<TRuleContext> : RuleAction<TRuleContext> where TRuleContext : RuleContext
    {
        public string workflowid { get; set; }
        public override void Apply(TRuleContext ruleContext)
        {
            if (ruleContext.Item == null) return;
            if (!ID.IsID(workflowid)) return;

            using (new SecurityDisabler())
            {
                using (new EditContext(ruleContext.Item))
                {                   
                    var workflow = Factory.GetDatabase("master")?.WorkflowProvider.GetWorkflow(workflowid);
                    if (workflow != null)
                    {
                        //ruleContext.Item[Sitecore.FieldIDs.Workflow] = new ID(workflowid).ToString();
                        workflow.Start(ruleContext.Item);
                    }
                }
            }
        }
    }
}