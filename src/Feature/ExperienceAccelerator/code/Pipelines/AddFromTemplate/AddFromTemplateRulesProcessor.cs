using System;
using MasterToWeb.Feature.XA.Extensions.Rules;
using Sitecore;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.Pipelines.ItemProvider.AddFromTemplate;

namespace MasterToWeb.Feature.XA.Extensions.Pipelines.AddFromTemplate
{
    public class AddFromTemplateRulesProcessor : AddFromTemplateProcessor
    {
        #region Public Properties
        [CanBeNull]
        [UsedImplicitly]
        public string RuleFolderId { get; set; }
        #endregion

        #region Public Methods and Operators
        public override void Process([NotNull] AddFromTemplateArgs args)
        {
            if (args.Aborted)
            {
                return;
            }

            Assert.IsNotNull(args.FallbackProvider, "FallbackProvider is null");

            try
            {
                var item = args.FallbackProvider.AddFromTemplate(args.ItemName, args.TemplateId, args.Destination, args.NewId);
                if (item == null)
                {
                    return;
                }

                args.ProcessorItem = args.Result = item;
            }
            catch (Exception ex)
            {
                Log.Error("AddFromTemplateRulesProcessor failed. Removing partially created item if it exists.", ex, this);

                var item = args.Destination.Database.GetItem(args.NewId);
                item?.Delete();

                throw;
            }

            ID id;
            if (string.IsNullOrWhiteSpace(RuleFolderId)
                || !Settings.Rules.ItemEventHandlers.RulesSupported(args.Destination.Database)
                || !ID.TryParse(RuleFolderId, out id))
            {
                return;
            }

            var ruleContext = new PipelineArgsRuleContext<AddFromTemplateArgs>(args);
            RuleManager.RunRules(ruleContext, id);


        }
        #endregion
    }
}