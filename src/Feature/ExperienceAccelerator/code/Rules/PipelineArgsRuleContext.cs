using Sitecore;
using Sitecore.Diagnostics;
using Sitecore.Pipelines;
using Sitecore.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MasterToWeb.Feature.XA.Extensions.Rules
{
    public class PipelineArgsRuleContext<TArgs> : RuleContext, IPipelineArgsRuleContext<TArgs>
        where TArgs : PipelineArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PipelineArgsRuleContext{TArgs}"/> class.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        public PipelineArgsRuleContext([NotNull] TArgs args)
        {
            Assert.IsNotNull(args, "args");

            Args = args;
            Item = args.ProcessorItem.InnerItem;
        }

        /// <summary>
        /// Gets the args.
        /// </summary>
        [NotNull]
        public TArgs Args { get; }
    }
}