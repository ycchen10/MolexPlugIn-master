using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NXOpen;

namespace MolexPlugin.Model.Electrode
{
    public class EDMAssembleModel
    {
        public EDMModel EDM { get; private set; }

        public List<Part> Workpieces { get; private set; }

        public EDMAssembleModel(Part part)
        {
            EDMModel eDM = new EDMModel();
            eDM.GetModelForPart(part);
            this.EDM = eDM;
            this.Initialization(part);
        }
        /// <summary>
        /// 添加工件
        /// </summary>
        /// <param name="part"></param>
        /// <returns></returns>
        public bool AddWorkpiece(Part part)
        {
            NXOpen.Assemblies.Component comp = part.OwningComponent;
            Part parent = comp.Parent.Prototype as Part;
            if (parent.Tag == this.EDM.PartTag.Tag)
            {
                if (!this.Workpieces.Exists(x => x.Name == part.Name))
                {
                    this.Workpieces.Add(part);
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialization(Part part)
        {
            NXOpen.Assemblies.Component[] comp = part.ComponentAssembly.RootComponent.GetChildren();
            foreach (NXOpen.Assemblies.Component ct in comp)
            {
                this.Workpieces.Add(ct.Prototype as Part);
            }
        }
    }
}
