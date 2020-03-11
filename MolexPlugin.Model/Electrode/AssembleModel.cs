using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NXOpen;
using Basic;

namespace MolexPlugin.Model.Electrode
{
    public class AssembleModel
    {
        public ASMModel ASM { get; private set; }

        public List<WorkAssembleModel> WorkAssembles { get; private set; }

        public AssembleModel(Part part)
        {
            ASMModel aSM = new ASMModel();
            aSM.GetModelForPart(part);
            this.ASM = aSM;
           
        }
        public void Clear()
        {
            this.WorkAssembles.Clear();
        }

        /// <summary>
        /// 添加Work
        /// </summary>
        /// <param name="part"></param>
        /// <returns></returns>
        public bool AddWork(Part part)
        {
            string name = this.ASM.MoldInfo.MoldNumber + "-" + this.ASM.MoldInfo.WorkpieceNumber;
            if (part.Name.Substring(0, name.Length).Equals(name))
            {
                string type = AttributeUtils.GetAttrForString(part, "PartType");
                if (!this.WorkAssembles.Exists(x => x.Work.AssembleName == part.Name) && type == "Work")
                {
                    WorkAssembleModel model = new WorkAssembleModel(part);
                    this.WorkAssembles.Add(model);
                    return true;
                }
            }
            return false;
        }
        public void Initialization(Part part)
        {
            NXOpen.Assemblies.Component[] comp = part.ComponentAssembly.RootComponent.GetChildren();
            foreach(NXOpen.Assemblies.Component ct in comp)
            {

            }
        }



    }
}
