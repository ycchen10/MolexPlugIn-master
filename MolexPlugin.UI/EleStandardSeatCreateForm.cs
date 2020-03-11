﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NXOpen;
using NXOpen.UF;
using NXOpen.Utilities;
using MolexPlugin.Model;
using Basic;
using MolexPlugin.DAL;

namespace MolexPlugin
{
    public class EleStandardSeatCreateForm
    {
        private Part workPart;
        private static UFSession theUFSession;
        private string vecName;
        private EleConditionModel model;

        public EleStandardSeatCreateForm(string vec)
        {
            theUFSession = UFSession.GetUFSession();
            workPart = Session.GetSession().Parts.Work;
            model = new EleConditionModel();
            this.vecName = vec.ToUpper();

        }

        private void ShowForm()
        {

            EleStandardSeatForm form = new EleStandardSeatForm(model);
            IntPtr intPtr = NXOpenUI.FormUtilities.GetDefaultParentWindowHandle();
            NXOpenUI.FormUtilities.ReparentForm(form);
            NXOpenUI.FormUtilities.SetApplicationIcon(form);
            Application.Run(form);
            form.Dispose();
        }

        public void Show()
        {
            Session.UndoMarkId markId;
            markId = Session.GetSession().SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "基准台");
            List<Body> bodys = new List<Body>();
            if (!AskAssembleJudge())
            {
                return;
            }
            if (!PartIsWork())
            {
                UI.GetUI().NXMessageBox.Show("错误", NXMessageBox.DialogType.Error, "请设置WORK为工作部件");
                return;
            }
            else
            {
                bodys = SelectObject();
                if (bodys == null || bodys.Count == 0)
                    return;
                this.model.Bodys = bodys;
                ShowForm();
            }

        }

        /// <summary>
        /// 选择对话框
        /// </summary>
        private List<Body> SelectObject()
        {
            List<Body> bodys = new List<Body>();
            string msg = "请选择电极头";
            string title = "电极基座";
            int res = 0;
            int connt = 0;
            IntPtr usr_data = IntPtr.Zero;
            Tag[] bodyObj;
            theUFSession.Ui.SelectWithClassDialog(msg, title, UFConstants.UF_UI_SEL_SCOPE_WORK_PART, sele_in_proc, usr_data, out res, out connt, out bodyObj);
            if (bodyObj.Length == 0)
            {
                return null;
            }

            for (int i = 0; i < bodyObj.Length; i++)
            {
                Body body = NXObjectManager.Get(bodyObj[i]) as Body;
                bodys.Add(body);
                body.Unhighlight();
            }
            return bodys;
        }

        /// <summary>
        /// 过滤选择
        /// </summary>
        /// <param name="select"></param>
        /// <param name="user_data"></param>
        /// <returns></returns>
        private static int sele_in_proc(IntPtr select, IntPtr user_data)
        {
            int num_triples = 1;
            UFUi.Mask[] triples = new UFUi.Mask[1];
            triples[0].object_type = 70;
            triples[0].solid_type = 0;
            triples[0].object_subtype = 0;
            theUFSession.Ui.SetSelMask(select, UFUi.SelMaskAction.SelMaskClearAndEnableSpecific, num_triples, triples);
            return UFConstants.UF_UI_SEL_SUCCESS;
        }

        private bool PartIsWork()
        {
            int workNumber = AttributeUtils.GetAttrForInt(workPart, "WorkNumber");
            if (workNumber != 0)
            {
                WorkAssembleModel work = new WorkAssembleModel();
                work.GetPart(workPart);
                this.model.Work = work;
                if (vecName == "Z+")
                    this.model.Vec = this.model.Work.Matr.GetZAxis();
                if (vecName == "X+")
                    this.model.Vec = this.model.Work.Matr.GetXAxis();
                if (vecName == "X-")
                {
                    Vector3d vec = this.model.Work.Matr.GetXAxis();
                    this.model.Vec = new Vector3d(-vec.X, -vec.Y, -vec.Z);
                }

                if (vecName == "Y+")
                    this.model.Vec = this.model.Work.Matr.GetYAxis();
                if (vecName == "Y-")
                {
                    Vector3d vec = this.model.Work.Matr.GetYAxis();
                    this.model.Vec = new Vector3d(-vec.X, -vec.Y, -vec.Z);
                }
                return true;
            }
            else
                return false;

        }

        private bool AskAssembleJudge()
        {
            AssembleInstance inst = AssembleInstance.GetInstance();
            AssembleCollection coll = inst.GetAssembleModle();
            if (coll.Modle.AsmModel == null)
            {
                UI.GetUI().NXMessageBox.Show("错误", NXMessageBox.DialogType.Error, "无法找到ASM装配档！");
                return false;
            }
            if (coll.Modle.EdmModel != null)
            {
                UI.GetUI().NXMessageBox.Show("错误", NXMessageBox.DialogType.Error, "无法找到EDM装配档！");
                return false;
            }
            return true;
        }
    }
}
