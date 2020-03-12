using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NXOpen;
using Basic;
using MolexPlugin.Model;


namespace MolexPlugin.DAL
{
    public class AddWorkBuilder
    {
        public AssembleModel Model { get; private set; }
        private Part asmPart;
        private AssembleSingleton singleton;
        public AddWorkBuilder(Part asmPart)
        {
            this.asmPart = asmPart;
            singleton = AssembleSingleton.Instance();
            this.Model = singleton.GetAssemble(asmPart);
        }
        /// <summary>
        /// 创建特征
        /// </summary>
        /// <param name="mat"></param>
        /// <param name="workNumber"></param>
        public void CreateBuilder(Matrix4 mat, int workNumber)
        {

            WorkModel work = new WorkModel(this.Model.Asm.WorkpieceDirectoryPath, this.Model.Asm.MoldInfo, workNumber, mat);
            work.CreatePart();
            work.Load(asmPart);
            singleton.AddWork(work);
        }

        /// <summary>
        /// 修改矩阵
        /// </summary>
        /// <param name="mat"></param>
        /// <param name="workNumber"></param>
        /// <returns></returns>
        public bool AlterMatr(Matrix4 mat, int workNumber)
        {
            WorkModel work = this.Model.Works.Find(x => x.WorkNumber == workNumber);
            if (work != null)
            {
                NXOpen.Assemblies.Component workComp = work.PartTag.OwningComponent.Parent;
                PartUtils.SetPartWork(workComp);
                work.AlterMatr(mat);
                CartesianCoordinateSystem csys = work.PartTag.WCS.Save();
                csys.Name = "WORK" + workNumber.ToString();
                csys.Color = 186;
                csys.Layer = 200;
                return true;
            }
            return false;

        }
        /// <summary>
        /// 获取最大电极
        /// </summary>
        /// <returns></returns>
        public int GetMaxWorkNumber()
        {
            this.Model.Electrodes.Sort();
            return this.Model.Electrodes[this.Model.Electrodes.Count - 1].EleInfo.EleNumber;
        }

    }
}
