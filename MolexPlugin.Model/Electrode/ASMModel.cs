﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NXOpen;
using Basic;
using NXOpen.Assemblies;

namespace MolexPlugin.Model.Electrode
{
    /// <summary>
    /// EDM
    /// </summary>
    public class ASMModel : AbstractModel
    {
        public ASMModel()
        {

        }
        public ASMModel(MoldInfoModel moldInfo)
        {
            this.MoldInfo = moldInfo;
            this.PartType = "ASM";
        }
        public override bool CreatePart(string filePath)
        {
            GetAssembleName();
            this.WorkpieceDirectoryPath = filePath;
            this.WorkpiecePath = filePath + this.AssembleName + ".prt";
            if (File.Exists(this.WorkpiecePath))
            {
                File.Delete(this.WorkpiecePath);
            }
            Part part = PartUtils.NewFile(this.WorkpiecePath) as Part;
            this.PartTag = part;
            SetAttribute();
            return true;
        }

        public override void GetAssembleName()
        {
            this.AssembleName = this.MoldInfo.MoldNumber + "-" + this.MoldInfo.WorkpieceNumber + "-ASM";
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
            return null;
        }
    }
}