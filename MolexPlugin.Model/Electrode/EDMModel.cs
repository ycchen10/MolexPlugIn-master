﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NXOpen;
using Basic;
using NXOpen.Assemblies;

namespace MolexPlugin.Model
{
    /// <summary>
    /// EDM
    /// </summary>
    public class EDMModel : AbstractModel
    {
        public EDMModel()
        {

        }
        public EDMModel(string filePath, MoldInfoModel moldInfo)
        {
            this.MoldInfo = moldInfo;
            this.PartType = "EDM";
            GetAssembleName();
            this.WorkpieceDirectoryPath = filePath;
            this.WorkpiecePath = filePath + this.AssembleName + ".prt";
        }
        public override void CreatePart()
        {
            Part part = PartUtils.NewFile(this.WorkpiecePath) as Part;
            this.PartTag = part;
            SetAttribute();
        }

        public override void GetAssembleName()
        {
            this.AssembleName = this.MoldInfo.MoldNumber + "-" + this.MoldInfo.WorkpieceNumber + "-EDM";
        }

        public override void GetModelForPart(Part part)
        {
            this.PartTag = part;
            this.GetAttribute(part);
            this.AssembleName = part.Name;
            this.WorkpiecePath = part.FullPath;
            this.WorkpieceDirectoryPath = Path.GetDirectoryName(WorkpiecePath) + "\\";
            this.AssembleName = Path.GetFileNameWithoutExtension(this.WorkpiecePath);
        }

        public override Component Load(Part parentPart)
        {
            Matrix4 matr = new Matrix4();
            matr.Identity();
            return Basic.AssmbliesUtils.PartLoad(parentPart, this.WorkpiecePath, this.AssembleName, matr, new Point3d(0, 0, 0));
        }
    }
}
