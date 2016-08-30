using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoWTBGapp.DataAccess.Abstractions;
using WoWTBGapp.DataObjects;

namespace WoWTBGapp.DataAccess.Nodejs
{
    public class RequirementImageDataAccess : BaseAccess<RequirementImageData>, IRequirementImageData
    {
        public override string BaseAPIRouting => "api/requierementimagedata";

        public RequirementImageDataAccess()
        {

        }
    }
}
