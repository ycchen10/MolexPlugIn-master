using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NXOpen;
using Basic;

namespace MolexPlugin.Model.Electrode
{
    public class WorkAssembleModel
    {
        public WorkModel Work { get; private set; }

        public EDMAssembleModel EDMAssemble { get; private set; }

        public List<ElectrodeModel> Electrodes { get; private set; } = new List<ElectrodeModel>();

        public WorkAssembleModel(Part part)
        {
            WorkModel work = new WorkModel();
            work.GetModelForPart(part);
            this.Work = work;
            this.Initialization(part);
        }
        public void Clear()
        {

            this.Electrodes.Clear();
        }

        /// <summary>
        /// 添加电极
        /// </summary>
        /// <param name="part"></param>
        /// <returns></returns>
        public bool AddElectrode(Part part)
        {
            string type = AttributeUtils.GetAttrForString(part, "PartType");
            if (!this.Electrodes.Exists(x => x.AssembleName == part.Name) && type == "Electrode")
            {
                ElectrodeModel ele = new ElectrodeModel();
                ele.GetModelForPart(part);
                this.Electrodes.Add(ele);
                return true;
            }
            return false;
        }

        public void Initialization(Part part)
        {
            NXOpen.Assemblies.Component[] comp = part.ComponentAssembly.RootComponent.GetChildren();
            foreach (NXOpen.Assemblies.Component ct in comp)
            {
                Part compPart = ct.Prototype as Part;
                string type = AttributeUtils.GetAttrForString(compPart, "PartType");
                if (type == "EDM")
                {
                    EDMAssembleModel model = new EDMAssembleModel(compPart);
                }
                if (type == "Electrode")
                {
                    ElectrodeModel electrode = new ElectrodeModel();
                    electrode.GetModelForPart(compPart);
                    this.Electrodes.Add(electrode);
                }
            }
        }

    }
}
