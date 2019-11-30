using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore;
using Sitecore.Data.Items;

namespace MasterToWeb.Feature.XA.Extensions.Rules
{
    /// <summary>
    /// The PipelineArgsRuleContext interface.
    /// </summary>
    /// <remarks>
    /// Credit to Jim "Jimbo" Baltika for developing this class
    /// </remarks>
    /// <typeparam name="TArgs">The type of the arguments.</typeparam>
    public interface IPipelineArgsRuleContext<out TArgs>
        where TArgs : class
    {
        /// <summary>
        /// Gets the args.
        /// </summary>
        [NotNull]
        TArgs Args { get; }

        /// <summary>
        /// Gets the processor item.
        /// </summary>
        [NotNull]
        Item Item { get; }
    }
}
