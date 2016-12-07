using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xBountyHunter.DependencyServices
{
    public interface ICamera
    {
        Task<string> takePhoto();
    }
}
