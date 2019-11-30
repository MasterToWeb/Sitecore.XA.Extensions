using Sitecore;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Rules;
using Sitecore.SecurityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MasterToWeb.Feature.XA.Extensions.Rules
{
    public static class RuleManager
    {
        /// <summary>
        /// The run rules.
        /// </summary>
        /// <param name="ruleContext">The rule context.</param>
        /// <param name="rulesFolder">The rules folder.</param>
        public static void RunRules<TRuleContext>([NotNull] TRuleContext ruleContext, [NotNull] ID rulesFolder)
                where TRuleContext : RuleContext
        {
            Assert.ArgumentNotNull(ruleContext, "ruleContext != null");
            Assert.ArgumentNotNull(rulesFolder, "rulesFolder");

            try
            {
                if (!Settings.Rules.ItemEventHandlers.RulesSupported(ruleContext.Item.Database))
                {
                    return;
                }

                Item rulesFolderItem;
                using (new SecurityDisabler())
                {
                    rulesFolderItem = ruleContext.Item.Database.GetItem(rulesFolder);
                    if (rulesFolderItem == null)
                    {
                        return;
                    }
                }

                var rules = RuleFactory.GetRules<TRuleContext>(rulesFolderItem, "Rule");
                if (rules == null || rules.Count == 0)
                {
                    return;
                }

                rules.Run(ruleContext);
            }
            catch (Exception exception)
            {
                Log.Error(exception.Message, exception, typeof(RuleManager));
            }
        }
    }
}