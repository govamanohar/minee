using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ISGN.Application.SessionProvider;
using ISGN.TCL.Controls.Datagrid;
using System.Collections;
using ISGN.Application.Common.Logging;
using ISGN.TCL.MODEL;
using System.Text;
using ISGN.TCL.Control.CustomGrid;
using ISGN.TCL.BL.Modules;
using ISGN.TCL.FrameWork.Base.BL;

namespace ISGN.TCL.Web.Modules.DailyProcessing
{
    public partial class NotesInfo : NoteInfoView
    {
        #region Declarations

        private NoteInfoPresenter noteInfoPresenter;
        private ISessionProvider sessionProvider = SessionProviderFactory.GetSessionProvider();
        private int iRowsPerPage = 10;
        private int iPageNumber = 1;
        //private bool mblnComRuleOverride = false;
        private bool mblnComRuleNoUpdate = false;
        private bool mblnComRuleChanged = false;
        private int mintLTOBLOC;
        private bool mblnInitLoad = false;
        private bool mblnFirstTime = false;
        string mstrExceedSUBLOC;
        public const string noteTypeBorrowerBase = "B";
        public const string noteTypeMaterNote = "M";
        public const string noteTypeSubNote = "D";
        public const string keyContEditFalse = "false";
        public const string ModifyTrue = "true";
        string Pnote = string.Empty;
        string Punit = string.Empty;
        string borrowerno = string.Empty;
        #endregion

        #region PageEvents

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.txtFincTabLoanCommit2Enabled = true;
                this.txtFincTabLoanCommit3Enabled = true;
                noteInfoPresenter = new NoteInfoPresenter(this);
                string s2 = this.NoteNumber = TCLHelper.GetData(Request.QueryString["N"]);
                string s3 = this.UnitNumber = TCLHelper.GetData(Request.QueryString["U"]);
                this.BorrowerNo = TCLHelper.GetData(Request.QueryString["B"]);
                sessionProvider.Set("Borrowervalues", BorrowerNo);
                sessionProvider.Set("Notevalues", NoteNumber);
                sessionProvider.Set("Unitvalues", UnitNumber);
                //this.IsNewNote = TCLHelper.ConvertToBool(Request.QueryString["IsNewNote"]);
                this.UserName = TCLHelper.GetData(sessionProvider.Get("User")).ToUpper();
                string s1 = this.NoteType = TCLHelper.GetData(Request.QueryString["NF"]);
                this.NoteMode = TCLHelper.GetData(sessionProvider.Get("gsNoteType"));
                this.IsBorrowerPortal = TCLHelper.ConvertToBool(Request.QueryString["BorrowerPortal"]);
                this.mProp_Inquiry = TCLHelper.ConvertToBool(Request.QueryString["IsInq"]);
                this.BBASE = TCLHelper.ConvertToBool(Request.QueryString["IsBase"]);
                this.Action = TCLHelper.ConvertToInt(Request.QueryString["Action"]);
                //venkatesh
                Pnote = TCLHelper.GetData(sessionProvider.Get("Notevalues"));
                Punit = TCLHelper.GetData(sessionProvider.Get("Unitvalues"));
                if (s1 == "M")
                    s1 = "Master Note - " + "&nbsp;";
                else if (s1 == "B")
                    s1 = "Borrower Note - " + "&nbsp;";
                else if (s1 == "D")
                    s1 = "Sub Note - " + "&nbsp;";
                else
                    s1 = "Single Maturity Note  " + "&nbsp;" + " - " + "&nbsp;";
                lblBrwName.Text = s1 + " " + " " + s2 + "&nbsp;&nbsp; " + s3;


                sessionProvider.Set("pnote", Pnote);
                sessionProvider.Set("punit", Punit);
                //end venkatesh
                lblBorrowingBaseMsg.Visible = false;
                InitializeViewStates();

                if ((this.Action == 2) && (this.bRenewal != 0))
                    this.dPrevMatDate = txtDateTabMturty.Text;
                this.dPrvCompDate = txtDateTabCnsCmpl.Text;

                this.noteInfoPresenter = new NoteInfoPresenter(this);

                //if (lstbStndardArtchBrwDocTrack.Items.Count > 0)
                //    btnDoctMove.Enabled = true;
                //else
                //    btnDoctMove.Enabled = false;

                //if (lstbArtchBrwDocTrack.Items.Count > 0)
                //    btnDocRemove.Enabled = true;
                //else
                //    btnDocRemove.Enabled = false;
                ScriptManager.RegisterStartupScript(upanel, upanel.GetType(), "Msg", "setDivWidth();", true);
                if (!IsPostBack)
                {
                    sessionProvider.Set("ModeBN", "");
                    NoteSecurity();
                    if (sessionProvider.Get("vendorName") != null)
                        sessionProvider.Clear("vendorName");
                    this.ReleaseSchNo = "1";
                    this.bLOCIntProf = false;
                    this.bLOCIntProfAdd = false;
                    this.bLOCEqtProf = false;
                    this.bLOCEqtProfAdd = false;
                    DataSet ds = this.noteInfoPresenter.LoadNoteInfo();
                    NoteinfoLoad(ds);
                    this.BindReleaseScheduleGrid(this.ListReleaseSchedule);
                    this.BindReleaseRuleGrid();
                    this.BindAttachedRuleGrid();
                    this.BindGeneral();
                    this.BindBBTerms();

                    ViewState.Add("BorrowerNo", this.BorrowerNo);
                    ViewState.Add("dPrevMatDate", this.dPrevMatDate);
                    ViewState.Add("sSubLookupIntProf", this.sSubLookupIntProf);
                    ViewState.Add("dPrvCompDate", this.dPrvCompDate);
                    ViewState.Add("nRenewalCount", this.nRenewalCount);
                    ViewState.Add("bRenewal", this.bRenewal);
                    ViewState.Add("bFeePostCompDate", this.bFeePostCompDate);
                    ViewState.Add("Action", this.Action);
                    ViewState.Add("mblnLOCProblemOnLoad", this.mblnLOCProblemOnLoad);
                    ViewState.Add("blnPostExtFee", this.blnPostExtFee);
                    ViewState.Add("nFeeAmount", this.nFeeAmount);
                    ViewState.Add("bLOCIntProfAdd", this.bLOCIntProfAdd);
                    ViewState.Add("bLOCEqtProfAdd", this.bLOCEqtProfAdd);
                    ViewState.Add("bLOCIntProf", this.bLOCIntProf);
                    ViewState.Add("bLOCEqtProf", this.bLOCEqtProf);
                    ViewState.Add("bFeePostMaturity", this.bFeePostMaturity);
                    ViewState.Add("dblMasterLookupIntProf", this.dblMasterLookupIntProf);
                    ViewState.Add("prsamt_d", this.prsamt_d);
                    ViewState.Add("pcfamt", this.pcfamt);
                    ViewState.Add("PCLAMT_D", this.PCLAMT_D);
                    ViewState.Add("PFDATE", this.PFDATE);

                    hdnBorrowerNo.Value = Convert.ToString(ViewState["BorrowerNo"]);
                    //this.noteInfoPresenter.AssignFinTabDefaultValues();
                    //this.GetGeneralNotes();
                    this.GetEquityProfileGrid();
                    this.GetIntrestProfileGrid();
                    this.BindNotePayRule();
                    string glCode = ddlGLTable.GetSelectedValue();
                    glCode = string.IsNullOrEmpty(glCode) == false ? glCode : "TCL";
                    this.noteInfoPresenter.GetTranCode(glCode);
                    BindUnitsGrid(this.noteInfoPresenter);
                    BindAuditGrid(this.noteInfoPresenter);

                    this.noteInfoPresenter.LoadDockTracking();
                    this.noteInfoPresenter.LoadDocAttach();
                    this.noteInfoPresenter.GetDated();
                    //To load Date Tab details for SubNote in future use the same function to load General tab as per ASIS
                    this.noteInfoPresenter.GetDateGeneralTab();
                    BindBorrowerAttachments();                    
                    //  
                    // Page.ClientScript.RegisterStartupScript(upanel.GetType(), "msg", "setDivWidth();", true);

                    if (Request.QueryString["IsBase"] != null)
                    {
                        if (TCLHelper.ConvertToBool(Request.QueryString["IsBase"]) == true)
                        {
                            txtPrjctStrtAddrs.Enabled = false;
                            txtPrjctCity.Enabled = false;
                            txtPrjctCounty.Enabled = false;
                            drpdwnlstState.Enabled = false;
                            txtPrjctZipCode.Enabled = false;
                            drpdwnlstCountry.Enabled = false;
                            txtPrjctShrtLglDesc.Enabled = true;
                            txtPrjctSubDiv.Enabled = false;
                            txtPrjctPurchLoan.Enabled = true;
                            txtPrjctPrimryContrct.Enabled = false;
                            chkDualAprvl.Enabled = false;
                            txtPrjctGPS.Enabled = false;
                            txtPrjctGPS1.Enabled = false;
                            txtPrjctAccID.Enabled = true;
                            drpdwnlstAdmin.Enabled = true;
                            txtPrjctLot.Enabled = false;
                            txtPrjctBlock.Enabled = false;
                            txtPrjctSection.Enabled = false;
                            txtPrjctPhase.Enabled = false;
                            txtPrjctNetRntArea.Enabled = false;
                            btnPjrctLeasingInfo.Visible = false;
                            btnPrjctApprslInfo.Visible = false;
                            btnprjctSalescntrct.Visible = false;
                            imgbtnPrjctVendor.Enabled = false;
                            lblBorrowingBaseMsg.Visible = true;
                        }
                    }

                    if (Request.QueryString["EditMode"] != null)
                    {
                        if (Request.QueryString["EditMode"].ToString() == "False")
                        {
                            //this.noteInfoPresenter.ClearData();
                            ///VendorBudget controls-Begin
                            //imgbtnSearchVendor.Enabled = false;
                            txtVNumber.Enabled = false;
                            rbVendorRel.Enabled = false;
                            btnApply.Enabled = false;
                            egBudget.Enabled = false;
                            // btncommitalltab.Enabled = false;
                            ///VendorBudget controls-End
                        }
                    }
                    ViewState.Add("blnAUDITRPT", this.blnAUDITRPT);
                    ViewState.Add("blnAR_TAXID", this.blnAR_TAXID);
                    ViewState.Add("blnAR_LDESC", this.blnAR_LDESC);
                    ViewState.Add("blnAR_LOC", this.blnAR_LOC);
                    ViewState.Add("blnAR_LOCMAT", this.blnAR_LOCMAT);
                    ViewState.Add("blnAR_INT", this.blnAR_INT);
                    ViewState.Add("blnAR_BILLOPT", this.blnAR_BILLOPT);
                    ViewState.Add("blnAR_BUD", this.blnAR_BUD);
                    ViewState.Add("blnAR_BUDPROF", this.blnAR_BUDPROF);
                    ViewState.Add("blnAR_EQTPROF", this.blnAR_EQTPROF);
                    ViewState.Add("blnAR_LOCPARENTTOTAL", this.blnAR_LOCPARENTTOTAL);
                    ViewState.Add("blnAR_LOCTERMS", this.blnAR_LOCTERMS);

                    this.noteInfoPresenter.ComRulesSetUp();
                    this.noteInfoPresenter.ComRulesApply();
                    this.Page.DataBind();
                    this.dPrvCompDate = TCLHelper.ConvertToDateTimeFormatted(txtDateTabCnsCmpl.Text,"MM/dd/yyyy");
                    ViewState.Add("dPrvCompDate", this.dPrvCompDate);
                    this.dPrevMatDate = TCLHelper.ConvertToDateTimeFormatted(txtDateTabMturty.Text,"MM/dd/yyyy");
                    ViewState.Add("dPrevMatDate", this.dPrevMatDate);
                    this.GetGeneralNotes();
                    this.mnPrvNonAccrual = chkNonAccural.Checked;
                    ViewState.Add("mnPrvNonAccrual", this.mnPrvNonAccrual);
                    if (drpdwnlstTranscode.Items.FindByValue(this.TransValue1) != null)
                        drpdwnlstTranscode.Items.FindByValue(this.TransValue1).Selected = true;
                    if (drpdwnlstLnvlDpstEftvTrncode.Items.FindByValue(this.TransValue2) != null)
                        drpdwnlstLnvlDpstEftvTrncode.Items.FindByValue(this.TransValue2).Selected = true;
                    if (drpdwnlstLnLvlEscrwTrncode.Items.FindByValue(this.TransValue3) != null)
                        drpdwnlstLnLvlEscrwTrncode.Items.FindByValue(this.TransValue3).Selected = true;
                }
                btnDoctMove.Enabled = false;
                btnDocRemove.Enabled = false;
                imgbtnDocTracAdd.Visible = false;
                imgbtnDocTracEdit.Visible = false;

                if (sessionProvider.Get("vendorName") != null)
                    txtPrjctPrimryContrct.Text = TCLHelper.GetData(sessionProvider.Get("vendorName"));

                this.ReleaseItemSelectedValue = drpdwnlstRlsItem.GetSelectedValue();
                if (cstGrdRlsPayoffTemp.GridViewPaging.Rows.Count == 0)
                {
                    btnRlsRulesMoveRight.Enabled = false;
                }
                else
                {
                    btnRlsRulesMoveRight.Enabled = true;
                }

                if (string.Compare(this.NoteType, noteTypeBorrowerBase) == 0)
                {
                    chk203K.Enabled = false;
                    NoteTypeSecurity(false);
                    ProjectTabSecurity(false);
                    //tabPnlRelease.Visible = true;
                    //tabPnlBdgCntrl.Visible = true;
                    //tabPnlProfiles.Visible = true;
                    //tabPnlkyCnt.Visible = true;
                    TabNoteInfo.Attributes.Add("OnClientActiveTabChanged", "clientActiveTabChanged");
                    return;
                }
                // Master Note
                else if (string.Compare(this.NoteType, noteTypeMaterNote) == 0)
                {
                    NoteTypeSecurity(true);
                    ProjectTabSecurity(false);
                    NoteTypeTabSecurity(false);
                }
                // Sub Note
                else if (string.Compare(this.NoteType, noteTypeSubNote) == 0)
                {
                    NoteTypeSecurity(true);
                    ProjectTabSecurity(true);
                    NoteTypeTabSecurity(true);
                    btnprjctSalescntrct.Visible = true;
                }
                // Single Maturity Note
                else
                {
                    NoteTypeSecurity(true);
                    ProjectTabSecurity(true);
                    NoteTypeTabSecurity(true);
                }
                if (mProp_Inquiry)
                {
                    DisableAllControls();
                    btnProjectSave.Visible = false;
                }
            }

            catch (Exception ex)
            {
                Logger.LogException(ex, Level.Error);
            }
        }

        #endregion
        private void NoteTypeTabSecurity(bool status)
        {
            tabPnlRelease.Visible = status;
            tabPnlBdgCntrl.Visible = status;
            tabPnlProfiles.Visible = status;
            tabPnlkyCnt.Visible = status;
        }
        private void NoteTypeSecurity(bool status)
        {
            pnlPricipal.Visible = status;

            pnlPurchAdd.Enabled = status;
            pnlGps.Enabled = status;
            pnlPrimaryContractor.Enabled = status;
            pnlLotSec.Enabled = status;
            pnlSubDivision.Enabled = status;
            txtPrjctSubDiv.Enabled = status;

            txtPrjctLot.Enabled = status;
            txtPrjctBlock.Enabled = status;
            txtPrjctSection.Enabled = status;
            txtPrjctPhase.Enabled = status;

            tabPnlBrwBasTerms.Visible = !status;
            tabPnlBrwBasUnits.Visible = !status;
            btnPrjctApprslInfo.Visible = status;

        }
        private void ProjectTabSecurity(bool status)
        {
            btnprjctSalescntrct.Visible = status;
            btnPjrctLeasingInfo.Visible = status;
            pnlTenantSpace.Visible = status;
        }
        #region Control Events

        protected void ImgbtnIntrstPrflsAdd_Click(object sender, EventArgs e)
        {
            string method = "ADD";
            string source = "INOTE";
            int intAmort = chkAmortizeLoan.Checked == true ? 1 : 0;
            int intDefault = chkInDefault.Checked == true ? 1 : 0;

            string script = "LoadIPEQWindow('" + this.NoteNumber + "', '" + this.UnitNumber + "', '" + method + "', '" + source + "', '" + this.BorrowerNo + "', '" + this.LCSeq + "', '" + this.dblLOCMID + "', '" + this.mProp_Inquiry + "', '" + intAmort + "', '" + intDefault + "')";
            InvokeJavascriptFunction(script);
        }

        protected void ImgbtnIntrstPrflsCopy_Click(object sender, EventArgs e)
        {
            string method = "COPY";
            string source = "INOTE";
            string effectiveDate = string.Empty;
            int selectedIndex = 0;
            if (IsGridrowSelected(CGIntrestProfile, ref selectedIndex))
            {
                effectiveDate = CGIntrestProfile.GridViewPaging.Rows[selectedIndex].Cells[2].Text;
                sessionProvider.Set("IEFFDATE", effectiveDate);
            }

            string script = "LoadIPEQWindow('" + this.NoteNumber + "', '" + this.UnitNumber + "', '" + method + "', '" + source + "', '" + this.BorrowerNo + "', '" + this.LCSeq + "', '" + this.dblLOCMID + "', '" + this.mProp_Inquiry + "')";
            InvokeJavascriptFunction(script);
        }

        protected void ImgbtEqtyPrflsAdd_Click(object sender, EventArgs e)
        {
            string method = "ADD";
            string source = "EQTYPROF";
            string equityType = string.Empty;
            string budgetID = string.Empty;

            string script = "LoadIPEQWindow('" + this.NoteNumber + "', '" + this.UnitNumber + "', '" + method + "', '" + source + "', '" + this.BorrowerNo + "', '" + this.LCSeq + "', '" + this.dblLOCMID + "', '" + this.mProp_Inquiry + "', '" + equityType + "', '" + budgetID + "')";
            InvokeJavascriptFunction(script);
        }

        protected void imgbtEqtyPrflsCopy_Click(object sender, EventArgs e)
        {
            string method = "COPY";
            string source = "EQTYPROF";
            string effectiveDate = string.Empty;
            string equityType = string.Empty;
            string budgetID = string.Empty;
            int selectedIndex = 0;
            if (IsGridrowSelected(CGEquityProfile, ref selectedIndex))
            {
                effectiveDate = CGEquityProfile.GridViewPaging.Rows[selectedIndex].Cells[5].Text;
                sessionProvider.Set("IEFFDATE", effectiveDate);
                equityType = CGEquityProfile.GridViewPaging.Rows[selectedIndex].Cells[4].Text;
                budgetID = CGEquityProfile.GridViewPaging.Rows[selectedIndex].Cells[3].Text;
            }

            string script = "LoadIPEQWindow('" + this.NoteNumber + "', '" + this.UnitNumber + "', '" + method + "', '" + source + "', '" + this.BorrowerNo + "', '" + this.LCSeq + "', '" + this.dblLOCMID + "', '" + this.mProp_Inquiry + "', '" + equityType + "', '" + budgetID + "')";
            InvokeJavascriptFunction(script);
        }

        #endregion

        #region BorrowingBase Units Shalin
        protected void btnRedirectUnits_Click(object sender, EventArgs e)
        {
            noteInfoPresenter = new NoteInfoPresenter(this);
            BindUnitsGrid(noteInfoPresenter);
            BindAuditGrid(noteInfoPresenter);
        }
        protected void btnRedirectTerms_Click(object sender, EventArgs e)
        {
            BindBBTerms();
        }
        private void BindUnitsGrid(NoteInfoPresenter objPresenter)
        {
            string strFilter = string.Empty;
            //string Pnote = TCLHelper.GetData(sessionProvider.Get("Notevalues"));
            //string Punit = TCLHelper.GetData(sessionProvider.Get("Unitvalues"));
            //sessionProvider.Set("pnote", Pnote);
            //sessionProvider.Set("punit", Punit);
            if (sessionProvider.Get("BBUnitFilter") != null)
            {
                strFilter = TCLHelper.GetData(sessionProvider.Get("BBUnitFilter"));
            }

            using (DataSet ds = objPresenter.GetBBCollateralByNoteUnit(
                        TCLHelper.GetData(sessionProvider.Get("gsNoteType")),
                    Pnote, Punit, string.Empty, false, strFilter))
            {
                GVUnitTerms.Visible = true;
                GVUnitTerms.GridAllowPaging = false;

                GVUnitTerms.DataKeyNames = new string[] { "P_PNOTENO", "P_PUNIT" };
                GVUnitTerms.ShowColumns = new int[] { 0, 1, 2, 3, 4, 5, 6, 7 };
                GVUnitTerms.GridShowFooter = true;
                GVUnitTerms.GridWidth = Unit.Percentage(100);
                GVUnitTerms.DataSource = ds;
                GVUnitTerms.BindGrid();
            }
        }

        private void BindAuditGrid(NoteInfoPresenter objPresenter)
        {
            string Pnote = TCLHelper.GetData(sessionProvider.Get("Notevalues"));
            string Punit = TCLHelper.GetData(sessionProvider.Get("Unitvalues"));

            using (DataSet ds = objPresenter.GetBBAuditLogRecords(
                    Pnote, Punit, 0))
            {
                GVUnitAuditLog.Visible = true;
                GVUnitAuditLog.GridAllowPaging = false;
                GVUnitAuditLog.DataKeyNames = new string[] { "PNOTENO", "PUNIT" };
                GVUnitAuditLog.ShowColumns = new int[] { 1, 3, 4, 5 };
                GVUnitAuditLog.GridShowFooter = true;
                GVUnitAuditLog.AllowLinks = new int[] { 1 };
                GVUnitAuditLog.DeleteMode = Control.CustomGrid.ExtendGrid.Deletemode.Single;
                GVUnitAuditLog.GridWidth = Unit.Percentage(100);
                GVUnitAuditLog.DataSource = ds;
                GVUnitAuditLog.BindGrid();
            }
        }

        //protected void btnBrwBaseFilter_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("BBUnitFilter.aspx");
        //}

        //protected void btnBrwBaseMangUnits_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("Borroweringbaseunit.aspx");
        //}

        //protected void btnBrwBstabLogUnit1_Click(object sender, EventArgs e)
        //{
        //    string redirectUrl = string.Empty;
        //    if (txtAuditLog1.Text != string.Empty)
        //        redirectUrl = "BBUnitAuditLog.aspx?Category=3&AuditDate=" + txtAuditLog1.Text.Replace("/", "-");
        //    else
        //        redirectUrl = "BBUnitAuditLog.aspx?Category=3";
        //    Response.Redirect(redirectUrl);
        //}

        //protected void btnBrwBstabLogUnit2_Click(object sender, EventArgs e)
        //{
        //    string redirectUrl = string.Empty;
        //    if (txtAuditLog1.Text != string.Empty)
        //        redirectUrl = "BBUnitAuditLog.aspx?Category=1&AuditDate=" + txtAuditLog1.Text.Replace("/", "-");
        //    else
        //        redirectUrl = "BBUnitAuditLog.aspx?Category=1";
        //    Response.Redirect(redirectUrl);
        //}

        //protected void ImgAuditAdd_Click(object sender, ImageClickEventArgs e)
        //{
        //    Response.Redirect("BBUnitAuditLog.aspx?Category=2");
        //}

        protected void ImgAuditDelete_Click(object sender, ImageClickEventArgs e)
        {
            noteInfoPresenter = new NoteInfoPresenter(this);
            bool success = false;
            string msg = string.Empty;
            if (ViewState["AuditID"] == null)
            {
                noteInfoPresenter.ShowMessageBox("Please select a record to delete");
                return;
            }
            success = noteInfoPresenter.DeleteAuditRecord(TCLHelper.ConvertToDouble(ViewState["AuditID"]));
            BindAuditGrid(noteInfoPresenter);

            lblErrorFail.Text = "Deleted Successfully";
        }

        protected void GVUnitAuditLog_GridCheckedChange(object sender, ImageClickEventArgs e)
        {
            GridViewRow gvRow = GVUnitAuditLog.SelectedRow;
            string Recid = gvRow.Cells[1].Text;
            ImageButton SelectUnSelect = (ImageButton)gvRow.FindControl("imgRowSelect");
            if (SelectUnSelect.AlternateText == "1")
            {
                hdnAuditDelete.Value = "1";
                ViewState["AuditID"] = TCLHelper.GetData(gvRow.Cells[1].Text);
            }
            else
            {
                hdnAuditDelete.Value = "0";
                ViewState["AuditID"] = null;
            }
        }

        protected void GVUnitAuditLog_GridRowEditing(object sender, EventArgs e)
        {
            GridViewRow gvRow = GVUnitAuditLog.SelectedRow;
            string Recid = gvRow.Cells[1].Text;
            sessionProvider.Set("GVUnitAuditLog", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(),
                        "Script", "WOpen('bbu','" + Recid + "');", true);
        }
        #endregion

        #region Shanki(Project,Release,Rules)

        private void BindAttachedRuleGrid()
        {
            using (DataSet dsAttchRg = this.noteInfoPresenter.LoadAttachedReleaseRule())
            {
                if (dsAttchRg.IsValidDataColumn())
                {
                    cstGrdRlsPayoffAtchmnt.GridRowStyleCSS = "normalrow";
                    cstGrdRlsPayoffAtchmnt.GridHeaderRowCSS = "pl10 pr10 gridheader";
                    cstGrdRlsPayoffAtchmnt.GridAlternatingRowCSS = "altrow";
                    //cstGrdRlsPayoffAtchmnt.GridClientID = "cstGrdRlsPayoffAtchmnt";
                    cstGrdRlsPayoffAtchmnt.GridAllowPaging = false;
                    cstGrdRlsPayoffAtchmnt.GridWidth = Unit.Percentage(100);
                    cstGrdRlsPayoffAtchmnt.ShowColumns = new int[] { 0, 1, 2 };
                    cstGrdRlsPayoffAtchmnt.DeleteMode = Control.CustomGrid.ExtendGrid.Deletemode.Multiple;
                    cstGrdRlsPayoffAtchmnt.DataSource = dsAttchRg;
                    cstGrdRlsPayoffAtchmnt.BindGrid();
                }
            }
        }

        private void BindReleaseRuleGrid()
        {
            using (DataSet ds = this.noteInfoPresenter.LoadReleaseRules())
            {
                if (ds.IsValidDataColumn())
                {
                    cstGrdRlsPayoffTemp.GridRowStyleCSS = "normalrow";
                    cstGrdRlsPayoffTemp.GridHeaderRowCSS = "pl10 pr10 gridheader";
                    cstGrdRlsPayoffTemp.GridAlternatingRowCSS = "altrow";
                    //cstGrdRlsPayoffTemp.GridClientID = "cstGrdRlsPayoffTemp";
                    cstGrdRlsPayoffTemp.GridAllowPaging = false;
                    cstGrdRlsPayoffTemp.GridWidth = Unit.Percentage(100);
                    cstGrdRlsPayoffTemp.ShowColumns = new int[] { 0, 1, 2 };
                    cstGrdRlsPayoffTemp.DeleteMode = Control.CustomGrid.ExtendGrid.Deletemode.Multiple;
                    cstGrdRlsPayoffTemp.DataSource = ds;
                    cstGrdRlsPayoffTemp.BindGrid();
                }
            }
        }

        private void BindReleaseScheduleGrid(List<ReleaseScheduleGrid> lstReleaseSchedule)
        {
            imgbtnRelsSchDel.Visible = false;
            imgbtnRelsSchEdit.Visible = false;
            if (lstReleaseSchedule != null && lstReleaseSchedule.Count > 0)
            {
                imgbtnRelsSchDel.Visible = true;
                imgbtnRelsSchEdit.Visible = true;
                hdnItemID.Value = string.Empty;
                grdRlsSch.GridRowStyleCSS = "normalrow";
                grdRlsSch.GridHeaderRowCSS = "pl10 pr10 gridheader";
                grdRlsSch.GridAlternatingRowCSS = "altrow";
                //grdRlsSch.GridClientID = "grdRlsSch";
                grdRlsSch.GridAllowPaging = false;
                grdRlsSch.AllowLinks = new int[] { 0 };
                grdRlsSch.GridShowFooter = true;
                grdRlsSch.GridWidth = Unit.Percentage(100);
                grdRlsSch.DeleteMode = Control.CustomGrid.ExtendGrid.Deletemode.Single;
                grdRlsSch.DataKeyNames = new string[] { "Item" };
                grdRlsSch.ShowColumns = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
                grdRlsSch.BindGrid<ReleaseScheduleGrid>(lstReleaseSchedule);
            }
            else
            {
                lstReleaseSchedule = new List<ReleaseScheduleGrid>();
                hdnItemID.Value = string.Empty;
                grdRlsSch.GridRowStyleCSS = "normalrow";
                grdRlsSch.GridHeaderRowCSS = "pl10 pr10 gridheader";
                grdRlsSch.GridAlternatingRowCSS = "altrow";
                //grdRlsSch.GridClientID = "grdRlsSch";
                grdRlsSch.GridAllowPaging = false;
                grdRlsSch.AllowLinks = new int[] { 0 };
                grdRlsSch.GridShowFooter = true;
                grdRlsSch.GridWidth = Unit.Percentage(100);
                grdRlsSch.DeleteMode = Control.CustomGrid.ExtendGrid.Deletemode.Single;
                grdRlsSch.DataKeyNames = new string[] { "Item" };
                grdRlsSch.ShowColumns = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
                grdRlsSch.DataSource = null;
                //grdRlsSch.BindGrid<ReleaseScheduleGrid>(lstReleaseSchedule);
                grdRlsSch.BindGrid();
                grdRlsSch.GridViewPaging.DataSource = null;
                grdRlsSch.GridViewPaging.DataBind();
            }
        }

        private List<ReleaseScheduleGrid> ConvertDataSetToList(ref bool isSelected, ref int selectedIndex)
        {
            var categoryList = new List<ReleaseScheduleGrid>();
            int index = 0;
            foreach (GridViewRow row in grdRlsSch.GridViewPaging.Rows)
            {
                ReleaseScheduleGrid category = new ReleaseScheduleGrid()
                {
                    Item = TCLHelper.RemoveHtmlWhiteSpace(((System.Web.UI.WebControls.LinkButton)(row.Cells[1].Controls[0])).Text),
                    Description = TCLHelper.RemoveHtmlWhiteSpace(row.Cells[2].Text),
                    MinimumAmount = TCLHelper.RemoveHtmlWhiteSpace(row.Cells[3].Text),
                    ReleaseAmount = TCLHelper.RemoveHtmlWhiteSpace(row.Cells[4].Text),
                    LastReleaseDate = TCLHelper.RemoveHtmlWhiteSpace(row.Cells[5].Text),
                    Status = TCLHelper.RemoveHtmlWhiteSpace(row.Cells[6].Text),
                    ReleaseDate = TCLHelper.RemoveHtmlWhiteSpace(row.Cells[7].Text),
                    AppraisedAmount = TCLHelper.RemoveHtmlWhiteSpace(row.Cells[8].Text),
                    AppraisalDate = TCLHelper.RemoveHtmlWhiteSpace(row.Cells[9].Text),
                    Type = TCLHelper.RemoveHtmlWhiteSpace(row.Cells[10].Text),
                    DiscountedAmount = TCLHelper.RemoveHtmlWhiteSpace(row.Cells[11].Text),
                    QuoteDate = TCLHelper.RemoveHtmlWhiteSpace(row.Cells[12].Text),
                    PayDownFlag = TCLHelper.RemoveHtmlWhiteSpace(row.Cells[13].Text),
                    rssamt = TCLHelper.RemoveHtmlWhiteSpace(row.Cells[14].Text),
                    rsspsqft = TCLHelper.RemoveHtmlWhiteSpace(row.Cells[15].Text),
                    rsssqft = TCLHelper.RemoveHtmlWhiteSpace(row.Cells[16].Text),
                    rsvpsqft = TCLHelper.RemoveHtmlWhiteSpace(row.Cells[17].Text),
                    rsvsqft = TCLHelper.RemoveHtmlWhiteSpace(row.Cells[18].Text),
                };

                ImageButton selectButton = (ImageButton)row.FindControl("imgRowSelect");
                if (selectButton.AlternateText == "1")
                {
                    selectedIndex = index;
                    isSelected = true;
                }

                categoryList.Add(category);
                index += 1;
            }

            return categoryList;
        }

        protected void imgbtnRelsRulesDel_Click(object sender, ImageClickEventArgs e)
        {
            bool bFlag = false;
            foreach (GridViewRow gridViewRow in cstGrdRlsPayoffAtchmnt.GridViewPaging.Rows)
            {
                CheckBox chkRowSelect = (gridViewRow.Cells[0].FindControl("chkMultipleSelect") as CheckBox);
                if (chkRowSelect.Checked == true)
                {
                    bFlag = true;
                    ScriptManager.RegisterStartupScript(upanel, upanel.GetType(), "confirm", "DeleteNoteReleasePayOff('Are you sure you want to remove selected Payoff Rule(s)?');", true);
                    return;
                }
            }
            if (!bFlag)
            {
                ScriptManager.RegisterStartupScript(upanel, upanel.GetType(), "alert", "alert('No Rule(s) selected to remove.');", true);
                return;
            }

            //RemoveRuleItem strRuleID, "", "PAYOFFRULESRELEASE", goNoteInfo.NoteNum, goNoteInfo.UnitNum, strRsItem$
        }

        protected void btnRelsRulesDelete_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gridViewRow in cstGrdRlsPayoffAtchmnt.GridViewPaging.Rows)
            {
                CheckBox chkChecked = (CheckBox)gridViewRow.FindControl("chkMultipleSelect");
                if (chkChecked.Checked)
                {
                    string ruleId = TCLHelper.RemoveHtmlWhiteSpace(gridViewRow.Cells[2].Text);
                    string rsitem = drpdwnlstRlsItem.GetSelectedValue();
                    string errorCode = string.Empty;
                    this.noteInfoPresenter.RemoveRuleItem(ruleId, string.Empty, "PAYOFFRULESRELEASE", this.NoteNumber, this.UnitNumber, rsitem, out errorCode);
                }
            }
            BindAttachedRuleGrid();
            BindReleaseRuleGrid();
        }

        protected void imgbtnRelsSchAdd_Click(object sender, ImageClickEventArgs e)
        {
            List<ReleaseScheduleGrid> listReleaseSch = null;
            if (grdRlsSch.GridViewPaging.Rows.Count == 0)
            {
                listReleaseSch = new List<ReleaseScheduleGrid>();
                int totalcount = TCLHelper.ConvertToInt(txtRlsSchTabNum.Text);
                for (int i = 0; i < totalcount; i++)
                {
                    ReleaseScheduleGrid releaseScheduleGrid = new ReleaseScheduleGrid();
                    if (i == 0)
                    {
                        releaseScheduleGrid.Item = "00001";
                        listReleaseSch.Add(releaseScheduleGrid);
                    }
                    else
                    {
                        releaseScheduleGrid.Item = listReleaseSch.Max(a => TCLHelper.ConvertToInt(a.Item) + 1).ToString("00000");
                        listReleaseSch.Add(releaseScheduleGrid);
                    }

                    this.noteInfoPresenter.InsertReleaseSchedule(releaseScheduleGrid);
                }
            }
            else
            {
                bool isSelected = false;
                int selectedIndex = -1;
                listReleaseSch = this.ConvertDataSetToList(ref isSelected, ref selectedIndex);
                if (selectedIndex > -1 && isSelected == true)
                {
                    for (int i = 0; i < TCLHelper.ConvertToInt(txtRlsSchTabNum.Text); i++)
                    {
                        string rlsItem = listReleaseSch.Max(a => TCLHelper.ConvertToInt(a.Item) + 1).ToString("00000");
                        ReleaseScheduleGrid relsSchGrid = new ReleaseScheduleGrid()
                        {
                            Item = listReleaseSch.Max(a => TCLHelper.ConvertToInt(a.Item) + 1).ToString("00000"),
                            Description = listReleaseSch[selectedIndex].Description,
                            MinimumAmount = listReleaseSch[selectedIndex].MinimumAmount,
                            ReleaseAmount = listReleaseSch[selectedIndex].ReleaseAmount,
                            LastReleaseDate = listReleaseSch[selectedIndex].LastReleaseDate,
                            Status = listReleaseSch[selectedIndex].Status,
                            ReleaseDate = listReleaseSch[selectedIndex].ReleaseDate,
                            AppraisedAmount = listReleaseSch[selectedIndex].AppraisedAmount,
                            AppraisalDate = listReleaseSch[selectedIndex].AppraisalDate,
                            Type = listReleaseSch[selectedIndex].Type,
                            DiscountedAmount = listReleaseSch[selectedIndex].DiscountedAmount,
                            QuoteDate = listReleaseSch[selectedIndex].QuoteDate,
                            PayDownFlag = listReleaseSch[selectedIndex].PayDownFlag,
                            rssamt = listReleaseSch[selectedIndex].rssamt,
                            rsspsqft = listReleaseSch[selectedIndex].rsspsqft,
                            rsssqft = listReleaseSch[selectedIndex].rsssqft,
                            rsvpsqft = listReleaseSch[selectedIndex].rsvpsqft,
                            rsvsqft = listReleaseSch[selectedIndex].rsvsqft,
                        };
                        listReleaseSch.Add(relsSchGrid);
                        this.noteInfoPresenter.InsertReleaseSchedule(relsSchGrid);
                    }
                }
                else
                {
                    List<ReleaseScheduleGrid> listReleaseSchInsert = new List<ReleaseScheduleGrid>();
                    for (int i = 0; i < TCLHelper.ConvertToInt(txtRlsSchTabNum.Text); i++)
                    {
                        if (listReleaseSchInsert.Count == 0)
                        {
                            listReleaseSchInsert.Add(new ReleaseScheduleGrid() { Item = listReleaseSch.Max(a => TCLHelper.ConvertToInt(a.Item) + 1).ToString("00000") });
                        }
                        else
                        {
                            listReleaseSchInsert.Add(new ReleaseScheduleGrid() { Item = listReleaseSchInsert.Max(a => TCLHelper.ConvertToInt(a.Item) + 1).ToString("00000") });
                        }
                        this.noteInfoPresenter.InsertReleaseSchedule(listReleaseSchInsert[i]);
                    }
                    listReleaseSch = listReleaseSch.Concat(listReleaseSchInsert).ToList();
                }
                //grdRlsSch.DataSource = listReleaseSch;
                //grdRlsSch.BindGrid<ReleaseScheduleGrid>(listReleaseSch);                
            }

            BindReleaseScheduleGrid(listReleaseSch);


            if (grdRlsSch.GridViewPaging.Rows.Count > 0)
            {
                drpdwnlstRlsItem.Items.Clear();
                this.noteInfoPresenter.LoadReleaseItems();
                drpdwnlstRlsItem.DataBind();
            }
        }
        protected void btnCancelRelschUpdate_Click(object sender, EventArgs e)
        {
            mdlPopupRelschUpdate.Hide();
        }

        protected void imgbtnRelsSchEdit_Click(object sender, ImageClickEventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Open", "javascript:c;", true);
        }

        public bool IsGridrowSelected(ExtendGrid gridName, ref int selectedIndex)
        {
            int index = 0;
            foreach (GridViewRow gvRow in gridName.GridViewPaging.Rows)
            {
                ImageButton selectButton = (ImageButton)gvRow.FindControl("imgRowSelect");
                if (selectButton.AlternateText == "1")
                {
                    LinkButton lnkButton = (LinkButton)gvRow.FindControl("lnk0");
                    selectedIndex = index;
                    return true;
                }

                index += 1;
            }

            return false;
        }

        protected void imgbtnRelsSchDel_Click(object sender, ImageClickEventArgs e)
        {
            //if (TCLHelper.ConvertToInt(adoRelsch!RSAAMT)==0) { 
            //RemoveRuleItem("", TCLHelper.GetData(adoSetupModTable("PBNUMBER")), "PAYOFFRULESRELEASE", goNoteInfo.NoteNum, goNoteInfo.UnitNum, pubFmtRelColItem(TCLHelper.GetData(adoRelsch("RSITEM"))));
            //adoRelsch.Requery(); 
            //fplstRelsch.DataSource = adoRelsch; 
            //if (adoRelsch.RecordCount==0) { 
            //lblHiddenRel.Text = Convert.ToString(O);
            // ---------------------------------------------------------------------------Ravi Shankar Sundaram
            //1:42 PM
            //} else { 
            //adoRelsch.MoveLast(); 
            //lblHiddenRel.Text = pubFmtRelColItem(TCLHelper.GetData((adoRelsch!RSITEM))); 
            //} 
            //} else { 
            //MessageBox.Show("Cannot delete release schedule"+gsCR_LF+TCLHelper.GetData(adoRelsch!RSDESC)+gsCR_LF+"Release Amount: "+(TCLHelper.ConvertToInt(adoRelsch!RSAAMT)).ToString("Currency"),"Delete Not Allowed", vbCritical+vbOKOnly, vbCritical+vbOKOnly); 
            //} 
            //SetButtons(); // Set cmd(2-5) 
            //if (fplstRelsch.Enabled) fplstRelsch.Focus(); 
            //} 
            int selectedIndex = 0;
            bool isSelected = IsGridrowSelected(grdRlsSch, ref selectedIndex);
            if (isSelected)
            {
                this.noteInfoPresenter.DeleteRelease(hdnItemID.Value);
                string errorCode = string.Empty;
                this.noteInfoPresenter.RemoveRuleItem(string.Empty, this.BorrowerNo, "PAYOFFRULESRELEASE", this.NoteNumber, this.UnitNumber, "", out errorCode);

                var categoryList = new List<ReleaseScheduleGrid>();
                foreach (GridViewRow row in grdRlsSch.GridViewPaging.Rows)
                {
                    ReleaseScheduleGrid category = new ReleaseScheduleGrid()
                    {
                        Item = TCLHelper.RemoveHtmlWhiteSpace(((System.Web.UI.WebControls.LinkButton)(row.Cells[1].Controls[0])).Text),
                        Description = TCLHelper.RemoveHtmlWhiteSpace(row.Cells[2].Text),
                        MinimumAmount = TCLHelper.RemoveHtmlWhiteSpace(row.Cells[3].Text),
                        ReleaseAmount = TCLHelper.RemoveHtmlWhiteSpace(row.Cells[4].Text),
                        LastReleaseDate = TCLHelper.RemoveHtmlWhiteSpace(row.Cells[5].Text),
                        Status = TCLHelper.RemoveHtmlWhiteSpace(row.Cells[6].Text),
                        ReleaseDate = TCLHelper.RemoveHtmlWhiteSpace(row.Cells[7].Text),
                        AppraisedAmount = TCLHelper.RemoveHtmlWhiteSpace(row.Cells[8].Text),
                        AppraisalDate = TCLHelper.RemoveHtmlWhiteSpace(row.Cells[9].Text),
                        Type = TCLHelper.RemoveHtmlWhiteSpace(row.Cells[10].Text),
                        DiscountedAmount = TCLHelper.RemoveHtmlWhiteSpace(row.Cells[11].Text),
                        QuoteDate = TCLHelper.RemoveHtmlWhiteSpace(row.Cells[12].Text)
                    };
                    if (hdnItemID.Value != TCLHelper.RemoveHtmlWhiteSpace(((System.Web.UI.WebControls.LinkButton)(row.Cells[1].Controls[0])).Text))
                    {
                        categoryList.Add(category);
                    }
                }
                ScriptManager.RegisterStartupScript(upanel, upanel.GetType(), "confirm", "alert('Selected Release schedule deleted Successfully.');", true);
                BindReleaseScheduleGrid(categoryList);
            }
            else
            {
                ScriptManager.RegisterStartupScript(upanel, upanel.GetType(), "confirm", "alert('Must select a Release schedule before delete.');", true);
            }
        }

        protected void grdRlsSch_GridCheckedChange(object sender, ImageClickEventArgs e)
        {
            GridViewRow gvRow = grdRlsSch.SelectedRow;
            ImageButton SelectUnSelect = (ImageButton)gvRow.FindControl("imgRowSelect");
            if (SelectUnSelect.AlternateText == "1")
            {
                hdnItemID.Value = TCLHelper.RemoveHtmlWhiteSpace(((System.Web.UI.WebControls.LinkButton)(gvRow.Cells[1].Controls[0])).Text);
            }
            else
            {
                hdnItemID.Value = string.Empty;
            }
        }

        protected void btnRlsRulesMoveRight_Click(object sender, EventArgs e)
        {
            PayOffRulesEntity payOffRulesEntity = new PayOffRulesEntity();
            List<string> lstErrMsg = new List<string>();
            bool bFlag = false;
            if (drpdwnlstRlsItem.SelectedIndex == -1)
            {
                ScriptManager.RegisterStartupScript(upanel, upanel.GetType(), "confirm", "alert('Must select a Release Item before attaching.');", true);
                return;
            }
            else
            {
                foreach (GridViewRow item in cstGrdRlsPayoffTemp.GridViewPaging.Rows)
                {
                    CheckBox chkRowSelect = (item.Cells[0].FindControl("chkMultipleSelect") as CheckBox);
                    if (chkRowSelect.Checked == true)
                    {
                        bFlag = true;
                        payOffRulesEntity.RuleType = TCLHelper.RemoveHtmlWhiteSpace(item.Cells[1].Text);
                        payOffRulesEntity.RuleID = TCLHelper.RemoveHtmlWhiteSpace(item.Cells[2].Text);
                        payOffRulesEntity.RuleDescription = TCLHelper.RemoveHtmlWhiteSpace(item.Cells[3].Text);
                        payOffRulesEntity.Calc1Desc = TCLHelper.RemoveHtmlWhiteSpace(item.Cells[4].Text);
                        payOffRulesEntity.Calc1Value = TCLHelper.ConvertToFloat(TCLHelper.RemoveHtmlWhiteSpace(item.Cells[5].Text));
                        payOffRulesEntity.Calc1UsePcnt = TCLHelper.ConvertToInt(TCLHelper.RemoveHtmlWhiteSpace(item.Cells[6].Text));
                        payOffRulesEntity.Calc1Operation = TCLHelper.RemoveHtmlWhiteSpace(item.Cells[7].Text);
                        payOffRulesEntity.Calc1Field = TCLHelper.RemoveHtmlWhiteSpace(item.Cells[8].Text);
                        payOffRulesEntity.Calc1FieldLabel = TCLHelper.RemoveHtmlWhiteSpace(item.Cells[9].Text);
                        payOffRulesEntity.Calc1Prompt = TCLHelper.ConvertToInt(TCLHelper.RemoveHtmlWhiteSpace(item.Cells[10].Text));
                        payOffRulesEntity.Calc1Calc2Operation = TCLHelper.RemoveHtmlWhiteSpace(item.Cells[11].Text);
                        payOffRulesEntity.Calc2Desc = TCLHelper.RemoveHtmlWhiteSpace(item.Cells[12].Text);
                        payOffRulesEntity.Calc2Value = TCLHelper.ConvertToFloat(TCLHelper.RemoveHtmlWhiteSpace(item.Cells[13].Text));
                        payOffRulesEntity.Calc2UsePcnt = TCLHelper.ConvertToInt(TCLHelper.RemoveHtmlWhiteSpace(item.Cells[14].Text));
                        payOffRulesEntity.Calc2Operation = TCLHelper.RemoveHtmlWhiteSpace(item.Cells[15].Text);
                        payOffRulesEntity.Calc2Field = TCLHelper.RemoveHtmlWhiteSpace(item.Cells[16].Text);
                        payOffRulesEntity.Calc2FieldLabel = TCLHelper.RemoveHtmlWhiteSpace(item.Cells[17].Text);
                        payOffRulesEntity.Calc2Prompt = TCLHelper.ConvertToInt(TCLHelper.RemoveHtmlWhiteSpace(item.Cells[18].Text));
                        payOffRulesEntity.LOTLOANNUM = TCLHelper.RemoveHtmlWhiteSpace(item.Cells[19].Text);
                        payOffRulesEntity.LOTUNITNUM = TCLHelper.RemoveHtmlWhiteSpace(item.Cells[20].Text);
                        payOffRulesEntity.LOTLOANRELEASE = TCLHelper.RemoveHtmlWhiteSpace(item.Cells[21].Text);
                        //string BNUMBER = item.Cells[22].Text;
                        //string AutoAttach = item.Cells[23].Text;
                        //string RuleEdited = item.Cells[24].Text;
                        payOffRulesEntity.PNOTE = this.NoteNumber;
                        payOffRulesEntity.PUNIT = this.UnitNumber;
                        payOffRulesEntity.RSITEM = drpdwnlstRlsItem.GetSelectedValue();
                        //string RSDESC = item.Cells[28].Text;
                        //string Calc1Result = item.Cells[29].Text;
                        //string Calc2Result = item.Cells[30].Text;
                        //string POQRECID = item.Cells[31].Text;
                        //string POQRULERECID = item.Cells[32].Text;
                        //string RQRECID = item.Cells[33].Text;
                        //string EditedDesc = item.Cells[34].Text;
                        //string LOTLOANNUMBER = item.Cells[35].Text;
                        //string QuotedAmount = item.Cells[36].Text;
                        //string UnknowValues = item.Cells[37].Text;
                        //string EOR = item.Cells[38].Text;
                        string returnMessage = this.noteInfoPresenter.InsertReleaseRules_Temp(payOffRulesEntity);
                        if (string.IsNullOrEmpty(returnMessage) == false)
                        {
                            returnMessage = returnMessage.Replace("^", Environment.NewLine);
                            lstErrMsg.Add(returnMessage);
                        }

                        this.BindAttachedRuleGrid();
                        this.BindReleaseRuleGrid();
                    }
                }
                if (!bFlag)
                {
                    ScriptManager.RegisterStartupScript(upanel, upanel.GetType(), "confirm", "alert('No Rule(s) selected to attach.');", true);
                    return;
                }
            }

            if (lstErrMsg.Count > 0)
            {
                string message = TCLHelper.JavascriptSerializer<List<string>>(lstErrMsg);
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "message", "javascript:messageParse(" + message + ");", true);


            }
        }

        protected void drpdwnlstRlsItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ReleaseItemSelectedValue = drpdwnlstRlsItem.GetSelectedValue();
            this.BindAttachedRuleGrid();
            this.BindReleaseRuleGrid();
        }

        protected void chkRlsRulsShwoAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRlsRulsShwoAll.Checked == true)
            {
                this.BindReleaseRuleGrid();
            }
            else
            {
                string selValue = drpdwnlstRlsItem.GetSelectedValue();
                using (DataSet ds = this.noteInfoPresenter.FilterRuleRelease(selValue))
                {
                    if (ds.IsValidDataSet())
                    {
                        cstGrdRlsPayoffTemp.GridRowStyleCSS = "normalrow";
                        cstGrdRlsPayoffTemp.GridHeaderRowCSS = "pl10 pr10 gridheader";
                        cstGrdRlsPayoffTemp.GridAlternatingRowCSS = "altrow";
                        //cstGrdRlsPayoffTemp.GridClientID = "cstGrdRlsPayoffTemp";
                        cstGrdRlsPayoffTemp.GridAllowPaging = false;
                        cstGrdRlsPayoffTemp.GridWidth = Unit.Percentage(100);
                        cstGrdRlsPayoffTemp.ShowColumns = new int[] { 0, 1, 2 };
                        cstGrdRlsPayoffTemp.DeleteMode = Control.CustomGrid.ExtendGrid.Deletemode.Multiple;
                        cstGrdRlsPayoffTemp.DataSource = ds;
                        cstGrdRlsPayoffTemp.BindGrid();
                    }
                }
            }
        }

        protected void grdRlsSch_GridRowEditing(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "OpenSingle", "javascript:OpenSingle();", true);
        }

        #endregion

        #region Gopi(Financial,Interest,Equiry)

        private void InitializeViewStates()
        {
            if (ViewState["BorrowerNo"] != null && string.IsNullOrEmpty(ViewState["BorrowerNo"].ToString()) == false)
            {
                this.BorrowerNo = ViewState["BorrowerNo"].ToString();
            }
            if (ViewState["dPrevMatDate"] != null && string.IsNullOrEmpty(ViewState["dPrevMatDate"].ToString()) == false)
            {
                this.dPrevMatDate = ViewState["dPrevMatDate"].ToString();
            }

            if (ViewState["blnPostExtFee"] != null && string.IsNullOrEmpty(ViewState["blnPostExtFee"].ToString()) == false)
            {
                this.blnPostExtFee = TCLHelper.ConvertToBool(ViewState["blnPostExtFee"].ToString());
            }

            if (ViewState["bFeePostMaturity"] != null && string.IsNullOrEmpty(ViewState["bFeePostMaturity"].ToString()) == false)
            {
                this.bFeePostMaturity = TCLHelper.ConvertToInt(ViewState["bFeePostMaturity"].ToString());
            }

            if (ViewState["nFeeAmount"] != null && string.IsNullOrEmpty(ViewState["nFeeAmount"].ToString()) == false)
            {
                this.nFeeAmount = TCLHelper.ConvertToDouble(ViewState["nFeeAmount"].ToString());
            }

            if (ViewState["bLOCIntProfAdd"] != null && string.IsNullOrEmpty(ViewState["bLOCIntProfAdd"].ToString()) == false)
            {
                this.bLOCIntProfAdd = TCLHelper.ConvertToBool(ViewState["bLOCIntProfAdd"].ToString());
            }

            if (ViewState["bLOCIntProf"] != null && string.IsNullOrEmpty(ViewState["bLOCIntProf"].ToString()) == false)
            {
                this.bLOCIntProf = TCLHelper.ConvertToBool(ViewState["bLOCIntProf"].ToString());
            }

            if (ViewState["bLOCEqtProfAdd"] != null && string.IsNullOrEmpty(ViewState["bLOCEqtProfAdd"].ToString()) == false)
            {
                this.bLOCEqtProfAdd = TCLHelper.ConvertToBool(ViewState["bLOCEqtProfAdd"].ToString());
            }

            if (ViewState["bLOCEqtProf"] != null && string.IsNullOrEmpty(ViewState["bLOCEqtProf"].ToString()) == false)
            {
                this.bLOCEqtProf = TCLHelper.ConvertToBool(ViewState["bLOCEqtProf"].ToString());
            }

            if (ViewState["mblnLOCProblemOnLoad"] != null && string.IsNullOrEmpty(ViewState["mblnLOCProblemOnLoad"].ToString()) == false)
            {
                this.mblnLOCProblemOnLoad = TCLHelper.ConvertToBool(ViewState["mblnLOCProblemOnLoad"].ToString());
            }

            if (ViewState["dPrvCompDate"] != null && string.IsNullOrEmpty(ViewState["dPrvCompDate"].ToString()) == false)
            {
                this.dPrvCompDate = ViewState["dPrvCompDate"].ToString();
            }

            if (ViewState["Action"] != null && string.IsNullOrEmpty(ViewState["Action"].ToString()) == false)
            {
                this.Action = TCLHelper.ConvertToInt(ViewState["Action"].ToString());
            }

            if (ViewState["bRenewal"] != null && string.IsNullOrEmpty(ViewState["bRenewal"].ToString()) == false)
            {
                this.bRenewal = TCLHelper.ConvertToInt(ViewState["bRenewal"].ToString());
            }

            if (ViewState["bFeePostCompDate"] != null && string.IsNullOrEmpty(ViewState["bFeePostCompDate"].ToString()) == false)
            {
                this.bFeePostCompDate = TCLHelper.ConvertToInt(ViewState["bFeePostCompDate"].ToString());
            }

            if (ViewState["nRenewalCount"] != null && string.IsNullOrEmpty(ViewState["nRenewalCount"].ToString()) == false)
            {
                this.nRenewalCount = TCLHelper.ConvertToInt(ViewState["nRenewalCount"].ToString());
            }

            if (ViewState["sSubLookupIntProf"] != null && string.IsNullOrEmpty(ViewState["sSubLookupIntProf"].ToString()) == false)
            {
                this.sSubLookupIntProf = ViewState["sSubLookupIntProf"].ToString();
            }

            if (ViewState["dblMasterLookupIntProf"] != null && string.IsNullOrEmpty(ViewState["dblMasterLookupIntProf"].ToString()) == false)
            {
                this.dblMasterLookupIntProf = ViewState["dblMasterLookupIntProf"].ToString();
            }

            if (ViewState["prsamt_d"] != null && string.IsNullOrEmpty(ViewState["prsamt_d"].ToString()) == false)
            {
                this.prsamt_d = ViewState["prsamt_d"].ToString();
            }

            if (ViewState["pcfamt"] != null && string.IsNullOrEmpty(ViewState["pcfamt"].ToString()) == false)
            {
                this.pcfamt = ViewState["pcfamt"].ToString();
            }

            if (ViewState["PCLAMT_D"] != null && string.IsNullOrEmpty(ViewState["PCLAMT_D"].ToString()) == false)
            {
                this.PCLAMT_D = ViewState["PCLAMT_D"].ToString();
            }

            if (ViewState["PFDATE"] != null && string.IsNullOrEmpty(ViewState["PFDATE"].ToString()) == false)
            {
                this.PFDATE = ViewState["PFDATE"].ToString();
            }

            if (ViewState["mnPrvNonAccrual"] != null && string.IsNullOrEmpty(ViewState["mnPrvNonAccrual"].ToString()) == false)
            {
                this.mnPrvNonAccrual = TCLHelper.ConvertToBool(ViewState["mnPrvNonAccrual"]);
            }

            if (ViewState["blnAUDITRPT"] != null && string.IsNullOrEmpty(ViewState["blnAUDITRPT"].ToString()) == false)
            {
                this.blnAUDITRPT = TCLHelper.ConvertToBool(ViewState["blnAUDITRPT"]);
            }

            if (ViewState["blnAR_TAXID"] != null && string.IsNullOrEmpty(ViewState["blnAR_TAXID"].ToString()) == false)
            {
                this.blnAR_TAXID = TCLHelper.ConvertToBool(ViewState["blnAR_TAXID"]);
            }

            if (ViewState["blnAR_LDESC"] != null && string.IsNullOrEmpty(ViewState["blnAR_LDESC"].ToString()) == false)
            {
                this.blnAR_LDESC = TCLHelper.ConvertToBool(ViewState["blnAR_LDESC"]);
            }

            if (ViewState["blnAR_LOC"] != null && string.IsNullOrEmpty(ViewState["blnAR_LOC"].ToString()) == false)
            {
                this.blnAR_LOC = TCLHelper.ConvertToBool(ViewState["blnAR_LOC"]);
            }

            if (ViewState["blnAR_LOCMAT"] != null && string.IsNullOrEmpty(ViewState["blnAR_LOCMAT"].ToString()) == false)
            {
                this.blnAR_LOCMAT = TCLHelper.ConvertToBool(ViewState["blnAR_LOCMAT"]);
            }

            if (ViewState["blnAR_INT"] != null && string.IsNullOrEmpty(ViewState["blnAR_INT"].ToString()) == false)
            {
                this.blnAR_INT = TCLHelper.ConvertToBool(ViewState["blnAR_INT"]);
            }

            if (ViewState["blnAR_BILLOPT"] != null && string.IsNullOrEmpty(ViewState["blnAR_BILLOPT"].ToString()) == false)
            {
                this.blnAR_BILLOPT = TCLHelper.ConvertToBool(ViewState["blnAR_BILLOPT"]);
            }

            if (ViewState["blnAR_BUD"] != null && string.IsNullOrEmpty(ViewState["blnAR_BUD"].ToString()) == false)
            {
                this.blnAR_BUD = TCLHelper.ConvertToBool(ViewState["blnAR_BUD"]);
            }

            if (ViewState["blnAR_BUDPROF"] != null && string.IsNullOrEmpty(ViewState["blnAR_BUDPROF"].ToString()) == false)
            {
                this.blnAR_BUDPROF = TCLHelper.ConvertToBool(ViewState["blnAR_BUDPROF"]);
            }

            if (ViewState["blnAR_EQTPROF"] != null && string.IsNullOrEmpty(ViewState["blnAR_EQTPROF"].ToString()) == false)
            {
                this.blnAR_EQTPROF = TCLHelper.ConvertToBool(ViewState["blnAR_EQTPROF"]);
            }

            if (ViewState["blnAR_LOCPARENTTOTAL"] != null && string.IsNullOrEmpty(ViewState["blnAR_LOCPARENTTOTAL"].ToString()) == false)
            {
                this.blnAR_LOCPARENTTOTAL = TCLHelper.ConvertToBool(ViewState["blnAR_LOCPARENTTOTAL"]);
            }

            if (ViewState["blnAR_LOCTERMS"] != null && string.IsNullOrEmpty(ViewState["blnAR_LOCTERMS"].ToString()) == false)
            {
                this.blnAR_LOCTERMS = TCLHelper.ConvertToBool(ViewState["blnAR_LOCTERMS"]);
            }

            if (ViewState["cAlloc"] != null && string.IsNullOrEmpty(ViewState["cAlloc"].ToString()) == false)
            {
                this.cAlloc = TCLHelper.ConvertToDouble(ViewState["cAlloc"]);
            }

            if (ViewState["cEst"] != null && string.IsNullOrEmpty(ViewState["cEst"].ToString()) == false)
            {
                this.cEst = TCLHelper.ConvertToDouble(ViewState["cEst"]);
            }

            if (ViewState["cDiff"] != null && string.IsNullOrEmpty(ViewState["cDiff"].ToString()) == false)
            {
                this.cDiff = TCLHelper.ConvertToDouble(ViewState["cDiff"]);
            }
        }

        protected void imgbtnBdgtCntrlAdd_Click(object sender, ImageClickEventArgs e)
        {
            lblNoteNo.Text = this.NoteNumber;
            lblUnitNumber.Text = this.UnitNumber;
            lblBudgetId.Text = Convert.ToString(ddlBudgets.SelectedItem.Text);
            txtGroupDesc.Text = "";

            if (string.IsNullOrEmpty(ddlBudgets.SelectedValue) == true)
            {
                ScriptManager.RegisterStartupScript(upanel, upanel.GetType(), "confirm", "alert('Please select a Budget Group.');", true);
                return;
            }
            mpeAddBudget.Show();
        }

        private void GetEquityProfileGrid()
        {
            imgbtEqtyPrflsCopy.Enabled = false;
            imgbtEqtyPrflsDel.Enabled = false;
            using (DataSet dsEquityProfile = this.noteInfoPresenter.LoadEquityProfileGrid())
            {
                if (dsEquityProfile.IsValidDataColumn() == true)
                {
                    hdnEPEffDate.Value = string.Empty;
                    CGEquityProfile.GridRowStyleCSS = "normalrow";
                    CGEquityProfile.GridHeaderRowCSS = "pl10 pr10 gridheader";
                    CGEquityProfile.GridAlternatingRowCSS = "altrow";
                    CGEquityProfile.GridViewPaging.ShowHeader = true;
                    CGEquityProfile.GridAllowPaging = false;
                    CGEquityProfile.AllowLinks = new int[] { 4 };
                    CGEquityProfile.DataKeyNames = new string[] { "EFFDATE" };
                    //CGEquityProfile.GridWidth = Unit.Percentage(100);
                    CGEquityProfile.DeleteMode = Control.CustomGrid.ExtendGrid.Deletemode.Single;
                    CGEquityProfile.DataSource = dsEquityProfile;
                    CGEquityProfile.BindGrid();
                    if (dsEquityProfile.IsValidDataSet())
                    {
                        imgbtEqtyPrflsCopy.Enabled = true;
                        imgbtEqtyPrflsDel.Enabled = true;
                    }
                }
            }
        }

        private void GetIntrestProfileGrid()
        {
            imgbtnIntrstPrflsDel.Enabled = false;
            imgbtnIntrstPrflsCopy.Enabled = false;
            using (DataSet dsInterestProf = this.noteInfoPresenter.LoadIntrestProfileGrid())
            {
                if (dsInterestProf.IsValidDataColumn() == true)
                {
                    hdnIPEffDate.Value = string.Empty;
                    CGIntrestProfile.GridRowStyleCSS = "normalrow";
                    CGIntrestProfile.GridHeaderRowCSS = "pl10 pr10 gridheader";
                    CGIntrestProfile.GridAlternatingRowCSS = "altrow";
                    CGIntrestProfile.GridViewPaging.ShowHeader = true;
                    CGIntrestProfile.GridAllowPaging = false;
                    CGIntrestProfile.AllowLinks = new int[] { 1 };
                    CGIntrestProfile.DataKeyNames = new string[] { "IEFFDATE" };
                    //CGIntrestProfile.GridWidth = Unit.Percentage(100);
                    CGIntrestProfile.DeleteMode = Control.CustomGrid.ExtendGrid.Deletemode.Single;
                    CGIntrestProfile.DataSource = dsInterestProf;
                    CGIntrestProfile.BindGrid();
                    if (dsInterestProf.IsValidDataSet())
                    {
                        imgbtnIntrstPrflsDel.Enabled = true;
                        imgbtnIntrstPrflsCopy.Enabled = true;
                    }
                }
            }
        }

        //private void ShowMessage()
        //{
        //    string strMsg = string.Format("This Will Remove the Highlighted Interest Profile");
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "confirm", "ValidateRate1('" + strMsg + "');", true);
        //}

        protected void IPImgbtnDelete_Click(object sender, ImageClickEventArgs e)
        {

            if (string.IsNullOrEmpty(hdnIPEffDate.Value) == false)
            {
                this.EffDate = Convert.ToString(hdnIPEffDate.Value);
                //ShowMessage();
                this.noteInfoPresenter.DeleteIntrestProfile();
                GetIntrestProfileGrid();
                ErrorSuccess.Visible = false;
                lblErrorSuccess.Text = "Record deleted successfully.";
            }
            else
            {
                ErrorFail.Visible = false;
                lblErrorFail.Text = "Please Select a Row to delete.";
            }
        }

        protected void imgbtEqtyPrflsDel_Click(object sender, ImageClickEventArgs e)
        {
            if (string.IsNullOrEmpty(hdnEPEffDate.Value) == false)
            {
                this.EffDate = Convert.ToString(hdnEPEffDate.Value);
                this.EQtype = Convert.ToString(hdnEPEQtype.Value);
                this.BudgetID = Convert.ToString(hdnEPBudgetID.Value);
                this.noteInfoPresenter.DeleteEquityProfile();
                GetEquityProfileGrid();
            }
        }

        protected void CGIntrestProfile_GridCheckedChange(object sender, ImageClickEventArgs e)
        {
            try
            {
                GridViewRow gvRow = CGIntrestProfile.SelectedRow;
                ImageButton SelectUnSelect = (ImageButton)gvRow.FindControl("imgRowSelect");
                if (SelectUnSelect.AlternateText == "1")
                {
                    hdnIPEffDate.Value = TCLHelper.GetData(((LinkButton)gvRow.Cells[2].Controls[0]).Text);
                }
                else
                {
                    hdnIPEffDate.Value = string.Empty;
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, Level.Error);
                throw;
            }
        }

        protected void CGEquityProfile_GridCheckedChange(object sender, ImageClickEventArgs e)
        {
            try
            {
                GridViewRow gvRow = CGEquityProfile.SelectedRow;
                ImageButton SelectUnSelect = (ImageButton)gvRow.FindControl("imgRowSelect");
                if (SelectUnSelect.AlternateText == "1")
                {
                    hdnEPEffDate.Value = TCLHelper.GetData(((LinkButton)gvRow.Cells[5].Controls[0]).Text);
                    hdnEPEQtype.Value = TCLHelper.GetData(gvRow.Cells[4].Text);
                    hdnEPBudgetID.Value = TCLHelper.GetData(gvRow.Cells[3].Text);
                }
                else
                {
                    hdnEPEffDate.Value = string.Empty;
                    hdnEPEQtype.Value = string.Empty;
                    hdnEPBudgetID.Value = string.Empty;
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, Level.Error);
                throw;
            }
        }

        protected void chkORComRule_Changed(object sender, EventArgs e)
        {
            mblnComRuleOverride = chkORComRule.Checked;
            if (!mblnComRuleNoUpdate)
            {
                if (chkORComRule.Tag != Convert.ToString(chkORComRule.Checked))
                    mblnComRuleChanged = true;
                if (this.noteInfoPresenter.ComRuleValidate())
                {

                }
            }
            this.noteInfoPresenter.ComRulesApply(); // Use apply to Set it
        }

        protected void btnFincTabPostFee_Click(object sender, EventArgs e)
        {
            string glCode = ddlGLTable.GetSelectedValue();
            if (string.IsNullOrEmpty(glCode) == false)
            {
                //frmFeePost.Show(vbModal);                    
                return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(upanel, upanel.GetType(), "confirm", "alert('Please select a GL Table.  Required for transaction codes in fee posting');", true);
                return;
            }
        }

        public void NoteinfoLoad(DataSet dsLoad)
        {
            try
            {	// On Error GoTo SetupLoadError
                bool blnMstrLOCFound;
                mblnInitLoad = true;
                blnMstrLOCFound = false;
                mblnFirstTime = true;
                MasterSubLOC MasterSubLOC = new MasterSubLOC();
                NoteInfoType NoteInfoType = new NoteInfoType();
                using (DataSet ds = this.noteInfoPresenter.BorrowerCheck())
                {
                    if (ds.IsValidDataSet() == false)
                    {
                        ScriptManager.RegisterStartupScript(upanel, upanel.GetType(), "alert", "alert('Borrower not found: '" + this.BorrowerNo, true);
                        btnProjectSave.Visible = false;
                        return;
                    }

                    this.bLOCIntProf = false;
                    bLOCIntProfAdd = false;
                    sSubLookupIntProf = "";
                    this.dblMasterLookupIntProf = "0";
                    this.bLOCEqtProf = false;
                    this.bLOCEqtProfAdd = false;

                    //start coding
                    if (this.Action == 1)
                    {
                        if (MasterSubLOC.LOCType == "P")
                        {
                            mintLTOBLOC = 1;
                        }
                        else if (MasterSubLOC.LOCType == "C")
                        {
                            mintLTOBLOC = 2;
                        }
                        else
                        {
                            mintLTOBLOC = 0;
                        }
                        this.ParentNo = TCLHelper.GetData(MasterSubLOC.ParentNO);
                    }
                    else
                    {
                        this.mintLTOBLOC = TCLHelper.ConvertToInt(TCLHelper.GetData(ds.Tables[0].Rows[0]["LTOBLOCTYPE"]));
                        this.ParentNo = TCLHelper.GetData(ds.Tables[0].Rows[0]["PARENTNO"]);
                    }
                }
                if (TCLHelper.GetData(this.ParentNo) == String.Empty && (mintLTOBLOC == 2 | MasterSubLOC.LOCType == "C"))
                {
                    if (this.ParentNo != TCLHelper.GetData(MasterSubLOC.ParentNO))
                    {
                        this.ParentNo = TCLHelper.GetData(MasterSubLOC.ParentNO);
                        if (MasterSubLOC.LOCType == "P")
                        {
                            mintLTOBLOC = 1;
                        }
                        else if (MasterSubLOC.LOCType == "C")
                        {
                            mintLTOBLOC = 2;
                        }
                        else
                        {
                            mintLTOBLOC = 0;
                        }
                    }
                }

                if (dsLoad.IsValidDataSet())
                {
                    this.mcurOrig_Commit = TCLHelper.ConvertToDouble(TCLHelper.GetData(dsLoad.Tables[0].Rows[0]["PCLAMT"]));
                    this.mblnComRuleOverride = TCLHelper.ConvertToTCLBool(TCLHelper.GetData(dsLoad.Tables[0].Rows[0]["RuleOverrideFlag"]));
                    this.mdblCommitmentRule = TCLHelper.ConvertToDouble(TCLHelper.GetData(dsLoad.Tables[0].Rows[0]["CommitmentRuleID"]));
                    this.nRenewalCount = TCLHelper.ConvertToInt(TCLHelper.GetData(dsLoad.Tables[0].Rows[0]["PRENEW"]));
                    this.sSubLookupIntProf = TCLHelper.GetData(dsLoad.Tables[0].Rows[0]["PLCSEQ"]); // save this one so we know if it changed
                    this.dblMasterLookupIntProf = TCLHelper.GetData(dsLoad.Tables[0].Rows[0]["LOCMID"]);
                    NoteInfoType.NoteFlag = TCLHelper.GetData(dsLoad.Tables[0].Rows[0]["PMFLAG"]); // set the note flag so we can disable some stuff

                    //FoundLOCIntProf(TCLHelper.GetData(adoSetupModTable("PBNUMBER")), dblMasterLookupIntProf, sSubLookupIntProf, pubGetDateLocalized, bLOCIntProfAdd);
                    this.bLOCIntProf = this.noteInfoPresenter.FoundLocIntProf(TCLHelper.GetData(dsLoad.Tables[0].Rows[0]["PBNUMBER"]), TCLHelper.ConvertToDouble(this.dblMasterLookupIntProf), sSubLookupIntProf, TCLHelper.ConvertToBool(this.bLOCIntProfAdd));

                    //FoundLOCEqtProf(TCLHelper.GetData(adoSetupModTable("PBNUMBER")), dblMasterLookupIntProf, sSubLookupIntProf, pubGetDateLocalized, bLOCEqtProfAdd);
                    this.bLOCEqtProf = this.noteInfoPresenter.FoundLocEqtProf(TCLHelper.GetData(dsLoad.Tables[0].Rows[0]["PBNUMBER"]), TCLHelper.ConvertToDouble(this.dblMasterLookupIntProf), sSubLookupIntProf, TCLHelper.ConvertToBool(this.bLOCEqtProfAdd));

                    if (blnMstrLOCFound == false)
                    {
                        if (TCLHelper.ConvertToInt(dsLoad.Tables[0].Rows[0]["LOCMID"]) > 0)
                        {
                            ScriptManager.RegisterStartupScript(upanel, upanel.GetType(), "alert", "alert('Master LOC attached to this note is no longer available, Problem with Master LOC')", true);
                            this.mblnLOCProblemOnLoad = true;
                            ddlMLoc.SelectedIndex = 0; // Set it to NONE, Not 'Not Attached'
                        }
                        else
                        {
                            ddlMLoc.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        if (ddlMLoc.Items.Count > 0)
                        {
                            if (ddlSLoc.SelectedIndex < 0)
                            {
                                if (TCLHelper.GetData(dsLoad.Tables[0].Rows[0]["PLCSEQ"]) != string.Empty)
                                {
                                    ScriptManager.RegisterStartupScript(upanel, upanel.GetType(), "alert", "alert('Sub LOC attached to this note is no longer available, Problem with Sub LOC')", true);
                                    this.mblnLOCProblemOnLoad = true;
                                    ddlSLoc.SelectedIndex = 0; // Set it to NONE, Not 'Not Attached'
                                }
                            }
                        }
                        else
                        {
                            if (TCLHelper.GetData(dsLoad.Tables[0].Rows[0]["PLCSEQ"]) != "")
                            {
                                ScriptManager.RegisterStartupScript(upanel, upanel.GetType(), "alert", "alert('Sub LOC attached to this note is no longer available, Problem with Sub LOC')", true);
                                this.mblnLOCProblemOnLoad = true;
                                ddlSLoc.SelectedIndex = 0; // Set it to NONE, Not 'Not Attached'
                            }
                        }
                    }
                    this.bRenewal = 0;
                    this.nRenewalCount = TCLHelper.ConvertToInt(TCLHelper.GetData(dsLoad.Tables[0].Rows[0]["PRENEW"]));
                }
            }
            catch
            {	// SetupLoadError:

            }
        }

        protected void csKeyContacts_GridCheckedChange(object sender, ImageClickEventArgs e)
        {
            if (TCLHelper.GetData(ViewState["KeyContEdit"]).Equals(keyContEditFalse, StringComparison.OrdinalIgnoreCase) == true)
                return;
            GridViewRow gvRow = csKeyContacts.SelectedRow;
            ImageButton imgbtnSelect = (ImageButton)gvRow.FindControl("imgRowSelect");
            if (imgbtnSelect.AlternateText == "1")
                btnKeyCntsMoveRight.Enabled = true;
            else
                btnKeyCntsMoveRight.Enabled = false;

        }

        protected void csKeyContactAttached_GridCheckedChange(object sender, ImageClickEventArgs e)
        {
            if (TCLHelper.GetData(ViewState["KeyContEdit"]).Equals(keyContEditFalse, StringComparison.OrdinalIgnoreCase) == true)
                return;
            GridViewRow gvRow = csKeyContactAttached.SelectedRow;
            ImageButton imgbtnSelect = (ImageButton)gvRow.FindControl("imgRowSelect");
            if (imgbtnSelect.AlternateText == "1")
                imgbtnDKeyContact.Visible = true;
            else
                imgbtnDKeyContact.Visible = false;

        }

        #endregion

        #region Gokul(General,NotePayOff,KeyContacts,BudgetControl)

        private void BindGeneral()
        {

            try
            {
                using (DataSet dsGeneral = noteInfoPresenter.GetNotesGeneral(this.BorrowerNo))
                {
                    //Bind Company
                    if (dsGeneral.Tables.Count > 0)
                    {
                        ddlCompany.DataSource = dsGeneral.Tables[0];
                        ddlCompany.DataValueField = "ID";
                        ddlCompany.DataTextField = "NAME";
                        ddlCompany.DataBind();
                    }

                    //Bind Area
                    if (dsGeneral.Tables.Count > 1)
                    {
                        ddlArea.DataSource = dsGeneral.Tables[1];
                        ddlArea.DataValueField = "ID";
                        ddlArea.DataTextField = "NAME";
                        ddlArea.DataBind();
                    }

                    // Bind M-Class
                    if (dsGeneral.Tables.Count > 2)
                    {
                        ddlMClass.DataSource = dsGeneral.Tables[2];
                        ddlMClass.DataValueField = "PRJMCLASS";
                        ddlMClass.DataTextField = "PRJDESC";
                        ddlMClass.DataBind();
                    }

                    //Bind GL Table
                    if (dsGeneral.Tables.Count > 3)
                    {
                        ddlGLTable.DataSource = dsGeneral.Tables[3];
                        ddlGLTable.DataValueField = "TABLE1";
                        ddlGLTable.DataTextField = "TABLE1";
                        ddlGLTable.DataBind();
                    }

                    //Bind Status 
                    if (dsGeneral.Tables.Count > 4)
                    {

                        ddlStatus.DataSource = dsGeneral.Tables[4];
                        ddlStatus.DataValueField = "CODE";
                        ddlStatus.DataTextField = "DFTVAL";
                        ddlStatus.DataBind();
                    }

                    //Federal Code Status 
                    if (dsGeneral.Tables.Count > 5)
                    {
                        ddlFederalCode.DataSource = dsGeneral.Tables[5];
                        ddlFederalCode.DataValueField = "CODE";
                        ddlFederalCode.DataTextField = "DFTVAL";
                        ddlFederalCode.DataBind();
                    }

                    //Loan Grade
                    if (dsGeneral.Tables.Count > 6)
                    {
                        ddlLoanGrade.DataSource = dsGeneral.Tables[6];
                        ddlLoanGrade.DataValueField = "CODE";
                        ddlLoanGrade.DataTextField = "DFTVAL";
                        ddlLoanGrade.DataBind();
                    }

                    // Loan Class
                    if (dsGeneral.Tables.Count > 7)
                    {
                        ddlLoanClass.DataSource = dsGeneral.Tables[7];
                        ddlLoanClass.DataValueField = "CODE";
                        ddlLoanClass.DataTextField = "DFTVAL";
                        ddlLoanClass.DataBind();
                    }

                    // Loan Type
                    if (dsGeneral.Tables.Count > 8)
                    {
                        ddlLoanType.DataSource = dsGeneral.Tables[8];
                        ddlLoanType.DataValueField = "CODE";
                        ddlLoanType.DataTextField = "DFTVAL";
                        ddlLoanType.DataBind();
                    }

                    // Loan Purpose
                    if (dsGeneral.Tables.Count > 9)
                    {
                        ddlLoanPurpose.DataSource = dsGeneral.Tables[9];
                        ddlLoanPurpose.DataValueField = "CODE";
                        ddlLoanPurpose.DataTextField = "DFTVAL";
                        ddlLoanPurpose.DataBind();
                    }

                    // Collateral Code
                    if (dsGeneral.Tables.Count > 10)
                    {
                        ddlCollateralCode.DataSource = dsGeneral.Tables[10];
                        ddlCollateralCode.DataValueField = "CODE";
                        ddlCollateralCode.DataTextField = "DFTVAL";
                        ddlCollateralCode.DataBind();
                    }

                    // Master LOC
                    if (dsGeneral.Tables.Count > 11)
                    {
                        ddlMLoc.DataSource = dsGeneral.Tables[11];
                        ddlMLoc.DataValueField = "LOCMID";
                        ddlMLoc.DataTextField = "LCDESC";
                        ddlMLoc.DataBind();
                    }

                    //Budgets
                    if (dsGeneral.Tables.Count > 12)
                    {
                        ddlBudgets.DataSource = dsGeneral.Tables[12];
                        ddlBudgets.DataValueField = "STDID";
                        ddlBudgets.DataTextField = "STDDESC";
                        ddlBudgets.DataBind();

                    }

                    //Inspection Company
                    if (dsGeneral.Tables.Count > 13)
                    {
                        ddlInspCompany.DataSource = dsGeneral.Tables[13];
                        ddlInspCompany.DataValueField = "COMPANYID";
                        ddlInspCompany.DataTextField = "COMPANYNAME";
                        ddlInspCompany.DataBind();
                    }

                    //Key Contacts
                    if (dsGeneral.Tables.Count > 14)
                    {
                        ddlKeyContacts.DataSource = dsGeneral.Tables[14];
                        ddlKeyContacts.DataValueField = "BCHMCLASS";
                        ddlKeyContacts.DataTextField = "BCHDESC";
                        ddlKeyContacts.DataBind();
                    }

                    if (ddlKeyContacts.SelectedIndex > -1)
                    {
                        this.BindKeyContacts();
                        this.BindAttachedKeyContacts("", "", "");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, Level.Error);
            }
        }

        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlCompany.SelectedIndex > 0)
                {
                    BindBranch();
                }
                else if (ddlCompany.SelectedIndex == 0)
                {
                    ddlBranch.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, Level.Error);
            }
        }

        private void BindBranch()
        {
            try
            {
                noteInfoPresenter = new NoteInfoPresenter(this);
                DataTable dtBranch = noteInfoPresenter.GetBranch(ddlCompany.SelectedValue);
                ddlBranch.Items.Clear();
                ddlBranch.DataSource = dtBranch;
                ddlBranch.DataTextField = "NAME";
                ddlBranch.DataValueField = "ID";
                ddlBranch.DataBind();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, Level.Error);
            }
        }

        private void BindLocale()
        {
            try
            {
                noteInfoPresenter = new NoteInfoPresenter(this);
                DataTable dtLocale = noteInfoPresenter.GetLocale(ddlArea.SelectedValue);
                ddlLocal.DataSource = dtLocale;
                ddlLocal.DataTextField = "NAME";
                ddlLocal.DataValueField = "ID";
                ddlLocal.DataBind();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, Level.Error);
            }
        }

        private void BindLClass()
        {
            try
            {
                noteInfoPresenter = new NoteInfoPresenter(this);
                DataTable dtSClass = noteInfoPresenter.GetSClass(ddlMClass.SelectedValue);
                ddlSClass.DataSource = dtSClass;
                ddlSClass.DataTextField = "NAME";
                ddlSClass.DataValueField = "ID";
                ddlSClass.DataBind();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, Level.Error);
            }
        }

        private void BindSLoc()
        {
            try
            {
                noteInfoPresenter = new NoteInfoPresenter(this);
                DataTable dtSClass = noteInfoPresenter.GetSLoc(TCLHelper.ConvertToDouble(ddlMLoc.SelectedValue).ToString());
                ddlSLoc.DataSource = dtSClass;
                ddlSLoc.DataTextField = "LCDESC";
                ddlSLoc.DataValueField = "LCSEQ";
                ddlSLoc.DataBind();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, Level.Error);
            }
        }

        protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlArea.SelectedIndex > 0)
                {
                    BindLocale();
                }
                else if (ddlArea.SelectedIndex == 0)
                {
                    ddlLocal.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, Level.Error);
            }
        }

        protected void ddlMClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlMClass.SelectedIndex > 0)
                {
                    BindLClass();
                }
                else if (ddlMClass.SelectedIndex == 0)
                {
                    ddlSClass.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, Level.Error);
            }
        }

        protected void ddlMLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlMLoc.SelectedIndex > 0)
                {
                    BindSLoc();
                }
                else if (ddlMLoc.SelectedIndex == 0)
                {
                    ddlSLoc.Items.Clear();
                    ddlSLoc.Items.Insert(0, new ListItem("Not Attached", ""));
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, Level.Error);
            }
        }

        private void GetGeneralNotes()
        {
            try
            {
                bool boolNoteMode = false;
                if (this.NoteMode.ToString().ToUpper() != "Pipeline".ToString().ToUpper())
                    boolNoteMode = true;

                GetProjectDetails = noteInfoPresenter.GetGeneralNotes(NoteNumber, UnitNumber, this.BorrowerNo, boolNoteMode);

                if (ddlCompany.Items.FindByValue(GetProjectDetails.Company) != null)
                {
                    ddlCompany.SelectedValue = GetProjectDetails.Company;
                    if (ddlCompany.SelectedIndex > 0)
                        BindBranch();
                }

                if (ddlBranch.Items.FindByValue(GetProjectDetails.Branch) != null)
                    ddlBranch.SelectedValue = GetProjectDetails.Branch;

                if (ddlArea.Items.FindByValue(GetProjectDetails.Area) != null)
                {
                    ddlArea.SelectedValue = GetProjectDetails.Area;
                    if (ddlArea.SelectedIndex > 0)
                        BindLocale();
                }

                if (ddlLocal.Items.FindByValue(GetProjectDetails.Locale) != null)
                    ddlLocal.SelectedValue = GetProjectDetails.Locale;

                if (ddlMClass.Items.FindByValue(GetProjectDetails.MClass) != null)
                {
                    ddlMClass.SelectedValue = GetProjectDetails.MClass;
                    if (ddlMClass.SelectedIndex > 0)
                        BindLClass();
                }

                if (ddlSClass.Items.FindByValue(GetProjectDetails.SClass) != null)
                    ddlSClass.SelectedValue = GetProjectDetails.SClass;

                if (ddlMLoc.Items.FindByValue(GetProjectDetails.MLoc) != null)
                    ddlMLoc.SelectedValue = GetProjectDetails.MLoc;

                if (ddlMLoc.SelectedIndex == 0)
                    ddlSLoc.Items.Insert(0, new ListItem("Not Attached", ""));
                else
                    BindSLoc();

                if (ddlSLoc.Items.FindByValue(GetProjectDetails.SLoc) != null)
                    ddlSLoc.SelectedValue = GetProjectDetails.SLoc;

                chkRevolving.Checked = GetProjectDetails.Revolving == "Y" ? true : false;
                chkFannieMae.Checked = GetProjectDetails.FannieMae == 0 ? false : true;

                if (chkRevolving.Checked)
                {
                    chkFannieMae.Checked = false;
                    chkFannieMae.Enabled = false;
                }
                if (chkFannieMae.Checked)
                {
                    chkRevolving.Checked = false;
                    chkRevolving.Enabled = false;
                }

                chkNonAccural.Checked = GetProjectDetails.NonAccural == 0 ? false : true;
                chkInDefault.Checked = GetProjectDetails.InDefault == 0 ? false : true;
                chkStopFin.Checked = GetProjectDetails.StopFinActivity == 0 ? false : true;
                chkForeclosure.Checked = GetProjectDetails.Foreclosure == 0 ? false : true;
                chkBankruptcy.Checked = GetProjectDetails.Bankruptcy == 0 ? false : true;
                chk203K.Checked = GetProjectDetails.TwoNotthreeK == 0 ? false : true;
                chkRollToPerm.Checked = GetProjectDetails.RollToPerm == 0 ? false : true;
                chkAssetManagementt.Checked = GetProjectDetails.AssetManagement == 0 ? false : true;
                chkAmortizeLoan.Checked = GetProjectDetails.AmortizeLoan == 0 ? false : true;
                if (chkAmortizeLoan.Checked == true)
                {
                    chkAutomatic.Checked = GetProjectDetails.AutomaticRecast == 0 ? false : true;
                    chkOriginal.Checked = GetProjectDetails.OriginalTerms == 0 ? false : true;
                }
                else
                {
                    chkAutomatic.Checked = false;
                    chkOriginal.Checked = false;
                    chkAutomatic.Enabled = false;
                    chkOriginal.Enabled = false;
                }
                ddlGLTable.SetSelectedText(GetProjectDetails.GLTable);
                ddlStatus.SetSelectedText(GetProjectDetails.Status);
                ddlFederalCode.SetSelectedText(GetProjectDetails.FederalCode);
                ddlLoanGrade.SetSelectedText(GetProjectDetails.LoanGrade);
                ddlLoanClass.SetSelectedText(GetProjectDetails.LoanClass);
                ddlLoanType.SetSelectedText(GetProjectDetails.LoanType);
                ddlLoanPurpose.SetSelectedText(GetProjectDetails.LoanPurpose);
                ddlCollateralCode.SetSelectedText(GetProjectDetails.CollateralCode);

                txtLoanGrdDate.Text = Convert.ToString(GetProjectDetails.LoanGradeDate);
                txtCensusTest.Text = Convert.ToString(GetProjectDetails.CensusTest);
                txtCommitment.Text = Convert.ToString(GetProjectDetails.Commitment);

                txtBudgetCommitment.Text = Convert.ToString(GetProjectDetails.BudgetCommitment);
                txtTotalEstimated.Text = Convert.ToString(GetProjectDetails.TotalEstimated);
                txtAlctCommit.Text = Convert.ToString(GetProjectDetails.AllocatedCommitement);
                txtTotalAllocated.Text = Convert.ToString(GetProjectDetails.TotalAllocated);
                chkOmit.Checked = GetProjectDetails.BudgetOmit == 0 ? false : true; //OmitBVOnWebInspFlag
                chkOutside.Checked = GetProjectDetails.BudgetOutSide == "Y" ? true : false; //OutsideLast

                ddlAutoGenBrwFndFirst.SetSelectedValue(GetProjectDetails.BorrowerFund);
                ddlInspCompany.SetSelectedValue(GetProjectDetails.CompanyId);
                ddlBudgets.SetSelectedValue(GetProjectDetails.BudgetId);
                if (ddlBudgets.Items.FindByValue(GetProjectDetails.BudgetId) != null)
                {
                    ddlBudgets.SelectedValue = GetProjectDetails.BudgetId;
                    hdnBudget.Value = ddlBudgets.SelectedValue;
                    BindBudgetNotes(0);
                    if (string.IsNullOrEmpty(ddlBudgets.SelectedValue) == false)
                        lblBudgetTitle.Text = Convert.ToString(GetProjectDetails.BudgetDesc);

                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, Level.Error);
            }
        }

        private void BindBudgetNotes(int intMode)
        {
            try
            {
                noteInfoPresenter = new NoteInfoPresenter(this);
                using (DataSet dsBudget = noteInfoPresenter.GetBudgetNotes(NoteNumber, UnitNumber, ddlBudgets.SelectedValue, intMode, chk203K.Checked == false ? 0 : -1))
                {
                    if (dsBudget.IsValidDataColumn() == true)
                    {

                        cgBudget.GridClientID = "cgBudget";
                        cgBudget.DataSource = dsBudget;
                        cgBudget.GridRowStyleCSS = "normalrow";
                        cgBudget.GridHeaderRowCSS = "pl10 pr10 gridheader";
                        cgBudget.GridAlternatingRowCSS = "altrow";
                        cgBudget.HideColumns = new int[] { 2, 3, 4, 5 };
                        cgBudget.DeleteMode = Control.CustomGrid.ExtendGrid.Deletemode.Single;
                        cgBudget.GridWidth = Unit.Percentage(100);
                        cgBudget.GridAllowPaging = false;
                        cgBudget.BindGrid();
                        if (dsBudget.Tables.Count > 1)
                        {
                            Int32 intGroupId = TCLHelper.ConvertToInt(dsBudget.Tables[1].Rows[0]["MAXITEM"]) + 1;
                            txtGroupID.Text = intGroupId.ToString("D2");
                        }

                        if (dsBudget.Tables.Count > 2)
                        {
                            CalcBudget(dsBudget);
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                Logger.LogException(ex, Level.Error);
            }

        }

        private void CalcBudget(DataSet dsBudget)
        {
            double dblAllocated = 0;
            double dblTotalAllocated = 0;
            if (dsBudget.Tables[2].Rows.Count > 0)
            {
                for (int intI = 0; intI < dsBudget.Tables[2].Rows.Count; intI++)
                {
                    dblAllocated = dblAllocated + TCLHelper.ConvertToDouble(dsBudget.Tables[2].Rows[intI]["BCCOST"]) + TCLHelper.ConvertToDouble(dsBudget.Tables[2].Rows[intI]["BCEQUITY"]);
                    dblTotalAllocated = dblTotalAllocated + TCLHelper.ConvertToDouble(dsBudget.Tables[2].Rows[intI]["BCCOST"]);
                }
            }

            if (dsBudget.Tables[0].Rows.Count > 0)
            {
                if (dsBudget.Tables[0].Rows.Count > 0)
                {
                    for (int intI = 0; intI < dsBudget.Tables[0].Rows.Count; intI++)
                    {
                        if (Convert.ToString(dsBudget.Tables[0].Rows[intI]["BCID"]) + Convert.ToString(dsBudget.Tables[0].Rows[intI]["BCLV1"]) + Convert.ToString(dsBudget.Tables[0].Rows[intI]["BCLV2"]) == "9999900")
                        {
                            dblAllocated = dblAllocated + TCLHelper.ConvertToDouble(Convert.ToString(txtFincTabConstAdjsmnt.Text).Replace("$", "").Replace(",", ""));
                            dblTotalAllocated = dblTotalAllocated + TCLHelper.ConvertToDouble(Convert.ToString(txtFincTabConstAdjsmnt.Text).Replace("$", "").Replace(",", ""));
                        }
                    }

                }
            }

            if (dblAllocated > 0)
                txtAlctCommit.Text = TCLHelper.ConvertToUSCurrency(dblAllocated);
            else
                txtAlctCommit.Text = "";

            if (dblTotalAllocated > 0)
                txtTotalAllocated.Text = TCLHelper.ConvertToUSCurrency(dblTotalAllocated);
            else
                txtTotalAllocated.Text = "";


        }

        protected void ImgbtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                foreach (GridViewRow gvRow in cgBudget.GridViewPaging.Rows)
                {
                    ImageButton selectButton = (ImageButton)gvRow.FindControl("imgRowSelect");
                    if (selectButton.AlternateText == "1")
                    {
                        ScriptManager.RegisterStartupScript(upanel, upanel.GetType(), "confirm", "DeleteBudget('Are you sure you want to delete the selected budget group(s) for this loan?');", true);
                        return;

                    }
                }
                ScriptManager.RegisterStartupScript(upanel, upanel.GetType(), "Alert", "alert('Budget group(s) not selected, nothing to delete!');", true);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, Level.Error);
            }
        }

        protected void btnDeleteBudget_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow gvRow in cgBudget.GridViewPaging.Rows)
                {
                    ImageButton selectButton = (ImageButton)gvRow.FindControl("imgRowSelect");
                    if (selectButton.AlternateText == "1")
                    {
                        string BCID = Convert.ToString(gvRow.Cells[1].Text);
                        if (BCID == "99")
                        {
                            ScriptManager.RegisterStartupScript(upanel, upanel.GetType(), "confirm", "alert('99 - General Draw Line Item cannot be deleted.\\n\\nContinuing to Process...?');", true);
                            return;
                        }

                        string Id = Convert.ToString(gvRow.Cells[3].Text);
                        string BCLV1 = Convert.ToString(gvRow.Cells[4].Text);
                        string BCLV2 = Convert.ToString(gvRow.Cells[5].Text);
                        string BCTYPE = Convert.ToString(gvRow.Cells[6].Text);

                        long intCalc;
                        noteInfoPresenter = new NoteInfoPresenter(this);
                        noteInfoPresenter.GetBudgetCalc(NoteNumber, UnitNumber, Id, BCLV1, out intCalc);

                        if (intCalc == 0)
                        {
                            noteInfoPresenter = new NoteInfoPresenter(this);
                            noteInfoPresenter.DeleteBudget(NoteNumber, UnitNumber, Id, BCLV1, BCLV2, BCTYPE);
                            BindBudgetNotes(0);
                            tblMsgMPopup.Visible = true;
                            trAlertSuccess.Visible = true;
                            lblSuccess.Text = "Group ID deleted sucessfully";
                            lblSuccess.Visible = true;
                            ErrorAlert1.Visible = false;
                            lblErrorAlert.Visible = false;
                            lblErrorAlert.Text = string.Empty;
                        }
                        else
                        {
                            string strMsg = string.Empty;
                            strMsg = "Budget Group(s) " + Convert.ToString(gvRow.Cells[4].Text) + " have Receipts/Disbursements or Inspections posted Cannot change budgets.";
                            ScriptManager.RegisterStartupScript(upanel, upanel.GetType(), "confirm", "alert('" + strMsg + "');", true);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, Level.Error);
            }
        }



        protected void btnSelectBudget_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToString(hdnBudgetTemp.Value) != "1")
                {


                    string strCheck = string.Empty;
                    tblMsgMPopup.Visible = false;
                    noteInfoPresenter = new NoteInfoPresenter(this);
                    using (DataSet dsBudget = noteInfoPresenter.CheckBudget(NoteNumber, UnitNumber))
                    {
                        if (TCLHelper.IsValidDataColumn(dsBudget))
                        {
                            if (dsBudget.Tables[0].Rows.Count > 0)
                            {
                                strCheck = Convert.ToString(dsBudget.Tables[0].Rows[0]["Result"]);
                            }
                        }
                    }
                    if (strCheck == "0")
                    {
                        hdnBudget.Value = ddlBudgets.SelectedValue;
                        BindBudgetNotes(1);
                        if (string.IsNullOrEmpty(ddlBudgets.SelectedValue) == false)
                        {
                            lblBudgetTitle.Text = Convert.ToString(ddlBudgets.SelectedItem.Text);
                        }
                        else
                        {
                            lblBudgetTitle.Text = "";
                        }
                    }
                    else
                    {
                        string strMsg = string.Empty;
                        if (ddlBudgets.Items.FindByValue(Convert.ToString(hdnBudget.Value)) != null)
                            ddlBudgets.SelectedValue = Convert.ToString(hdnBudget.Value);
                        strMsg = "Budget Group(s) " + strCheck + " have Receipts/Disbursements or Inspections posted Cannot change budgets.";
                        ScriptManager.RegisterStartupScript(upanel, upanel.GetType(), "confirm", "alert('" + strMsg + "');", true);
                        return;
                    }
                }
                else
                {
                    BindBudgetNotes(1);
                    hdnBudgetTemp.Value = "";
                }

            }
            catch (Exception ex)
            {
                Logger.LogException(ex, Level.Error);
            }
        }

        private void BindKeyContacts()
        {
            try
            {
                noteInfoPresenter = new NoteInfoPresenter(this);
                using (DataSet dsBudget = noteInfoPresenter.GetKeyContact(ddlKeyContacts.SelectedValue, Convert.ToString(txtFindContact.Text)))
                {
                    if (dsBudget.IsValidDataColumn())
                    {
                        csKeyContacts.GridClientID = "csKeyContacts";
                        csKeyContacts.DataSource = dsBudget;
                        csKeyContacts.GridRowStyleCSS = "normalrow";
                        csKeyContacts.GridHeaderRowCSS = "pl10 pr10 gridheader";
                        csKeyContacts.GridAlternatingRowCSS = "altrow";
                        csKeyContacts.ShowColumns = new int[] { 0, 1, 2, 3, 4 };
                        csKeyContacts.DeleteMode = Control.CustomGrid.ExtendGrid.Deletemode.Single;
                        csKeyContacts.GridWidth = Unit.Percentage(100);
                        csKeyContacts.GridAllowPaging = false;
                        csKeyContacts.BindGrid();
                        btnKeyCntsMoveRight.Enabled = false;
                        pnlKeyContact.Visible = false;
                    }
                    else
                    {
                        pnlKeyContact.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, Level.Error);
            }
        }

        protected void btnKeyCntsMoveRight_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow gv in csKeyContacts.GridViewPaging.Rows)
                {
                    ImageButton selectButton = (ImageButton)gv.FindControl("imgRowSelect");
                    if (selectButton.AlternateText == "1")
                    {
                        BindAttachedKeyContacts(Convert.ToString(gv.Cells[6].Text), Convert.ToString(gv.Cells[1].Text), Convert.ToString(gv.Cells[2].Text));
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, Level.Error);
            }
        }


        private void BindAttachedKeyContacts(string KeyContactMId, string KeyContactSId, string strDesc)
        {
            try
            {
                if (ddlKeyContacts.SelectedIndex > -1)
                {
                    noteInfoPresenter = new NoteInfoPresenter(this);
                    using (DataSet dsBudget = noteInfoPresenter.GetKeyContactAttachment(NoteNumber, UnitNumber, KeyContactMId, KeyContactSId))
                    {
                        if (dsBudget.IsValidDataColumn())
                        {
                            if (strDesc != "")
                            {
                                if (dsBudget.Tables[0].Rows.Count > 0)
                                {
                                    if (dsBudget.Tables[0].Columns.Contains("Already"))
                                    {
                                        if (Convert.ToString(dsBudget.Tables[0].Rows[0]["Already"]).ToString().ToUpper() == "Already Exists".ToUpper())
                                        {
                                            string strMsg;
                                            strMsg = Convert.ToString(KeyContactSId) + " - " + Convert.ToString(strDesc) + " has already been added!";
                                            ScriptManager.RegisterStartupScript(upanel, upanel.GetType(), "confirm", "alert('" + strMsg + "');", true);
                                            return;
                                        }

                                    }
                                    pnlKeyAttached.Visible = false;
                                }
                                else
                                {
                                    pnlKeyAttached.Visible = true;
                                }

                            }
                            csKeyContactAttached.GridClientID = "csKeyContactAttached";
                            csKeyContactAttached.DataSource = dsBudget;
                            csKeyContactAttached.GridRowStyleCSS = "normalrow";
                            csKeyContactAttached.GridHeaderRowCSS = "pl10 pr10 gridheader";
                            csKeyContactAttached.GridAlternatingRowCSS = "altrow";
                            csKeyContactAttached.ShowColumns = new int[] { 0, 1, 2, 3, 4 };
                            csKeyContactAttached.DeleteMode = Control.CustomGrid.ExtendGrid.Deletemode.Single;
                            csKeyContactAttached.GridWidth = Unit.Percentage(100);
                            csKeyContactAttached.GridAllowPaging = false;
                            csKeyContactAttached.BindGrid();
                            imgbtnDKeyContact.Visible = false;
                            if (csKeyContactAttached.GridViewPaging.Rows.Count == 0)
                                pnlKeyAttached.Visible = true;
                        }
                        else
                        {
                            pnlKeyAttached.Visible = false;
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, Level.Error);
            }
        }

        protected void ddlKeyContacts_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlKeyContacts.SelectedIndex > -1)
                {
                    txtFindContact.Text = "";
                    BindKeyContacts();
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, Level.Error);
            }
        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                BindKeyContacts();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, Level.Error);
            }
        }

        protected void imgbtnDKeyContact_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                foreach (GridViewRow gvRow in csKeyContactAttached.GridViewPaging.Rows)
                {
                    ImageButton selectButton = (ImageButton)gvRow.FindControl("imgRowSelect");
                    if (selectButton.AlternateText == "1")
                    {
                        noteInfoPresenter = new NoteInfoPresenter(this);
                        int intResult = noteInfoPresenter.DeleteKeyContactDelete(NoteNumber, UnitNumber, Convert.ToString(gvRow.Cells[1].Text));
                        BindAttachedKeyContacts("", "", "");
                        return;

                    }
                }
                ScriptManager.RegisterStartupScript(upanel, upanel.GetType(), "confirm", "alert('Please select a Attached Key Contact to Delete?');", true);
                return;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, Level.Error);
            }
        }

        private void BindNotePayRule()
        {
            try
            {

                this.ShowAll = chkPayoffShwAll.Checked ? "1" : "0";
                this.BorrowerNo = Convert.ToString(hdnBorrowerNo.Value);
                noteInfoPresenter = new NoteInfoPresenter(this);
                using (DataSet dsNoteRule = noteInfoPresenter.GetNotePayOffRules())
                {
                    if (TCLHelper.IsValidDataColumn(dsNoteRule))
                    {
                        using (DataSet dsNotePayOff = new DataSet())
                        {
                            dsNotePayOff.Tables.Add(dsNoteRule.Tables[0].Copy());
                            cgNotePayOffRules.GridClientID = "cgNotePayOffRules";
                            cgNotePayOffRules.DataSource = dsNotePayOff;
                            cgNotePayOffRules.GridRowStyleCSS = "normalrow";
                            cgNotePayOffRules.GridHeaderRowCSS = "pl10 pr10 gridheader";
                            cgNotePayOffRules.GridAlternatingRowCSS = "altrow";
                            cgNotePayOffRules.ShowColumns = new int[] { 1, 0, 2 };
                            cgNotePayOffRules.DeleteMode = Control.CustomGrid.ExtendGrid.Deletemode.Multiple;
                            cgNotePayOffRules.GridWidth = Unit.Percentage(100);
                            cgNotePayOffRules.GridAllowPaging = false;
                            cgNotePayOffRules.BindGrid();
                        }

                        if (dsNoteRule.Tables.Count > 1)
                        {
                            using (DataSet dsAttachedPayOff = new DataSet())
                            {
                                dsAttachedPayOff.Tables.Add(dsNoteRule.Tables[1].Copy());
                                BindNotePayOffAttach(dsAttachedPayOff);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, Level.Error);
            }
        }

        protected void btnPayoffMoveRight_Click(object sender, EventArgs e)
        {
            try
            {
                bool bFlag = false;
                tblNotePayOff.Visible = false;
                this.ShowAll = chkPayoffShwAll.Checked ? "1" : "0";
                this.BorrowerNo = Convert.ToString(hdnBorrowerNo.Value);
                noteInfoPresenter = new NoteInfoPresenter(this);
                StringBuilder strAttachError = new StringBuilder();

                foreach (GridViewRow gvRows in cgNotePayOffRules.GridViewPaging.Rows)
                {
                    CheckBox chkChecked = (CheckBox)gvRows.FindControl("chkMultipleSelect");
                    if (chkChecked.Checked)
                    {
                        bFlag = true;
                        this.RuleID = Convert.ToString(gvRows.Cells[1].Text);

                        string strError = string.Empty;
                        strError = "Loan Number: " + this.NoteNumber + " " + this.UnitNumber + "\\r\\n \\r\\n Rule: " + Convert.ToString(gvRows.Cells[1].Text) + "\\r\\n         " + Convert.ToString(gvRows.Cells[3].Text + "\\r\\n \\r\\n");

                        using (DataSet dsPayOff = noteInfoPresenter.GetNotePayOffRulesById())
                        {
                            if (dsPayOff.Tables.Count > 0 || dsPayOff != null)
                            {
                                if (dsPayOff.Tables[0].Rows.Count > 0)
                                {
                                    DataView dvPayOff = (DataView)dsPayOff.Tables[0].DefaultView;
                                    dvPayOff.RowFilter = "RULEID = '" + Convert.ToString(gvRows.Cells[1].Text) + "'";
                                    if (dvPayOff.Count == 0)
                                    {
                                        this.RuleType = Convert.ToString(gvRows.Cells[2].Text);
                                        this.RuleID = Convert.ToString(gvRows.Cells[1].Text);
                                        dvPayOff.RowFilter = "RULETYPE = '" + Convert.ToString(gvRows.Cells[2].Text) + "'";

                                        if (dvPayOff.Count == 0)
                                        {
                                            noteInfoPresenter.GetNotePayOffRules();
                                        }
                                        else
                                        {
                                            switch (Convert.ToString(gvRows.Cells[2].Text))
                                            {
                                                case "P":
                                                    // PRINC     MAX: 1, 1 or More Already
                                                    strAttachError.Append("alert('" + strError + "- Principal - Maximum rules per loan is 1.');");
                                                    break;
                                                case "F":
                                                    // FEE       MAX: 3
                                                    if (dvPayOff.Count >= 3)
                                                        strAttachError.Append("alert('" + strError + " - Fee - Maximum rules per loan is 3.');");
                                                    else
                                                        noteInfoPresenter.GetNotePayOffRules();
                                                    break;
                                                case "O":
                                                    // OTHER MAX: 2
                                                    if (dvPayOff.Count >= 2)
                                                        strAttachError.Append("alert('" + strError + " - Other - Maximum rules per loan is 2.');");
                                                    else
                                                        noteInfoPresenter.GetNotePayOffRules();
                                                    break;
                                                case "B":
                                                    // BB Col    MAX 1                   Col Rule Not Allowed @ NOTE Level
                                                    strAttachError.Append("alert('" + strError + " - Borrowing Base Collateral Rules Not Allowed At Note Level.');");
                                                    break;
                                                default:
                                                    noteInfoPresenter.GetNotePayOffRules();
                                                    break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        strAttachError.Append("alert('" + strError + " - Rule Already Attached');");
                                    }

                                }
                                else
                                {
                                    noteInfoPresenter.GetNotePayOffRules();
                                }
                            }
                        }
                    }

                }
                if (!bFlag)
                {
                    ScriptManager.RegisterStartupScript(upanel, upanel.GetType(), "confirm", "alert('No Rule(s) selected to attach.');", true);
                }
                this.RuleID = "";
                BindNotePayRule();
                if (strAttachError.ToString().Trim() != "")
                    ScriptManager.RegisterStartupScript(upanel, upanel.GetType(), "confirm", strAttachError.ToString(), true);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, Level.Error);
            }
        }

        private void BindNotePayOffAttach(DataSet dsNotePayOff)
        {
            try
            {
                cgNotPayOffRulsAttach.GridClientID = "cgNotPayOffRulsAttach";
                cgNotPayOffRulsAttach.DataSource = dsNotePayOff;
                cgNotPayOffRulsAttach.GridRowStyleCSS = "normalrow";
                cgNotPayOffRulsAttach.GridHeaderRowCSS = "pl10 pr10 gridheader";
                cgNotPayOffRulsAttach.GridAlternatingRowCSS = "altrow";
                cgNotPayOffRulsAttach.ShowColumns = new int[] { 1, 0, 2 };
                cgNotPayOffRulsAttach.DeleteMode = Control.CustomGrid.ExtendGrid.Deletemode.Multiple;
                cgNotPayOffRulsAttach.GridWidth = Unit.Percentage(100);
                cgNotPayOffRulsAttach.GridAllowPaging = false;
                cgNotPayOffRulsAttach.BindGrid();
                hdnNotePayOffCount.Value = Convert.ToString(dsNotePayOff.Tables[0].Rows.Count);
                if (TCLHelper.ConvertToBool(ViewState["NotePayOff"]))
                {
                    imgbtnNtPayoffRulsDelte.Enabled = false;
                    imgbtnNotePayOffEdit.Enabled = false;
                    if (TCLHelper.IsValidDataSet(dsNotePayOff))
                    {
                        if (dsNotePayOff.Tables[0].Rows.Count > 0)
                        {
                            imgbtnNtPayoffRulsDelte.Enabled = true;
                            imgbtnNotePayOffEdit.Enabled = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, Level.Error);
            }
        }

        protected void chkPayoffShwAll_Checked(object sender, EventArgs e)
        {
            BindNotePayRule();

        }

        protected void imgbtnNtPayoffRulsDelte_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                foreach (GridViewRow gvRow in cgNotPayOffRulsAttach.GridViewPaging.Rows)
                {
                    CheckBox chkChecked = (CheckBox)gvRow.FindControl("chkMultipleSelect");
                    if (chkChecked.Checked)
                    {
                        ScriptManager.RegisterStartupScript(upanel, upanel.GetType(), "confirm", "DeleteNotePayOff('Are you sure you want to remove selected Payoff Rule(s)?');", true);
                        return;
                    }
                }
                if (TCLHelper.ConvertToInt(hdnNotePayOffCount.Value) > 0)
                {
                    //lblPayOffError.Text = "No Rule(s) selected to remove.";
                    ScriptManager.RegisterStartupScript(upanel, upanel.GetType(), "alter", "alert('No Rule(s) selected to remove.');", true);
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, Level.Error);
            }
        }

        protected void btnNotePayOff_Click(object sender, EventArgs e)
        {
            BindNotePayRule();
        }

        protected void btnNPODelete_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow gvRows in cgNotPayOffRulsAttach.GridViewPaging.Rows)
                {
                    CheckBox chkChecked = (CheckBox)gvRows.FindControl("chkMultipleSelect");
                    if (chkChecked.Checked)
                    {
                        this.IsDelete = "1";
                        RuleID = Convert.ToString(gvRows.Cells[1].Text);
                        RuleType = Convert.ToString(gvRows.Cells[2].Text);
                        noteInfoPresenter = new NoteInfoPresenter(this);
                        noteInfoPresenter.GetNotePayOffRules();
                    }

                }

                if (this.IsDelete == "1")
                {
                    tblNotePayOff.Visible = true;
                    trSuccess.Visible = true;
                    lblPayOffSuccess.Text = "Group ID deleted sucessfully";
                    lblPayOffSuccess.Visible = true;
                    trError.Visible = false;
                    lblPayOffError.Visible = false;
                    lblPayOffError.Text = string.Empty;
                }

                this.IsDelete = "0";
                this.RuleID = "";
                this.RuleType = "";
                BindNotePayRule();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, Level.Error);
            }

        }

        protected void imgbtnEdit_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                foreach (GridViewRow gvRow in cgBudget.GridViewPaging.Rows)
                {
                    ImageButton selectButton = (ImageButton)gvRow.FindControl("imgRowSelect");
                    if (selectButton.AlternateText == "1")
                    {
                        sessionProvider.Set("BCLV1", Convert.ToString(gvRow.Cells[1].Text));
                        sessionProvider.Set("BdgGroupNote", Convert.ToString(gvRow.Cells[2].Text));
                        sessionProvider.Set("BdgtitleNote", Convert.ToString(ddlBudgets.SelectedItem.Text));
                        ScriptManager.RegisterStartupScript(upanel, upanel.GetType(), "confirm", "WBOpen();", true);
                        return;

                    }
                }


                ScriptManager.RegisterStartupScript(upanel, upanel.GetType(), "Alert", "alert('Please select a row to edit');", true);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, Level.Error);
            }
        }

        protected void btnEditBudget_Click(object sender, EventArgs e)
        {
            try
            {
                BindBudgetNotes(0);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, Level.Error);
            }
        }
        protected void btnIPTemp_Click(object sender, EventArgs e)
        {
            try
            {
                GetIntrestProfileGrid();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, Level.Error);
            }
        }
        protected void btnEPTemp_Click(object sender, EventArgs e)
        {
            try
            {
                GetEquityProfileGrid();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, Level.Error);
            }
        }

        protected void btnAddBudget_Click(object sender, EventArgs e)
        {
            try
            {

                noteInfoPresenter = new NoteInfoPresenter(this);
                bool IsExists;
                noteInfoPresenter.SaveBudget(NoteNumber, UnitNumber, Convert.ToString(ddlBudgets.SelectedValue), Convert.ToString(txtGroupID.Text), Convert.ToString(txtGroupDesc.Text), out IsExists);
                if (IsExists)
                {
                    ScriptManager.RegisterStartupScript(upanel, upanel.GetType(), "confirm", "alert('Group ID already exists, cannot be added.');", true);
                    mpeAddBudget.Show();
                    return;
                }
                tblMsgMPopup.Visible = false;
                BindBudgetNotes(0);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, Level.Error);
            }

        }

        protected void imgbtnNotePayOffEdit_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Int32 intMultiSelect = 0;
                string ruleId = string.Empty;
                foreach (GridViewRow gvRow in cgNotPayOffRulsAttach.GridViewPaging.Rows)
                {
                    CheckBox chkChecked = (CheckBox)gvRow.FindControl("chkMultipleSelect");
                    if (chkChecked.Checked)
                    {
                        intMultiSelect += 1;
                        ruleId = TCLHelper.RemoveHtmlWhiteSpace(gvRow.Cells[1].Text);
                    }
                }

                switch (intMultiSelect)
                {
                    case 0:
                        ScriptManager.RegisterStartupScript(upanel, upanel.GetType(), "Alert", "alert('Please select a row to edit.');", true);
                        break;
                    case 1:
                        ScriptManager.RegisterStartupScript(upanel, upanel.GetType(), "Open", "wNotePayOffOpen('" + ruleId + "','1','NoteEdit');", true);
                        break;
                    default:
                        ScriptManager.RegisterStartupScript(upanel, upanel.GetType(), "Alert", "alert('Please select only ONE record to edit.');", true);
                        break;
                }

            }
            catch (Exception ex)
            {
                Logger.LogException(ex, Level.Error);
            }
        }

        protected void btnReleaseRules_Click(object sender, EventArgs e)
        {
            BindAttachedRuleGrid();
        }

        protected void imgbtnReleaseRule_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Int32 intMultiSelect = 0;
                string ruleId = string.Empty;
                foreach (GridViewRow gvRow in cstGrdRlsPayoffAtchmnt.GridViewPaging.Rows)
                {
                    CheckBox chkChecked = (CheckBox)gvRow.FindControl("chkMultipleSelect");
                    if (chkChecked.Checked)
                    {
                        intMultiSelect += 1;
                        ruleId = TCLHelper.RemoveHtmlWhiteSpace(gvRow.Cells[2].Text);
                    }
                }

                switch (intMultiSelect)
                {
                    case 0:
                        ScriptManager.RegisterStartupScript(upanel, upanel.GetType(), "Alert", "alert('Please select a row to edit.');", true);
                        break;
                    case 1:
                        ScriptManager.RegisterStartupScript(upanel, upanel.GetType(), "Open", "wNotePayOffOpen('" + ruleId + "','2','NoteEdit');", true);
                        break;
                    default:
                        ScriptManager.RegisterStartupScript(upanel, upanel.GetType(), "Alert", "alert('Please select only ONE record to edit.');", true);
                        break;
                }

            }
            catch (Exception ex)
            {
                Logger.LogException(ex, Level.Error);
            }
        }

        protected void chkRevolving_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRevolving.Checked == true)
                chkFannieMae.Enabled = false;
            else
                chkFannieMae.Enabled = true;
        }

        protected void chkFannieMae_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFannieMae.Checked == true)
                chkRevolving.Enabled = false;
            else
                chkRevolving.Enabled = true;
        }

        protected void chkAmortizeLoan_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAmortizeLoan.Checked == true)
            {
                chkAutomatic.Enabled = true;
                chkOriginal.Enabled = true;
                ScriptManager.RegisterStartupScript(upanel, upanel.GetType(), "confirm", "alert('Existing Interest Profile with Billing Cycle should not be used to process Amortized/Recasted loans.  Please create new interest profile with the effective of the Amortization/Recasting Start date');", true);
            }
            else
            {
                chkAutomatic.Checked = false;
                chkOriginal.Checked = false;
                chkAutomatic.Enabled = false;
                chkOriginal.Enabled = false;
                ScriptManager.RegisterStartupScript(upanel, upanel.GetType(), "confirm", "alert('You are attempting to remove the Amortized/Recasting function from this loan. This is not recommended. Please verify terms and conditions of the Interest Profile currently attached to this loan. Attach a new Interest Profile if terms or conditions deviate from the existing Interest Profile using the effective date that you remove the loan from Amortized/Recasting status.');", true);
            }
            //TabNoteInfo.ActiveTabIndex =1;
        }

        #endregion

        #region Sheeba(BorrowerBaseTerms)

        private void BindBBTerms()
        {
            noteInfoPresenter = new NoteInfoPresenter(this);
            using (DataSet dsBBTerms = noteInfoPresenter.GetBBTermsByNote())
            {
                if (dsBBTerms.IsValidDataSet())
                {
                    CstGrdBrwBaseTerms.DataSource = dsBBTerms;
                    //CstGrdBrwBaseTerms.TotalRecords = 1;
                    CstGrdBrwBaseTerms.GridAllowPaging = false;
                    CstGrdBrwBaseTerms.DataKeyNames = new string[] { "BBTermID", "Term ID" };
                    CstGrdBrwBaseTerms.HideColumns = new int[] { 7, 8, 9, 10, 11 };
                    //CstGrdBrwBaseTerms.ShowColumns = new int[] { 1, 2, 3, 5, 6, 10, 11 };
                    //CstGrdBrwBaseTerms.HideColumns = new int[] { 0, 1, 2 };
                    CstGrdBrwBaseTerms.AllowLinks = new int[] { 0 };//Term ID
                    CstGrdBrwBaseTerms.GridShowFooter = true;
                    CstGrdBrwBaseTerms.GridWidth = Unit.Percentage(100);
                    //CstGrdBrwBaseTerms.DeleteMode = Control.CustomGrid.ExtendGrid.Deletemode.Single;
                    CstGrdBrwBaseTerms.BindGrid();
                }
            }
        }

        protected void CstGrdBrwBaseTerms_OnGridRowEditing(object sender, EventArgs e)
        {
            GridViewRow gvr = ((TCL.Control.CustomGrid.ExtendGrid)(sender)).SelectedRow;
            //string strTermID = gvr.Cells[4].Text;//BBTermID
            string[] uniqueIds = CstGrdBrwBaseTerms.UniqueIDs;
            //Response.Redirect("NotesBB_terms.aspx?TermID=" + strTermID);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(),
                       "Script", "WOpen('TEEdit','" + TCLHelper.GetData(uniqueIds[0]) + "@" +
                        TCLHelper.GetData(uniqueIds[1]) + "');", true);
        }

        #endregion

        #region VLU(DocTracking,Attachments,Dates)

        protected void imgbtnAtchmntsAdd_Click(object sender, ImageClickEventArgs e)
        {
            txtDocument.Text = string.Empty;
            mPopupAttachDoc.Show();
        }

        //protected void btnPrjctUseDfndfld_Click(object sender, EventArgs e)
        //{
        //    // Response.Redirect("UserDefinedFieldNoteInfo.aspx");
        //    string strNB = Convert.ToString(Request.QueryString["B"]);
        //    Response.Redirect("UserDefinedFieldNoteInfo.aspx?NB=" + strNB + "");
        //    //mdlpopupiframeuserdefined.Show();
        //}

        protected void lstbStndardArtchBrwDocTrack_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TCLHelper.GetData(ViewState["DocCheck"]).Equals(ModifyTrue, StringComparison.OrdinalIgnoreCase) == true)
            {
                if (lstbStndardArtchBrwDocTrack.Items.Count > 0)
                    btnDoctMove.Enabled = true;
                else
                    btnDoctMove.Enabled = false;
            }
            else
            {
                btnDoctMove.Enabled = false;
                btnDocRemove.Enabled = false;
                imgbtnDocTracAdd.Visible = false;
                imgbtnDocTracEdit.Visible = false;
            }
            //if (TCLHelper.GetData(ViewState["DocCheck"]).Equals(ModifyAttachTrue, StringComparison.OrdinalIgnoreCase) == true)
            //    btnDoctMove.Enabled = true;
            //else
            //    btnDoctMove.Enabled = false;
        }
        protected void lstbArtchBrwDocTrack_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TCLHelper.GetData(ViewState["DocCheck"]).Equals(ModifyTrue, StringComparison.OrdinalIgnoreCase) == true)
            {
                if (lstbArtchBrwDocTrack.Items.Count > 0)
                    btnDocRemove.Enabled = true;
                else
                    btnDocRemove.Enabled = false;
            }
            else
            {
                btnDoctMove.Enabled = false;
                btnDocRemove.Enabled = false;
                imgbtnDocTracAdd.Visible = false;
                imgbtnDocTracEdit.Visible = false;
                return;
            }
            string getDocid = lstbArtchBrwDocTrack.GetSelectedText();
            sessionProvider.Set("DOCTRACKDOCID", getDocid);
            string lenDocid = getDocid.Split('-')[0].ToString();
            btnDocRemove.Enabled = true;
            var docParameterName = "";

            if (lstbArtchBrwDocTrack.Items.Count > 0)
            {
                for (int i = 0; i < lstbArtchBrwDocTrack.Items.Count; i++)
                {
                    if (lstbArtchBrwDocTrack.Items[i].Selected)
                    {
                        ViewState.Add("DocId", Convert.ToString(lstbArtchBrwDocTrack.Items[i].Text));
                        if (docParameterName == "")
                            docParameterName = lstbArtchBrwDocTrack.Items[i].Text.Split('-')[0].ToString();
                        else
                            docParameterName += "," + lstbArtchBrwDocTrack.Items[i].Text.Split('-')[0].ToString();
                        hdnDocId.Value = docParameterName;
                        string strDesc = string.Empty;
                        if (string.IsNullOrEmpty(lstbArtchBrwDocTrack.Items[i].Text) == false)
                        {
                            if (lstbArtchBrwDocTrack.Items[i].Text.Split('-').Length > 0)
                                ViewState.Add("docdesc", Convert.ToString(lstbArtchBrwDocTrack.Items[i].Text.Split('-')[1]));
                        }
                    }
                }
            }
            if (!docParameterName.Trim().Contains(","))
            {
                if (lenDocid.Length > 3)
                {
                    imgbtnDocTracEdit.Visible = true;
                    imgbtnDocTracAdd.Visible = false;
                }
                else
                {
                    imgbtnDocTracEdit.Visible = false;
                    imgbtnDocTracAdd.Visible = true;
                }
            }
            else
            {
                imgbtnDocTracEdit.Visible = false;
                imgbtnDocTracAdd.Visible = false;
            }
        }

        protected void btnDoctMove_Click(object sender, EventArgs e)
        {
            var docParameterName = "";
            if (lstbStndardArtchBrwDocTrack.Items.Count > 0)
            {
                for (int i = 0; i < lstbStndardArtchBrwDocTrack.Items.Count; i++)
                {
                    if (lstbStndardArtchBrwDocTrack.Items[i].Selected)
                    {
                        if (docParameterName == "")
                            docParameterName = lstbStndardArtchBrwDocTrack.Items[i].Text.Split('-')[0].ToString();
                        else
                            docParameterName += "," + lstbStndardArtchBrwDocTrack.Items[i].Text.Split('-')[0].ToString();
                    }
                }
            }
            string vvDocId = docParameterName;
            DocID = vvDocId;
            this.noteInfoPresenter.InsertAttachments();
            this.noteInfoPresenter.LoadDocAttach();
            lstbArtchBrwDocTrack.DataBind();
            imgbtnDocTracAdd.Visible = false;
            imgbtnDocTracEdit.Visible = false;
        }

        protected void btnDocRemove_Click(object sender, EventArgs e)
        {
            var docDocIdName = "";
            if (lstbArtchBrwDocTrack.Items.Count > 0)
            {
                for (int i = 0; i < lstbArtchBrwDocTrack.Items.Count; i++)
                {
                    if (lstbArtchBrwDocTrack.Items[i].Selected)
                    {
                        if (docDocIdName == "")
                            docDocIdName = lstbArtchBrwDocTrack.Items[i].Text.Split('-')[0].ToString();
                        else
                            docDocIdName += "," + lstbArtchBrwDocTrack.Items[i].Text.Split('-')[0].ToString();
                    }
                }
            }
            DocAttachSelected = docDocIdName;
            this.noteInfoPresenter.DeleteDoc();
            this.noteInfoPresenter.LoadDocAttach();
            lstbArtchBrwDocTrack.DataBind();
            imgbtnDocTracAdd.Visible = false;
            imgbtnDocTracEdit.Visible = false;
            if (lstbArtchBrwDocTrack.Items.Count == 0)
                btnDocRemove.Enabled = false;
        }

        protected void imgbtnAttachDetails_Click(object sender, ImageClickEventArgs e)
        {
            foreach (GridViewRow gvRow in gvAttachments.GridViewPaging.Rows)
            {
                ImageButton selectButton = (ImageButton)gvRow.FindControl("imgRowSelect");
                if (selectButton.AlternateText == "1")
                {
                    string[] strAttached = ViewState["Attached"].ToString().Split(new char[] { '-' }, 2);
                    lblAttached.Text = strAttached[1] + " by " + strAttached[0];
                    lblNoteInfo.Text = NoteNumber + " " + UnitNumber;
                    lblAttachID.Text = ViewState["AttachID"].ToString();
                    lblDescription.Text = ViewState["Description"].ToString();
                    //lblFileLocation.Text = ViewState["FileLoc"].ToString();
                    mPopupAttachdetails.Show();
                }
            }

        }

        protected void btnAttachmentOk_Click(object sender, EventArgs e)
        {
            if (this.fuAttachments.HasFile)
            {
                InsertAttachment();
            }
        }

        private void InsertAttachment()
        {
            noteInfoPresenter = new NoteInfoPresenter(this);
            AttachmentEntity attachmentEntity = new AttachmentEntity();
            double AttachmentID = noteInfoPresenter.GetRandomId();
            Stream strm = this.fuAttachments.PostedFile.InputStream;
            BinaryReader br = new BinaryReader(strm);
            Byte[] size = br.ReadBytes((int)strm.Length);
            attachmentEntity.AttachedTo = "N";
            attachmentEntity.AttachmentID = AttachmentID;
            attachmentEntity.BNUMBER = string.Empty; ;
            attachmentEntity.CreatedBy = Session["User"].ToString().ToUpper();
            attachmentEntity.CreatedDate = DateTime.Now.ToLocalTime();
            attachmentEntity.FileDescription = txtDocument.Text;
            if (attachmentEntity.FileDescription == string.Empty)
                attachmentEntity.FileDescription = this.fuAttachments.FileName;
            attachmentEntity.OriginalFilename = Path.GetFullPath(fuAttachments.PostedFile.FileName);  //this.fuAttachments.FileName; 
            attachmentEntity.PhysicalDocument = size;
            attachmentEntity.PNOTENO = NoteNumber;
            attachmentEntity.PUNIT = UnitNumber;
            attachmentEntity.UNCFlag = 0;
            noteInfoPresenter.InsertAttachments(attachmentEntity);
            //   if(gvAttachments.RowsPerPage!=null)
            // ARowsPerPage = gvAttachments.RowsPerPage;
            //APageNumber = 1;
            ScriptManager.RegisterStartupScript(upanel, upanel.GetType(), "msg", "alert('New record successfully added.');", true);
            BindBorrowerAttachments();
        }

        private void BindBorrowerAttachments()
        {
            // from  
            Int64 TotalRowCount = 0;
            noteInfoPresenter = new NoteInfoPresenter(this);
            imgbtnAtchmntsDel.Visible = false;
            imgbtnAttachDetails.Visible = false;
            using (DataSet dsBorrowerAttachments = noteInfoPresenter.GetBorrowerAttachments(out TotalRowCount, "N", NoteNumber, UnitNumber, iRowsPerPage, iPageNumber))
            {
                if (dsBorrowerAttachments.IsValidDataColumn())
                {

                    if (TCLHelper.ConvertToBool(ViewState["ModifyAttach"]))
                    {
                        imgbtnAtchmntsAdd.Visible = true;
                        if (TotalRowCount > 0)
                        {
                            imgbtnAtchmntsDel.Enabled = true;
                            imgbtnAtchmntsDel.Visible = true;
                        }
                    }

                    if (TotalRowCount > 0)
                    {
                        imgbtnAttachDetails.Visible = true;
                        imgbtnAttachDetails.Enabled = true;
                    }

                    gvAttachments.GridRowStyleCSS = "normalrow";
                    gvAttachments.GridHeaderRowCSS = "pl10 pr10 gridheader";
                    gvAttachments.GridAlternatingRowCSS = "altrow";
                    gvAttachments.TotalRecords = TotalRowCount;
                    gvAttachments.AllowLinks = new int[] { 0 };
                    gvAttachments.HideColumns = new int[] { 2, 3, 4 };
                    gvAttachments.DataKeyNames = new string[] { "Description" };
                    gvAttachments.GridAllowPaging = true;
                    gvAttachments.DeleteMode = Control.CustomGrid.ExtendGrid.Deletemode.Single;
                    gvAttachments.RowsPerPage = iRowsPerPage;
                    gvAttachments.PageNumber = iPageNumber;
                    gvAttachments.DataSource = dsBorrowerAttachments;
                    gvAttachments.BindGrid();
                }
            }
        }

        protected void gvAttachments_GridCheckedChange(object sender, ImageClickEventArgs e)
        {
            GridViewRow gvRow = gvAttachments.SelectedRow;
            LinkButton lnkButton = (LinkButton)gvRow.FindControl("lnk0");
            ViewState["Description"] = Convert.ToString(lnkButton.Text);
            ViewState["Attached"] = gvRow.Cells[2].Text;
            ViewState["AttachID"] = gvRow.Cells[3].Text;
            ViewState["FileLoc"] = gvRow.Cells[4].Text;

        }

        protected void gvAttachments_GridRowEditing(object sender, EventArgs e)
        {
            GridViewRow gvRow = gvAttachments.SelectedRow;
            LinkButton lnkAttach = (LinkButton)gvRow.FindControl("lnk0");
            string createdDate = gvRow.Cells[2].Text.ToString();
            DataTable dt = new DataTable();
            dt = noteInfoPresenter.GetDetails(createdDate);
            if (dt != null)
            {
                string strUri = "ViewDocs.aspx?DocId=" + Convert.ToString(gvRow.Cells[3].Text);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Script", "javascript:window.open('" + strUri + "');", true);
            }
        }

        protected void gvAttachments_GridRowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton linkAttach = (LinkButton)e.Row.FindControl("lnk0");
                ToolkitScriptManager1.RegisterPostBackControl(linkAttach);
            }
        }

        protected void imgbtnAtchmntsDel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                try
                {
                    foreach (GridViewRow gvRow in gvAttachments.GridViewPaging.Rows)
                    {
                        ImageButton selectButton = (ImageButton)gvRow.FindControl("imgRowSelect");
                        if (selectButton.AlternateText == "1")
                        {
                            noteInfoPresenter = new NoteInfoPresenter(this);
                            noteInfoPresenter.DeleteDocAttachment(this.NoteNumber, this.UnitNumber, Convert.ToString(gvRow.Cells[3].Text));
                            BindBorrowerAttachments();
                            ScriptManager.RegisterStartupScript(upanel, upanel.GetType(), "msg", "alert('Attachment deleted successfully.');", true);
                            return;

                        }
                    }
                    ScriptManager.RegisterStartupScript(upanel, upanel.GetType(), "msg", "alert('Please select a record to delete.');", true);
                }
                catch (Exception ex)
                {
                    Logger.LogException(ex, Level.Error);
                }

                //int istatus = 0;
                //gvAttachments


                //if (ViewState["Attached"] == null)
                //{
                //    ScriptManager.RegisterStartupScript(upanel, upanel.GetType(), "msg", "alert('Please select a record to delete.');", true);
                //    // lblErrorFail.Text = "Please select a record to delete.";
                //}
                //else
                //{
                //    istatus = noteInfoPresenter.DeleteAttachment("N", ViewState["Attached"].ToString());
                //    //lblErrorSuccess.Text = "Code deleted successfully.";
                //    ScriptManager.RegisterStartupScript(upanel, upanel.GetType(), "msg", "alert('Code deleted successfully.');", true);
                //}
                //BindBorrowerAttachments();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, Level.Error);
            }
        }


        #endregion

        #region Save NoteInfo

        #endregion

        private void NoteSecurity()
        {
            bool chkLoanCommit;
            bool chkPrnplBalCap;
            bool chkLoanDeposit;
            bool chkEscrow;
            bool chkIntrProf;
            bool chkDocChklst;
            bool chkBdgtCtrl;
            bool chkNoteLvl;
            bool chkReleseLvl;
            bool chkAttachments;
            bool chkContInfo;
            if (sessionProvider.Get("UserSecuirtyCheck") != null)
            {
                using (DataSet ds = (DataSet)sessionProvider.Get("UserSecuirtyCheck"))
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    // Daily Processing Note Security
                    if (this.NoteMode.Equals("Property", StringComparison.OrdinalIgnoreCase))
                    {
                        // Post fees New/Edit Disable/Enable in Financial Tab
                        btnFincTabPostFee.Enabled = TCLHelper.GetData(dr["OS_OPT25"]) == "0" ? false : true;

                        //Loan Commitment in Financial Tab
                        chkLoanCommit = TCLHelper.GetData(dr["OS_OPT6"]) == "0" ? false : true;
                        // Rollback Gowtham
                        //txtFincTabLoanCommit1.Enabled = chkLoanCommit;
                        txtFincTabLoanCommit2.Enabled = chkLoanCommit;
                        txtFincTabLoanCommit3.Enabled = chkLoanCommit;
                        txtFincTabEffctDate1.Enabled = chkLoanCommit;
                        drpdwnlstTranscode.Enabled = chkLoanCommit;

                        // Commitment Rule in Financial Tab
                        chkORComRule.Enabled = TCLHelper.GetData(dr["OS_OPT64"]) == "0" ? false : true;

                        // Principal Bal.Cap in Financial Tab
                        chkPrnplBalCap = TCLHelper.GetData(dr["OS_OPT77"]) == "0" ? false : true;
                        //txtFincTabprincBal1.Enabled = chkPrnplBalCap;
                        txtFincTabprincBal2.Enabled = chkPrnplBalCap;
                        txtFincTabprincBal3.Enabled = chkPrnplBalCap;

                        // Loan level Deposit in Financial Tab
                        chkLoanDeposit = TCLHelper.GetData(dr["OS_OPT66"]) == "0" ? false : true;
                        //txtFincTabLnLvlDpst1.Enabled = chkLoanDeposit;
                        txtFincTabLnLvlDpst2.Enabled = chkLoanDeposit;
                        txtFincTabLnLvlDpst3.Enabled = chkLoanDeposit;
                        //txtFincTabEffctDate2.Enabled = chkLoanDeposit;
                        drpdwnlstLnvlDpstEftvTrncode.Enabled = chkLoanDeposit;

                        // Loan level Escrow in Financial Tab
                        chkEscrow = TCLHelper.GetData(dr["OS_OPT68"]) == "0" ? false : true;
                        //txtFincTabLnLvlEscrw1.Enabled = chkEscrow;
                        txtFincTabLnLvlEscrw2.Enabled = chkEscrow;
                        txtFincTabLnLvlEscrw3.Enabled = chkEscrow;
                        //txtFincTabEffctDate3.Enabled = chkEscrow;
                        drpdwnlstLnLvlEscrwTrncode.Enabled = chkEscrow;

                        // Maturity Date in Dates Tab
                        txtDateTabMturty.Enabled = TCLHelper.GetData(dr["OS_OPT7"]) == "0" ? false : true;
                        txtDateTabQotPayOff.Enabled = dr["Os_Opt41"].ToString() == "0" ? true : false;
                        txtDateTabQotPost.Enabled = dr["Os_Opt41"].ToString() == "0" ? true : false;

                        // Doc Tracking in Document Tab
                        ViewState["DocCheck"] = TCLHelper.GetData(dr["OS_OPT3"]) == "0" ? false : true;

                        //Budget Control Tab
                        chkBdgtCtrl = TCLHelper.GetData(dr["OS_OPT4"]) == "0" ? false : true;
                        imgbtnBdgtCntrlAdd.Visible = chkBdgtCtrl;
                        imgbtnEdit.Visible = chkBdgtCtrl;
                        ImgbtnDelete.Visible = chkBdgtCtrl;

                        // Interest Profile Tab
                        chkIntrProf = TCLHelper.GetData(dr["OS_OPT5"]) == "0" ? false : true;
                        //imgbtnIntrstPrflsAdd.Enabled = TCLHelper.GetData(dr["OS_OPT5"]) == "0" ? false : true;
                        imgbtnIntrstPrflsAdd.Visible = chkIntrProf;
                        imgbtnIntrstPrflsDel.Visible = chkIntrProf;
                        imgbtnIntrstPrflsCopy.Visible = chkIntrProf;


                        // General Tab
                        chkInDefault.Enabled = TCLHelper.GetData(dr["OS_OPT57"]) == "0" ? false : true;
                        chkNonAccural.Enabled = TCLHelper.GetData(dr["OS_OPT59"]) == "0" ? false : true;
                        chkStopFin.Enabled = TCLHelper.GetData(dr["OS_OPT20"]) == "0" ? false : true;

                        // Note Payoff Rules in Release Tab
                        chkNoteLvl = TCLHelper.GetData(dr["OS_OPT48"]) == "0" ? false : true;
                        btnPayoffMoveRight.Enabled = chkNoteLvl;
                        imgbtnNtPayoffRulsDelte.Enabled = chkNoteLvl;
                        ViewState.Add("NotePayOff", chkNoteLvl);
                        // Release Rules in Release Tab
                        ViewState["ModifyReleaseRule"] = TCLHelper.GetData(dr["OS_OPT50"]) == "0" ? false : true;
                        //chkReleseLvl = TCLHelper.GetData(dr["OS_OPT50"]) == "0" ? false : true;
                        //btnRlsRulesMoveRight.Enabled = chkReleseLvl;
                        //imgbtnRelsRulesDel.Enabled = chkReleseLvl;

                        // Attachments in Document Tab
                        ViewState["ModifyAttach"] = TCLHelper.GetData(dr["OS_OPT81"]) == "0" ? false : true;



                        // Tenant security(Leasing Info) in Project Tab
                        //have to check with Ravi
                        ViewState["LeaseInfo"] = TCLHelper.GetData(dr["OS_OPT98"]) == "0" ? false : true;

                        // Sales Contract Information in Project Tab
                        //have to check with Ravi
                        ViewState["KeyContEdit"] = TCLHelper.GetData(dr["OS_OPT100"]) == "0" ? false : true;
                        //btnKeyCntsMoveRight.Enabled = chkContInfo;
                        //imgbtnDKeyContact.Visible = chkContInfo;
                        //ViewState.Add("KeyContEdit", chkContInfo);

                        //TAB Security
                        tabPnlProject.Enabled = TCLHelper.GetData(dr["TS_OPT8"]) == "0" ? false : true;
                        tabPnlGeneral.Enabled = TCLHelper.GetData(dr["TS_OPT9"]) == "0" ? false : true;
                        tabPnlDates.Enabled = TCLHelper.GetData(dr["TS_OPT10"]) == "0" ? false : true;
                        tabPnlFinc.Enabled = TCLHelper.GetData(dr["TS_OPT11"]) == "0" ? false : true;
                        tabpnlRlsSch.Enabled = TCLHelper.GetData(dr["TS_OPT12"]) == "0" ? false : true;
                        tabPnlBdgCntrl.Enabled = TCLHelper.GetData(dr["TS_OPT13"]) == "0" ? false : true;
                        tabpnlInterestPrfl.Enabled = TCLHelper.GetData(dr["TS_OPT14"]) == "0" ? false : true;
                        tabPnlEquityPrfl.Enabled = TCLHelper.GetData(dr["TS_OPT15"]) == "0" ? false : true;
                        tabpnlDocTrack.Enabled = TCLHelper.GetData(dr["TS_OPT16"]) == "0" ? false : true;
                        tabpnlNotePayOffRules.Enabled = TCLHelper.GetData(dr["TS_OPT38"]) == "0" ? false : true;
                        tabpnlRules.Enabled = TCLHelper.GetData(dr["TS_OPT40"]) == "0" ? false : true;
                        tabPnlkyCnt.Enabled = TCLHelper.GetData(dr["TS_OPT17"]) == "0" ? false : true;
                        this.mblnCanOverrideRule = TCLHelper.GetData(dr["OS_OPT64"]) == "0" ? false : true;
                    }
                    // Pipeline Note Security
                    else if (NoteMode == "Pipeline")
                    {
                        // Post fees New/Edit Disable/Enable in Financial Tab
                        btnFincTabPostFee.Enabled = TCLHelper.GetData(dr["OS_OPT32"]) == "0" ? false : true;

                        //Loan Commitment in Financial Tab
                        chkLoanCommit = TCLHelper.GetData(dr["OS_OPT33"]) == "0" ? false : true;
                        // Rollback Gowtham
                        //txtFincTabLoanCommit1.Enabled = chkLoanCommit;
                        txtFincTabLoanCommit2.Enabled = chkLoanCommit;
                        txtFincTabLoanCommit3.Enabled = chkLoanCommit;
                        txtFincTabEffctDate1.Enabled = chkLoanCommit;
                        drpdwnlstTranscode.Enabled = chkLoanCommit;

                        // Commitment Rule in Financial Tab
                        chkORComRule.Enabled = TCLHelper.GetData(dr["OS_OPT65"]) == "0" ? false : true;

                        // Principal Bal.Cap in Financial Tab
                        chkPrnplBalCap = TCLHelper.GetData(dr["OS_OPT76"]) == "0" ? false : true;
                        //txtFincTabprincBal1.Enabled = chkPrnplBalCap;
                        txtFincTabprincBal2.Enabled = chkPrnplBalCap;
                        txtFincTabprincBal3.Enabled = chkPrnplBalCap;

                        // Loan level Deposit in Financial Tab
                        chkLoanDeposit = TCLHelper.GetData(dr["OS_OPT67"]) == "0" ? false : true;
                        //txtFincTabLnLvlDpst1.Enabled = chkLoanDeposit;
                        txtFincTabLnLvlDpst2.Enabled = chkLoanDeposit;
                        txtFincTabLnLvlDpst3.Enabled = chkLoanDeposit;
                        txtFincTabEffctDate2.Enabled = chkLoanDeposit;
                        drpdwnlstLnvlDpstEftvTrncode.Enabled = chkLoanDeposit;

                        // Loan level Escrow in Financial Tab
                        chkEscrow = TCLHelper.GetData(dr["OS_OPT69"]) == "0" ? false : true;
                        //txtFincTabLnLvlEscrw1.Enabled = chkEscrow;
                        txtFincTabLnLvlEscrw2.Enabled = chkEscrow;
                        txtFincTabLnLvlEscrw3.Enabled = chkEscrow;
                        txtFincTabEffctDate3.Enabled = chkEscrow;
                        drpdwnlstLnLvlEscrwTrncode.Enabled = chkEscrow;

                        // Maturity Date in Dates Tab
                        txtDateTabMturty.Enabled = TCLHelper.GetData(dr["OS_OPT34"]) == "0" ? false : true;
                        txtDateTabQotPayOff.Enabled = TCLHelper.GetData(dr["Os_Opt41"]) == "0" ? true : false;
                        txtDateTabQotPost.Enabled = TCLHelper.GetData(dr["Os_Opt41"]) == "0" ? true : false;

                        // Doc Tracking in Document Tab
                        ViewState["DocCheck"] = TCLHelper.GetData(dr["OS_OPT35"]) == "0" ? false : true;

                        //Budget Control Tab
                        chkBdgtCtrl = TCLHelper.GetData(dr["OS_OPT36"]) == "0" ? false : true;
                        imgbtnBdgtCntrlAdd.Visible = chkBdgtCtrl;
                        imgbtnEdit.Visible = chkBdgtCtrl;
                        ImgbtnDelete.Visible = chkBdgtCtrl;

                        // Interest Profile Tab                        
                        chkIntrProf = TCLHelper.GetData(dr["OS_OPT37"]) == "0" ? false : true;
                        imgbtnIntrstPrflsAdd.Visible = chkIntrProf;
                        imgbtnIntrstPrflsDel.Visible = chkIntrProf;
                        imgbtnIntrstPrflsCopy.Visible = chkIntrProf;

                        // General Tab
                        chkInDefault.Enabled = TCLHelper.GetData(dr["OS_OPT56"]) == "0" ? false : true;
                        chkNonAccural.Enabled = TCLHelper.GetData(dr["OS_OPT58"]) == "0" ? false : true;
                        chkStopFin.Enabled = TCLHelper.GetData(dr["OS_OPT38"]) == "0" ? false : true;

                        // Note Payoff Rules in Release Tab
                        chkNoteLvl = TCLHelper.GetData(dr["OS_OPT47"]) == "0" ? false : true;
                        btnPayoffMoveRight.Enabled = chkNoteLvl;
                        imgbtnNtPayoffRulsDelte.Enabled = chkNoteLvl;
                        ViewState.Add("NotePayOff", chkNoteLvl);
                        // Release Rules in Release Tab
                        ViewState["ModifyReleaseRule"] = TCLHelper.GetData(dr["OS_OPT49"]) == "0" ? false : true;



                        // Attachments in Document Tab
                        ViewState["ModifyAttach"] = TCLHelper.GetData(dr["OS_OPT80"]) == "0" ? false : true;
                        //chkAttachments = dr["OS_OPT80"].ToString() == "0" ? false : true;



                        // Tenant security(Leasing Info) in Project Tab
                        //have to check with Ravi
                        ViewState["LeaseInfo"] = TCLHelper.GetData(dr["OS_OPT97"]) == "0" ? false : true;

                        // Sales Contract Information in Project Tab
                        //have to check with Ravi
                        ViewState["KeyContEdit"] = TCLHelper.GetData(dr["OS_OPT99"]) == "0" ? false : true;
                        //btnKeyCntsMoveRight.Enabled = chkContInfo;
                        //imgbtnDKeyContact.Visible = chkContInfo;
                        //ViewState.Add("KeyContEdit", chkContInfo);

                        //TAB Security
                        tabPnlProject.Enabled = TCLHelper.GetData(dr["TS_OPT25"]) == "0" ? false : true;         // Project
                        tabPnlGeneral.Enabled = TCLHelper.GetData(dr["TS_OPT26"]) == "0" ? false : true;         // General
                        tabPnlDates.Enabled = TCLHelper.GetData(dr["TS_OPT27"]) == "0" ? false : true;          // Dates
                        tabPnlFinc.Enabled = TCLHelper.GetData(dr["TS_OPT28"]) == "0" ? false : true;           // Financial
                        tabpnlRlsSch.Enabled = TCLHelper.GetData(dr["TS_OPT29"]) == "0" ? false : true;         // Release Schedule
                        tabPnlBdgCntrl.Enabled = TCLHelper.GetData(dr["TS_OPT30"]) == "0" ? false : true;       // Budget Control
                        tabpnlInterestPrfl.Enabled = TCLHelper.GetData(dr["TS_OPT31"]) == "0" ? false : true;   // Interest Profile
                        tabPnlEquityPrfl.Enabled = TCLHelper.GetData(dr["TS_OPT32"]) == "0" ? false : true;     // Equity Profile
                        tabpnlDocTrack.Enabled = TCLHelper.GetData(dr["TS_OPT33"]) == "0" ? false : true;       // Doc Tracking
                        tabpnlNotePayOffRules.Enabled = TCLHelper.GetData(dr["TS_OPT37"]) == "0" ? false : true;// Note Payoff Rules    
                        tabpnlRules.Enabled = TCLHelper.GetData(dr["TS_OPT39"]) == "0" ? false : true;          // Release Rules
                        tabPnlkyCnt.Enabled = TCLHelper.GetData(dr["TS_OPT34"]) == "0" ? false : true;          // Key Content
                        this.mblnCanOverrideRule = TCLHelper.GetData(dr["OS_OPT65"]) == "0" ? false : true;
                    }
                    if (ds.IsTableExists(1))
                    {
                        this.blnAUDITRPT = TCLHelper.ConvertToTCLBool(ds.Tables[1].Rows[0]["AUDITRPT"].ToString());
                        this.blnAR_TAXID = TCLHelper.ConvertToTCLBool(ds.Tables[1].Rows[0]["AR_TAXID"].ToString());
                        this.blnAR_LDESC = TCLHelper.ConvertToTCLBool(ds.Tables[1].Rows[0]["AR_LDESC"].ToString());
                        this.blnAR_LOC = TCLHelper.ConvertToTCLBool(ds.Tables[1].Rows[0]["AR_LOC"].ToString());
                        this.blnAR_LOCMAT = TCLHelper.ConvertToTCLBool(ds.Tables[1].Rows[0]["AR_LOCMAT"].ToString());
                        this.blnAR_INT = TCLHelper.ConvertToTCLBool(ds.Tables[1].Rows[0]["AR_INT"].ToString());
                        this.blnAR_BILLOPT = TCLHelper.ConvertToTCLBool(ds.Tables[1].Rows[0]["AR_BILLOPT"].ToString());
                        this.blnAR_BUD = TCLHelper.ConvertToTCLBool(ds.Tables[1].Rows[0]["AR_BUD"].ToString());
                        this.blnAR_BUDPROF = TCLHelper.ConvertToTCLBool(ds.Tables[1].Rows[0]["AR_BUDPROF"].ToString());
                        this.blnAR_EQTPROF = TCLHelper.ConvertToTCLBool(ds.Tables[1].Rows[0]["AR_EQTPROF"].ToString());
                        this.blnAR_LOCPARENTTOTAL = TCLHelper.ConvertToTCLBool(ds.Tables[1].Rows[0]["AR_LOCPARENTTOTAL"].ToString());
                        this.blnAR_LOCTERMS = TCLHelper.ConvertToTCLBool(ds.Tables[1].Rows[0]["AR_LOC"].ToString());
                    }
                }
            }
        }

        #region Save NoteInfo


        protected void btnProjectCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Notes.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            InvokeJavascriptFunction("Save1('" + this.mProp_Inquiry + "','" + this.txt_Adminstratorflag + "','" + this.Action + "','" + this.bRenewal + "','" + this.NoteMode + "','" + this.nRenewalCount + "','" + this.dPrevMatDate + "','" + this.dPrvCompDate + "')");
        }

        protected void btnSaveProcess_Click(object sender, EventArgs e)
        {
            try
            {
                string eventTarget = this.Request.Form["__EVENTTARGET"];
                string eventArg = this.Request.Form["__EVENTARGUMENT"];
                string sMsg = string.Empty;
                string effDate = string.Empty;
                string oldEffDate = string.Empty;
                int bRateChange;
                long nMonths;
                if (eventArg.Equals("save1", StringComparison.OrdinalIgnoreCase))
                {
                    Dictionary<string, string> dictScriptValues = TCLHelper.JavascriptDeSerializer<Dictionary<string, string>>(eventTarget);
                    this.bFeePostCompDate = TCLHelper.ConvertToInt(dictScriptValues["bFeePostCompDate"]);
                    this.bFeePostMaturity = TCLHelper.ConvertToInt(dictScriptValues["bFeePostMaturity"]);
                    ViewState.Add("bFeePostCompDate", this.bFeePostCompDate);
                    ViewState.Add("bFeePostMaturity", this.bFeePostMaturity);
                    if (this.UnitNumber != "000" && this.NoteType != "B")
                    {
                        CalcAllocCommit();
                        this.cAlloc = TCLHelper.ConvertToDouble(TCLHelper.ConvertFromUSCurrency(txtTotalAllocated.Text));
                        ViewState.Add("cAlloc", this.cAlloc);
                        this.cEst = TCLHelper.ConvertToDouble(TCLHelper.ConvertFromUSCurrency(txtTotalEstimated.Text));
                        ViewState.Add("cEst", this.cEst);
                        if (this.cEst != this.cAlloc)
                        {
                            this.cDiff = this.cEst - this.cAlloc;
                            ViewState.Add("cDiff", this.cDiff);
                        }
                        InvokeJavascriptFunction("Save2('" + cEst + "','" + cAlloc + "');");
                        return;
                    }
                    else
                    {
                        InvokeJavascriptFunction("Save3('" + TCLHelper.ConvertFromUSCurrency(this.txtFincTabprincBal3.Text) + "','" + TCLHelper.ConvertFromUSCurrency(this.txtFincTabLoanCommit3.Text) + "');");
                        return;
                    }
                }
                if (eventArg.Equals("save2", StringComparison.OrdinalIgnoreCase))
                {
                    InvokeJavascriptFunction("Save3('" + TCLHelper.ConvertFromUSCurrency(this.txtFincTabprincBal3.Text) + "','" + TCLHelper.ConvertFromUSCurrency(this.txtFincTabLoanCommit3.Text) + "');");
                    return;
                }
                if (eventArg.Equals("save3", StringComparison.OrdinalIgnoreCase))
                {
                    if (this.noteInfoPresenter.ComRuleValidate() == false)
                    {
                        //TabNoteInfo.ActiveTab = TabNoteInfo.Tabs[5];
                        //TabNoteInfo.Tabs[5].Focus();
                        //return;
                    }

                    if (this.noteInfoPresenter.RuleChangedCommitmentAmt() == false)
                    {
                        // TabNoteInfo.ActiveTab = TabNoteInfo.Tabs[5];
                        // TabNoteInfo.Tabs[5].Focus();
                        //return;
                    }

                    if (this.Action == 1)
                    {
                        bRateChange = Convert.ToInt32(false);
                        DataSet dsRate = null;
                        bool isDateRange = false;
                        DateTime? toDate = DateTime.Now;
                        dsRate = this.noteInfoPresenter.CmdCommand(TCLHelper.ConvertToDateTimeNullable(txtFincTabEffctDate1.Text), toDate, out isDateRange);
                        if (isDateRange == true)
                        {
                            sMsg = "Interest rate changes have occurred between the commitment effective date and the current date.";
                            sMsg = sMsg + "You should enter an interest profile for each interest rate effective date ";
                            sMsg = sMsg + "or the interest will be calculated to date using only the current rate.";
                            sMsg = sMsg + "Continue with save?";
                            InvokeJavascriptFunction("Save4('" + sMsg + "')");
                            return;
                        }

                        dsRate = this.noteInfoPresenter.CmdCommandValidate(TCLHelper.ConvertToDateTimeNullable(txtFincTabEffctDate1.Text), toDate, out isDateRange);
                        //till sp -uspCmdCommandValidate

                        if (isDateRange == true)
                        {
                            sMsg = "Interest rate changes have ocurred between the Equity Profile's effective date and the current date.  ";
                            sMsg = sMsg + "You will need to create an Equity Interest Profile for each rate change effective date.  ";
                            sMsg = sMsg + "Continue with save?";
                            InvokeJavascriptFunction("Save5('" + sMsg + "')");
                            return;
                        }
                        //till sp -uspCmdCommand
                    }
                }
                if (eventArg.Equals("save4", StringComparison.OrdinalIgnoreCase))
                {
                    bool isDateRange = false;
                    bRateChange = 0;
                    DataSet dsRate1 = new DataSet();
                    DateTime? toDate = DateTime.Now;
                    dsRate1 = this.noteInfoPresenter.CmdCommandValidate(TCLHelper.ConvertToDateTimeNullable(txtFincTabEffctDate1.Text), toDate, out isDateRange);
                    //till sp -uspCmdCommandValidate

                    if (bRateChange != 0)
                    {
                        sMsg = "Interest rate changes have ocurred between the Equity Profile's effective date and the current date.  ";
                        sMsg = sMsg + "You will need to create an Equity Interest Profile for each rate change effective date.  ";
                        sMsg = sMsg + "Continue with save?";
                        InvokeJavascriptFunction("Save5('" + sMsg + "')");
                        return;
                    }
                }

                if (eventArg.Equals("save5", StringComparison.OrdinalIgnoreCase) || eventArg.Equals("save3", StringComparison.OrdinalIgnoreCase) || eventArg.Equals("save4", StringComparison.OrdinalIgnoreCase))
                {
                    //                ' 8.11.0 - RFC 15151
                    //        With mobjAdministrators
                    //            If .NotFound Then                                   ' Initial Value NOT Found; INVALID
                    //                If Trim$(UCase$(cboNote(18))) = Trim$(UCase$(.InitalValue)) Then  ' Still Same
                    //                    MsgBox "Current 'Administrator' is not Valid, Please select a valid user"
                    //                    tbNoteInfo.ActiveTab = GetTabIndex("PROJECT")
                    //                    GoTo SetNoteBailOut
                    //                End If
                    //            End If
                    //        End With
                    //' 8.11.0 - RFC 15151
                    this.bFeePostCompDate = TCLHelper.ConvertToInt(ViewState["bFeePostCompDate"]);
                    if (bFeePostCompDate != 0)
                    {
                        nMonths = TCLHelper.DateDiff("M", DateTime.Parse(this.dPrvCompDate), txtDateTabCnsCmpl.Text);
                        //mGoSubStack.PutPosition(1); 
                        double nFeeAmt;
                        bool blnPostExtFee = true;
                        DataSet dsAmountDetail = this.noteInfoPresenter.CalcAmtValidate(this.BorrowerNo, this.NoteNumber, this.UnitNumber, nMonths, out nFeeAmt, out blnPostExtFee);
                        this.nFeeAmount = nFeeAmt;

                        InvokeJavascriptFunction("Save7('" + this.blnPostExtFee + "','" + nFeeAmount + "','" + bFeePostCompDate + "','');");
                        return;
                        //if (this.blnPostExtFee == false)
                        //{
                        //    //MessageBox.Show("Extension fee cannot be calculated.  The loan is not participated or the participant interest profile is missing.", null, vbOKOnly, vbOKOnly);
                        //    goto Continue;
                        //}

                        //if (this.nFeeAmount <= 0)
                        //{
                        //    //MessageBox.Show("No Extension Fee Required.", null, vbOKOnly, vbOKOnly);
                        //    goto Continue;
                        //}
                        //PostFee(this.NoteNumber, this.UnitNumber, cboNote[0], "NI:Extension", nMonths, nFeeAmount, "Extension Fee", true);
                    }
                    else
                    {
                        this.bFeePostMaturity = TCLHelper.ConvertToInt(ViewState["bFeePostMaturity"]);
                        int isValidProp = 0;
                        if (this.NoteType == "M")
                        {
                            if (txtDateTabQotPost.Text != string.Empty)
                            {
                                DataSet dsPropVal = this.noteInfoPresenter.GetValidateProperty();
                                if (dsPropVal.IsValidDataSet())
                                {
                                    isValidProp = -1;
                                }
                            }
                        }

                        InvokeJavascriptFunction("Save7('" + this.blnPostExtFee + "','" + nFeeAmount + "','" + bFeePostCompDate + "','" + bFeePostMaturity + "','" + isValidProp + "','" + this.NoteType + "');");
                        return;
                    }
                }

                if (eventArg.Equals("save7", StringComparison.OrdinalIgnoreCase) || eventArg.Equals("save3", StringComparison.OrdinalIgnoreCase))
                {
                    if (chkNonAccural.Checked != this.mnPrvNonAccrual)
                    {
                        InvokeJavascriptFunction("LoadNonAccural()");
                        return;
                    }

                    goto Continue;
                }

                if (eventArg.Equals("savefinal", StringComparison.OrdinalIgnoreCase) || eventArg.Equals("save3", StringComparison.OrdinalIgnoreCase))
                {
                    effDate = eventTarget.Split(',')[0].Split(':')[1];
                    oldEffDate = eventTarget.Split(',')[1].Split(':')[1];
                    goto Continue;
                }

                if (eventArg.Equals("auditresponse", StringComparison.OrdinalIgnoreCase))
                {
                    Response.Redirect("Notes.aspx");
                }

            Continue:
                //if (bFeePostMaturity != 0)
                //{
                //    //PostFee(this.NoteNumber, this.UnitNumber, cboNote[0], "NI:Renewal", , , "Renewal Fee", true);
                //}

                //if (ddlMLoc.SelectedIndex > 0)
                //{
                //    if (ddlSLoc.SelectedIndex == 0)
                //    {
                //        //MessageBox.Show("If a Master LOC is selected then a Sub LOC must be selected.", "Master/Sub Line of Credit", vbOKOnly + vbInformation, vbOKOnly + vbInformation);
                //        return;
                //    }
                //}
                //else
                //{
                //    if (ddlSLoc.SelectedIndex > 0)
                //    {
                //        //MessageBox.Show("If a Sub LOC is selected then a Master LOC must be selected.", "Master/Sub Line of Credit", vbOKOnly + vbInformation, vbOKOnly + vbInformation);
                //        return;
                //    }
                //}

                //if (this.mblnLOCProblemOnLoad)
                //{
                //    if (ddlMLoc.SelectedIndex <= 0)
                //    {
                //        sMsg = "While Loading this notes information, the Master/Sub LOC originally assigned could not be located" + "\n" + "\n";
                //        sMsg = sMsg + "A Line of Credit has not been set for this loan and the assignemt will be removed if you save now" + "\n" + "\n";
                //        sMsg = sMsg + "Press YES to continue, NO to modify.";
                //        //if (MessageBox.Show(sMsg, "Problem with Line of Credit", vbCritical + vbYesNo, vbCritical + vbYesNo) == vbNo)
                //        //{
                //        //    return;
                //        //}
                //    }
                //    else
                //    {
                //    }
                //}

                //if (this.NoteType == "M")
                //{
                //    if (txtDateTabQotPost.Text != string.Empty)
                //    {
                //        sSql = "Select * from Property Where PNoteno = '" + this.NoteNumber + "' and PUnit <> '000' and PPDate is null";
                //        DataSet rsMasterSubPayoff = null;
                //        rsMasterSubPayoff = this.noteInfoPresenter.uspGetValidateProperty();
                //        if (rsMasterSubPayoff.IsValidDataSet())
                //        {
                //            //MessageBox.Show("There are Sub Notes attached to this Master Note which are not paid-off. You cannot input a Payoff Posted date until all Sub Notes are paid-off.", "Master\\Sub Note Payoff", vbOKOnly + vbInformation, vbOKOnly + vbInformation);
                //            //tbNoteInfo.ActiveTab = 2;
                //            txtDateTabQotPost.Focus();
                //            return;
                //        }
                //    }
                //}

                //till sp - ??????????????????????????????
                Dictionary<string, string> novinputParams = new Dictionary<string, string>();
                novinputParams.Add("PNOTE", this.NoteNumber);
                novinputParams.Add("PUNIT", this.UnitNumber);
                novinputParams.Add("ExitState", "YES");
                novinputParams.Add("EffDate", oldEffDate);
                novinputParams.Add("GLTable", string.Empty);
                novinputParams.Add("TranDate", DateTime.Now.ToString());
                novinputParams.Add("PNUMBER", this.BorrowerNo);
                novinputParams.Add("TYPE", string.Empty);
                novinputParams.Add("ICODE", string.Empty);
                novinputParams.Add("TRANCODE", string.Empty);
                novinputParams.Add("strUser", this.UserName);
                novinputParams.Add("EventSource", "NA");
                novinputParams.Add("BilledAdj", "0");
                novinputParams.Add("BookedAdj", "0");
                novinputParams.Add("UnbookedAdj", "0");
                novinputParams.Add("RecID", new CommonService("ACC04").RecordID().ToString());
                novinputParams.Add("EventType", "N");
                novinputParams.Add("blnNoGLImpact", "false");
                novinputParams.Add("bln_DailyAccrual", string.Empty);
                novinputParams.Add("blnNonAccrual", chkNonAccural.Checked.ToString());
                novinputParams.Add("SYSDATE", effDate);
                novinputParams.Add("Flag", string.Empty);
                //call nonaccural window.
                this.noteInfoPresenter.NonAccrualValidate(novinputParams);
                SaveNote();
                this.noteInfoPresenter.ShowMessageBox("Saved Successfully.");
                if (this.blnAUDITRPT == true)
                {
                    InvokeAuditReport();
                    return;
                }
                Response.Redirect("Notes.aspx");
            }
            catch (Exception ex)
            {

            }
        }

        private void InvokeAuditReport()
        {
            //calling sp
            string sMsg = string.Empty;
            bool bTax = false;
            bool bLOCMDate = false;
            bool bLOC = false;
            bool bLOCTerms = false;
            bool bLOCParentTotal = false;
            bool bLegalDesc = false;
            bool bIntProf = false;
            bool bBillOpt = false;
            bool bNLEqtProf = false;
            bool bBLEqtProf = false;
            string sMsgSubLMDate = string.Empty;
            string sMsgMasterLMDate = string.Empty;
            string sMsgLOC = string.Empty;
            string strBudget = string.Empty;
            bool blnShowReport = false;
            bool blnBorrowingBase = false;
            int nRow = 0;
            int cExceed = 0;
            int mcurParentTotalExceed = 0;
            string mstrTermsMsg = string.Empty;
            DataSet ds = null;

            if (this.NoteType == "B")
                blnBorrowingBase = true;

            if (this.blnAR_TAXID == true || this.blnAR_LDESC || this.blnAR_LOC || this.blnAR_LOCMAT || this.blnAR_INT || this.blnAR_BILLOPT || this.blnAR_BUD || this.blnAR_BUDPROF || this.blnAR_EQTPROF || this.blnAR_LOCPARENTTOTAL || this.blnAR_LOCTERMS)
            {
                Dictionary<string, string> dictAuditParams = new Dictionary<string, string>();
                dictAuditParams.Add("gsNoteType", this.NoteMode);
                dictAuditParams.Add("PNOTENO", this.NoteNumber);
                dictAuditParams.Add("PUNIT", this.UnitNumber);
                dictAuditParams.Add("bln_AR_TAXID", Convert.ToString(this.blnAR_TAXID));
                dictAuditParams.Add("bln_AR_LDESC", Convert.ToString(this.blnAR_LDESC));
                dictAuditParams.Add("bln_AR_LOC", Convert.ToString(this.blnAR_LOC));
                dictAuditParams.Add("bln_AR_LOCMAT", Convert.ToString(this.blnAR_LOCMAT));
                dictAuditParams.Add("bln_AR_INT", Convert.ToString(this.blnAR_INT));
                dictAuditParams.Add("bln_AR_BILLOPT", Convert.ToString(this.blnAR_BILLOPT));
                dictAuditParams.Add("bln_AR_BUD", Convert.ToString(this.blnAR_BUD));
                dictAuditParams.Add("bln_AR_BUDPROF", Convert.ToString(this.blnAR_BUDPROF));
                dictAuditParams.Add("bln_AR_EQTPROF", Convert.ToString(this.blnAR_EQTPROF));
                dictAuditParams.Add("bln_AR_LOCPARENTTOTAL", Convert.ToString(this.blnAR_LOCPARENTTOTAL));
                dictAuditParams.Add("bln_AR_LOCTERMS", Convert.ToString(this.blnAR_LOCTERMS));
                if (chkAmortizeLoan.Checked)
                    dictAuditParams.Add("chkNote4", Convert.ToString(true));
                else
                    dictAuditParams.Add("chkNote4", Convert.ToString(false));
                dictAuditParams.Add("cboNote13", ddlArea.SelectedValue);
                dictAuditParams.Add("cboNote14", ddlLocal.SelectedValue);
                dictAuditParams.Add("cboNote15", ddlMClass.SelectedValue);
                dictAuditParams.Add("cboNote16", ddlSClass.SelectedValue);
                dictAuditParams.Add("cboNote2", drpdwnlstState.SelectedValue);
                dictAuditParams.Add("cboNote4", ddlStatus.SelectedValue);
                using (DataSet dsAudit = this.noteInfoPresenter.GetAuditInfo(dictAuditParams))
                {

                    if (dsAudit.IsValidDataSet())
                    {
                        bTax = TCLHelper.ConvertToBool(TCLHelper.GetData(dsAudit.Tables[0].Rows[0]["bTax"]));
                        bLOCMDate = TCLHelper.ConvertToBool(TCLHelper.GetData(dsAudit.Tables[0].Rows[0]["bLOCMDate"]));
                        bLOC = TCLHelper.ConvertToBool(TCLHelper.GetData(dsAudit.Tables[0].Rows[0]["bLOC"]));
                        bLegalDesc = TCLHelper.ConvertToBool(TCLHelper.GetData(dsAudit.Tables[0].Rows[0]["bLegalDesc"]));
                        bIntProf = TCLHelper.ConvertToBool(TCLHelper.GetData(dsAudit.Tables[0].Rows[0]["bIntProf"]));
                        bBillOpt = TCLHelper.ConvertToBool(TCLHelper.GetData(dsAudit.Tables[0].Rows[0]["bBillOpt"]));
                        bNLEqtProf = TCLHelper.ConvertToBool(TCLHelper.GetData(dsAudit.Tables[0].Rows[0]["bNLEqtProf"]));
                        bBLEqtProf = TCLHelper.ConvertToBool(TCLHelper.GetData(dsAudit.Tables[0].Rows[0]["bBLEqtProf"]));
                        sMsgSubLMDate = TCLHelper.GetData(dsAudit.Tables[0].Rows[0]["sMsgSubLMDate"]);
                        sMsgMasterLMDate = TCLHelper.GetData(dsAudit.Tables[0].Rows[0]["sMsgMasterLMDate"]);
                        sMsgLOC = TCLHelper.GetData(dsAudit.Tables[0].Rows[0]["sMsgLOC"]);
                        bLOCTerms = TCLHelper.ConvertToBool(TCLHelper.GetData(dsAudit.Tables[0].Rows[0]["bLOCTerms"]));
                        bLOCParentTotal = TCLHelper.ConvertToBool(TCLHelper.GetData(dsAudit.Tables[0].Rows[0]["bLOCParentTotal"]));
                        mstrTermsMsg = TCLHelper.GetData(dsAudit.Tables[0].Rows[0]["mstrTermsMsg"]);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", "javascript:alert('No Audit Report options have been selected in the System Profile. Cannot perform Audit report.'", true);
                goto ExitAuditReport;
            }

            if (bTax)
            {
                sMsg = "Borrower TaxID is missing" + "^";
                blnShowReport = true;
            }
            if (bLOCMDate)
            {
                sMsg = sMsg + sMsgMasterLMDate + "^";
                blnShowReport = true;
            }
            if (bLOCMDate)
            {
                sMsg = sMsgSubLMDate + "^";
                blnShowReport = true;
            }
            if (bLOC)
            {
                sMsg = sMsg + "Sub Line of Credit Commitment exceeded by " + (cExceed).ToString("Currency") + "^";
                blnShowReport = true;
            }
            if (bLOCParentTotal)
            {
                sMsg = sMsg + "The sum of all Master Commitments, including Child records, exceed the Parent Total Line by " + (mcurParentTotalExceed).ToString("Currency") + "^";
            }
            if (bLOCTerms)
            {
                if (string.IsNullOrEmpty(mstrTermsMsg.Trim()) == false)
                {
                    sMsg = sMsg + mstrTermsMsg + "^";
                    blnShowReport = true;
                }
                if (mstrExceedSUBLOC != "")
                {
                    sMsg = sMsg + mstrExceedSUBLOC + "^";
                    blnShowReport = true;
                }
            }
            if (bLegalDesc && !blnBorrowingBase)
            {
                sMsg = sMsg + "Legal Description is missing" + "^";
                blnShowReport = true;
            }
            if (this.blnAR_BUD && ((this.cAlloc - this.cEst) != 0) && blnBorrowingBase == false)
            {
                sMsg = sMsg + "Budget Estimated " + TCLHelper.ConvertToUSCurrency(this.cEst) + " - Allocated " + TCLHelper.ConvertToUSCurrency(this.cAlloc) + "  =  " + TCLHelper.ConvertToUSCurrency(this.cDiff) + "^";
                blnShowReport = true;
            }
            if (bIntProf)
            {
                if (TCLHelper.Trim(sMsgLOC) == "")
                {
                    sMsg = sMsg + "Interest Profile or Interest Rate missing" + "^";
                }
                else
                {
                    sMsg = sMsg + sMsgLOC + "^";
                }
                blnShowReport = true;
            }
            if (bBillOpt)
            {
                if (string.IsNullOrEmpty(sMsgLOC) == true)
                {
                    sMsg = sMsg + "Interest Profile or Billing Cycle missing" + "^";
                }
                else
                {
                    sMsg = sMsg + sMsgLOC + "^";
                }
                blnShowReport = true;
            }
            if (bNLEqtProf)
            {
                sMsg = sMsg + "Note Level Equity Interest Profile missing or rate not found." + "^";
                blnShowReport = true;
            }

            if (bBLEqtProf)
            {
                ds = this.noteInfoPresenter.NoteEqtyProfGet();
                if (ds.IsValidDataSet())
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        sMsg = sMsg + TCLHelper.GetData(ds.Tables[0].Rows[i]["ErrMsg"]) + "^";
                    }
                }
            }

            if (blnShowReport)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Script", "WOpenAuditTrail('" + this.NoteNumber + "','" + this.UnitNumber + "' ,'" + sMsg + "');", true);
            }
        ExitAuditReport:
            return;
        }

        private void InvokeJavascriptFunction(string functionName)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "InVokeFunc", functionName, true);
        }

        public void CalcAllocCommit()
        {
            DataSet dsCalculate = this.noteInfoPresenter.CalcAllocCommit();
            bool mblnGenDrawBudget = false;

            if (dsCalculate.IsValidDataSet() == true)
            {
                foreach (DataRow drow in dsCalculate.Tables[0].Rows)
                {
                    txtAlctCommit.Text = TCLHelper.ConvertToUSCurrency(TCLHelper.ConvertToDouble(txtAlctCommit.Text) + (TCLHelper.ConvertToDouble(drow["BCCOST"]) - TCLHelper.ConvertToDouble(drow["BCEQUITY"])));
                    txtTotalAllocated.Text = TCLHelper.ConvertToUSCurrency(TCLHelper.ConvertToDouble(txtTotalAllocated.Text) + TCLHelper.ConvertToDouble(drow["BCCOST"]));
                }
            }
            else
            {
                DataSet adoBudget = this.noteInfoPresenter.GetBudget();
                if (adoBudget.IsValidDataSet())
                {
                    foreach (DataRow drow in adoBudget.Tables[0].Rows)
                    {
                        if ((TCLHelper.GetData(drow["BCID"]) + TCLHelper.GetData(drow["BCLV1"]) + TCLHelper.GetData(drow["BCLV2"])) == "9999900")
                        {
                            txtAlctCommit.Text = TCLHelper.ConvertToUSCurrency(TCLHelper.ConvertToDouble(txtAlctCommit.Text) + TCLHelper.ConvertToDouble(txtFincTabConstRslt.Text));
                            mblnGenDrawBudget = true;
                            txtTotalAllocated.Text = TCLHelper.ConvertToUSCurrency(TCLHelper.ConvertToDouble(txtTotalAllocated.Text) + TCLHelper.ConvertToDouble(txtFincTabConstRslt.Text));
                        }
                    }
                }
            }

            txtAlctCommit.Text = TCLHelper.ConvertToUSCurrency(TCLHelper.ConvertToDouble(txtAlctCommit.Text));
            txtTotalAllocated.Text = TCLHelper.ConvertToUSCurrency(TCLHelper.ConvertToDouble(txtTotalAllocated.Text));
        }

        public void SaveNote()
        {
            //blnLOCSEQ = false;
            //if (!Status_Begin("Saving All Note Information", "Selecting Note Record", 0, 5, false)) 
            //{
            //}
            //if (!Status_Refresh(1, "Saving Note Information")) {
            //}

            string sSql = string.Empty;
            ////if (chkNote[0].Tag==true)
            ////{
            //    //mGoSubStack.PutPosition(1); 
            //            //goto AddToSQLStmt; 
            //            //GoSubReturnPosition1:
            //    sSql = sSql+"PPTYPE = '"+(chkNote[0].Checked==false ? "N" : "Y")+"'";
            ////}

            //if (chkNote[1].Tag==true) 
            //{
            //if (chkNote[1].Checked!=Math.Abs(mnPrvNonAccrual)) 
            //{
            //    mGoSubStack.PutPosition(2); goto AddToSQLStmt; GoSubReturnPosition2:
            //    sSql = sSql+"PNOACCRUAL = "+(chkNote[1].Checked==false ? 0 : -1); // Field Can't Take Boolean Values
            //}
            //}

            //if (chkNote[2].Tag==true) {
            //if (chkNote[2].Checked!=Math.Abs(mnPrvInDefault)) {
            //mGoSubStack.PutPosition(3); goto AddToSQLStmt; GoSubReturnPosition3:
            //sSql = sSql+"PINDEFAULT = "+(chkNote[2].Checked==false ? 0 : -1); // Field Can't Take Boolean Values
            //}
            //}
            //if (chkNote[3].Tag==true) {
            //if (chkNote[3].Checked!=Math.Abs(mnPrvStopFinAct)) {
            //mGoSubStack.PutPosition(4); goto AddToSQLStmt; GoSubReturnPosition4:
            //sSql = sSql+"STOPPED = "+(chkNote[3].Checked==false ? 0 : -1);
            //}
            //}
            //if (chkNote[4].Tag==true) {
            //if (chkNote[4].Checked!=Math.Abs(mnPrvLPniSched)) {
            //mGoSubStack.PutPosition(5); goto AddToSQLStmt; GoSubReturnPosition5:
            //sSql = sSql+"LPNISCHED = "+(chkNote[4].Checked==false ? 0 : -1);
            //}
            //}
            //if (chkNote[5].Tag==true) {
            //if (chkNote[5].Checked!=Math.Abs(mnPrvFannieMae)) {
            //mGoSubStack.PutPosition(6); goto AddToSQLStmt; GoSubReturnPosition6:
            //sSql = sSql+"FANNIEMAE = "+(chkNote[5].Checked==false ? 0 : -1);
            //}
            //}
            //if (chkNote[6].Tag==true) {
            //if (chkNote[6].Checked!=Math.Abs(mnPrvForeclosure)) {
            //mGoSubStack.PutPosition(7); goto AddToSQLStmt; GoSubReturnPosition7:
            //sSql = sSql+"PFORECLOSURE = "+(chkNote[6].Checked==false ? 0 : -1);
            //}
            //}
            //if (chkNote[7].Tag==true) {
            //if (chkNote[7].Checked!=Math.Abs(mnPrvBankruptcy)) {
            //mGoSubStack.PutPosition(8); goto AddToSQLStmt; GoSubReturnPosition8:
            //sSql = sSql+"PBANKRUPTCY = "+(chkNote[7].Checked==false ? 0 : -1);
            //}
            //}
            //if (chkNote[8].Tag==true) {
            //if (chkNote[8].Checked!=Math.Abs(mnPrv203KIndicator)) {
            //mGoSubStack.PutPosition(9); goto AddToSQLStmt; GoSubReturnPosition9:
            //sSql = sSql+"LOANINDICATOR = "+(chkNote[8].Checked==false ? 0 : -1);
            //}
            //}
            //if (chkNote[9].Tag==true) {
            //if (chkNote[9].Checked!=Math.Abs(mnPrvRollPerm)) {
            //mGoSubStack.PutPosition(10); goto AddToSQLStmt; GoSubReturnPosition10:
            //sSql = sSql+"PROLLPERM = "+(chkNote[9].Checked==false ? 0 : -1);
            //}
            //}
            //if (chkNote[10].Tag==true) {
            //if (chkNote[10].Checked!=Math.Abs(mnPrvAssetMgt)) {
            //mGoSubStack.PutPosition(11); goto AddToSQLStmt; GoSubReturnPosition11:
            //sSql = sSql+"PASSETMGT = "+(chkNote[10].Checked==false ? 0 : -1);
            //}
            //}
            //if (chkNote[11].Tag==true) {
            //if (chkNote[11].Checked!=Math.Abs(mnPrvOriginalTerms)) {
            //mGoSubStack.PutPosition(12); goto AddToSQLStmt; GoSubReturnPosition12:
            //sSql = sSql+"PORIGTERMS = "+(chkNote[11].Checked==false ? 0 : -1);
            //}
            //}
            //if (chkNote[12].Tag==true) {
            //if (chkNote[12].Checked!=Math.Abs(mnPrvAutomaticRecast)) {
            //mGoSubStack.PutPosition(13); goto AddToSQLStmt; GoSubReturnPosition13:
            //sSql = sSql+"PAUTORECAST = "+(chkNote[12].Checked==false ? 0 : -1);
            //}
            //}
            //if (optAutoGen[0].Tag==true) {
            //mGoSubStack.PutPosition(14); goto AddToSQLStmt; GoSubReturnPosition14:
            //sSql = sSql+"AUTOGENBORFUNDS = 'D'";
            //}
            //if (optAutoGen[1].Tag==true) {
            //mGoSubStack.PutPosition(15); goto AddToSQLStmt; GoSubReturnPosition15:
            //sSql = sSql+"AUTOGENBORFUNDS = 'E'";
            //}
            //if (optAutoGen[2].Tag==true) {
            //mGoSubStack.PutPosition(16); goto AddToSQLStmt; GoSubReturnPosition16:
            //sSql = sSql+"AUTOGENBORFUNDS = ' '";
            //}
            //if (ChkOutside.Tag==true) {
            //mGoSubStack.PutPosition(17); goto AddToSQLStmt; GoSubReturnPosition17:
            //sSql = sSql+"OutsideLast = "+(ChkOutside.Checked==vbChecked ? "'Y'" : "''");
            //}
            //if (chkOmitBVInfo.Tag==true) {
            //mGoSubStack.PutPosition(18); goto AddToSQLStmt; GoSubReturnPosition18: // This Should Be VbUnchecked
            //sSql = sSql+"OmitBVOnWebInspFlag = "+(chkOmitBVInfo.Checked==false ? 0 : -1);
            //}
            //if (goSerialInfo.BorrowerPortal) {
            //if (chkDualApproval.Tag==true) {
            //mGoSubStack.PutPosition(19); goto AddToSQLStmt; GoSubReturnPosition19:
            //sSql = sSql+"DualApprovalFlag = "+(chkDualApproval.Checked==vbChecked ? "1" : "0");
            //}
            //} else {
            //}
            //if (!mblnComRuleNoUpdate &&  mblnComRuleChanged) { // Leave COM RULE Info UnChanged?
            //if (lblComRule.Tag!=mdblCommitmentRule) {
            //mGoSubStack.PutPosition(20); goto AddToSQLStmt; GoSubReturnPosition20:
            //sSql = sSql+"CommitmentRuleID = "+mdblCommitmentRule;
            //}
            //if (chkORComRule.Checked!=chkORComRule.Tag) {
            //mGoSubStack.PutPosition(21); goto AddToSQLStmt; GoSubReturnPosition21:
            //sSql = sSql+"RuleOverrideFlag = "+(mblnComRuleOverride ? -1 : 0);
            //}
            //}
            //sSql = StrUpdChgFld(mskNote[8], "PADDRESS", sSql, mskNote[8].MaxLength, Convert.ToInt32(false));
            //sSql = StrUpdChgFld(mskNote[9], "PCITY", sSql, mskNote[9].MaxLength, Convert.ToInt32(false));
            //sSql = StrUpdChgFld(mskNote[1], "PCOUNTY", sSql, mskNote[1].MaxLength, Convert.ToInt32(false));
            //sSql = StrUpdChgFld(mskNote[11], "PZIP", sSql, mskNote[11].MaxLength, Convert.ToInt32(false));
            //sSql = StrUpdChgFld(mskGPSCoords[1], "PGPSCoordinate1", sSql, mskGPSCoords[1].MaxLength, Convert.ToInt32(false));
            //sSql = StrUpdChgFld(mskGPSCoords[2], "PGPSCoordinate2", sSql, mskGPSCoords[2].MaxLength, Convert.ToInt32(false));
            //sSql = StrUpdChgFld(mskNote[5], "PLOT", sSql, mskNote[5].MaxLength, Convert.ToInt32(false));
            //sSql = StrUpdChgFld(mskNote[0], "PBLOCK", sSql, mskNote[0].MaxLength, Convert.ToInt32(false));
            //sSql = StrUpdChgFld(mskNote[3], "PSECTION", sSql, mskNote[3].MaxLength, Convert.ToInt32(false));
            //sSql = StrUpdChgFld(mskNote[6], "PPHASE", sSql, mskNote[6].MaxLength, Convert.ToInt32(false));
            //sSql = StrUpdChgFld(mskNote[10], "PSUBDIVIS", sSql, mskNote[10].MaxLength, Convert.ToInt32(false));
            //if (TCLHelper.Trim(mskNote[7])=="" && this.NoteType=="B") {
            //mskNote[7] = "Borrowing Base Note";
            //mskNote[7].Tag = true;
            //sSql = StrUpdChgFld(mskNote[7], "PLEGALDESC", sSql, mskNote[7].MaxLength, Convert.ToInt32(false));
            //} else {
            //sSql = StrUpdChgFld(mskNote[7], "PLEGALDESC", sSql, mskNote[7].MaxLength, Convert.ToInt32(false));
            //}
            //sSql = StrUpdChgFld(mskNote[12], "PCOTRACTOR", sSql, mskNote[12].MaxLength, Convert.ToInt32(false));
            //sSql = StrUpdChgFld(mskNote[13], "ACCOUNTID", sSql, mskNote[13].MaxLength, Convert.ToInt32(false));
            //sSql = StrUpdChgFld(mskNote[2], "PCTRACK", sSql, mskNote[2].MaxLength, Convert.ToInt32(false));
            //sSql = StrUpdChgFld(mskNote[4], "PCLNO", sSql, mskNote[4].MaxLength, Convert.ToInt32(false));
            //sSql = StrUpdChgFld(mskNote[14], "PSPECINS1", sSql, mskNote[14].MaxLength, Convert.ToInt32(false));
            //sSql = StrUpdChgFld(cboNote[0], "PGLTABLE", sSql, 3, Convert.ToInt32(false));
            //sSql = StrUpdChgFld(cboNote[2], "PSTATE", sSql, 2, Convert.ToInt32(false));
            //sSql = StrUpdChgFld(cboNote[12], "PCOUNTRY", sSql, 3, Convert.ToInt32(false));
            //sSql = StrUpdChgFld(cboNote[5], "PFEDCD", sSql, 4, Convert.ToInt32(false));
            //if (TCLHelper.Trim(gsPayoffDate)=="") { // For RFC - 29728
            //sSql = StrUpdChgFld(cboNote[4], "PSTATUS", sSql, 1, Convert.ToInt32(false));
            //} else {
            //sSql = StrUpdChgFld(cboNote[4], "X_PSTATUS", sSql, 1, Convert.ToInt32(false));
            //}
            //sSql = StrUpdChgFld(cboNote[7], "PLCLAS", sSql, 3, Convert.ToInt32(false));
            //sSql = StrUpdChgFld(cboNote[6], "PCSHEET", sSql, 3, Convert.ToInt32(false));
            //sSql = StrUpdChgFld(cboNote[8], "PLOANTYPE", sSql, 10, Convert.ToInt32(false));
            //sSql = StrUpdChgFld(cboNote[9], "PLOANPURPOSE", sSql, 10, Convert.ToInt32(false));
            //sSql = StrUpdChgFld(cboNote[10], "PLOANCOLLATERAL", sSql, 10, Convert.ToInt32(false));
            //sSql = StrUpdChgFld(cboNote[18], "PADMINISTRATOR", sSql, 10, Convert.ToInt32(false)); // 8.11.0 - RFC 15151
            //sSql = StrUpdChgFld(ssdDates[0], "PNDATE", sSql, 0, Convert.ToInt32(true));
            //sSql = StrUpdChgFld(ssdDates[1], "PMDATE", sSql, 0, Convert.ToInt32(true));
            //sSql = StrUpdChgFld(ssdDates[4], "PAPPLY", sSql, 0, Convert.ToInt32(true));
            //sSql = StrUpdChgFld(ssdDates[5], "PCLOSE", sSql, 0, Convert.ToInt32(true));
            //sSql = StrUpdChgFld(ssdDates[6], "PAPPROVAL", sSql, 0, Convert.ToInt32(true));
            //sSql = StrUpdChgFld(ssdDates[2], "PCDATE", sSql, 0, Convert.ToInt32(true));
            //sSql = StrUpdChgFld(ssdDates[3], "PSDATE", sSql, 0, Convert.ToInt32(true));
            //sSql = StrUpdChgFld(ssdDates[7], "PQDATE", sSql, 0, Convert.ToInt32(true));
            //sSql = StrUpdChgFld(ssdDates[8], "PPDATE", sSql, 0, Convert.ToInt32(true));
            //sSql = StrUpdChgFld(ssdDates[9], "CONVDATE", sSql, 0, Convert.ToInt32(true));
            //sSql = StrUpdChgFld(ssdDates[10], "RATELOCK", sSql, 0, Convert.ToInt32(true));
            //sSql = StrUpdChgFld(ssdDates[11], "LoanGradeDate", sSql, 0, Convert.ToInt32(true));
            //if (TCLHelper.Trim(gsPayoffDate)=="") {
            //mGoSubStack.PutPosition(22); goto AddToSQLStmt; GoSubReturnPosition22:
            //sSql = sSql+"X_PSTATUS = '' ";
            //}
            //if (mskFund.Tag==true) {
            //mGoSubStack.PutPosition(23); goto AddToSQLStmt; GoSubReturnPosition23:
            //sSql = sSql+"PPCNTFD = "+(mskFund).ToString("Fixed");
            //}
            //if (this.NoteType=="S" || this.NoteType=="D") {
            //if (mskTenant.Tag==true) {
            //mGoSubStack.PutPosition(24); goto AddToSQLStmt; GoSubReturnPosition24:
            //sSql = sSql+" TenantSpaceSF = "+TCLHelper.ConvertToInt(mskTenant.Text);
            //}
            //}
            //if (cboNote[1].Tag==true) {
            //mGoSubStack.PutPosition(25); goto AddToSQLStmt; GoSubReturnPosition25:
            //strWork = pubCBALCombo_GetValue(cboNote[1]); // RFC 28684     SaveNote()
            //sSql = sSql+"PCOMPANY = '"+SQLChar(strWork)+"'"; // RFC 28684     SaveNote()
            //}
            //if (cboNote[3].Tag==true) {
            //mGoSubStack.PutPosition(26); goto AddToSQLStmt; GoSubReturnPosition26:
            //strWork = pubCBALCombo_GetValue(cboNote[3]); // RFC 28684     SaveNote()
            //sSql = sSql+"PBRANCH = '"+SQLChar(strWork)+"'"; // RFC 28684     SaveNote()
            //}
            //Debug.WriteLine("NOTE: "+TCLHelper.Right(sSql, 45));
            //if (cboNote[13].Tag==true || cboNote[14].Tag==true) {
            //strWork = pubCBALCombo_GetValue(cboNote[13]); // AREA                          ' RFC 28684     SaveNote()
            //mGoSubStack.PutPosition(27); goto AddToSQLStmt; GoSubReturnPosition27: // AREA                          ' RFC 28684     SaveNote()
            //sSql = sSql+"PLOCMCLASS = '"+strWork+"'"; // AREA                          ' RFC 28684     SaveNote()
            //strWork2 = pubCBALCombo_GetValue(cboNote[14]); // LOCALE                        ' RFC 28684     SaveNote()
            //mGoSubStack.PutPosition(28); goto AddToSQLStmt; GoSubReturnPosition28: // LOCALE                        ' RFC 28684     SaveNote()
            //sSql = sSql+"PLOCSCLASS = '"+strWork2+"'"; // LOCALE                        ' RFC 28684     SaveNote()
            //if (strWork=="") { // NO M CLASS - NO PLOC              ' PLOC                          ' RFC 28684     SaveNote()
            //mGoSubStack.PutPosition(29); goto AddToSQLStmt; GoSubReturnPosition29: // PLOC                          ' RFC 28684     SaveNote()
            //sSql = sSql+"PLOC = ''"; // PLOC                          ' RFC 28684     SaveNote()
            //} else {						// PLOC                          ' RFC 28684     SaveNote()
            //mGoSubStack.PutPosition(30); goto AddToSQLStmt; GoSubReturnPosition30: // PLOC                          ' RFC 28684     SaveNote()
            //sSql = sSql+"PLOC = '"+strWork+strWork2+"'"; // PLOC                          ' RFC 28684     SaveNote()
            //} // PLOC                          ' RFC 28684     SaveNote()
            //}
            //if (cboNote[15].Tag==true || cboNote[16].Tag==true) {
            //if (TCLHelper.GetData(SelectedClass())=="0000") {
            //mGoSubStack.PutPosition(31); goto AddToSQLStmt; GoSubReturnPosition31:
            //sSql = sSql+"PCLASS = ''";
            //mGoSubStack.PutPosition(32); goto AddToSQLStmt; GoSubReturnPosition32:
            //sSql = sSql+"PPRJMCLASS = ''";
            //mGoSubStack.PutPosition(33); goto AddToSQLStmt; GoSubReturnPosition33:
            //sSql = sSql+"PPRJSCLASS = ''";
            //} else {
            //mGoSubStack.PutPosition(34); goto AddToSQLStmt; GoSubReturnPosition34:
            //sSql = sSql+"PCLASS = '"+SQLChar(SelectedClass())+"'";
            //mGoSubStack.PutPosition(35); goto AddToSQLStmt; GoSubReturnPosition35:
            //sSql = sSql+"PPRJMCLASS = '"+(TCLHelper.ConvertToInt(aClassMaj[cboNote[15].SelectedIndex])).ToString("00")+"'";
            //mGoSubStack.PutPosition(36); goto AddToSQLStmt; GoSubReturnPosition36:
            //sSql = sSql+"PPRJSCLASS = '"+(TCLHelper.ConvertToInt(aClassMin[cboNote[16].SelectedIndex])).ToString("00")+"'";
            //}
            //}
            //if (cboInspCom.Tag==true) {
            //mGoSubStack.PutPosition(37); goto AddToSQLStmt; GoSubReturnPosition37:
            //sSql = sSql+"COMPANYID= "+Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemData(cboInspCom, cboInspCom.SelectedIndex);
            //}
            //if (Convert.ToBoolean(bRenewal)) {
            //mGoSubStack.PutPosition(38); goto AddToSQLStmt; GoSubReturnPosition38:
            //sSql = sSql+"PRENEW = "+(nRenewalCount).ToString();
            //}
            //if (TCLHelper.ConvertToInt(aCreditMasterLines[cboNote[11].SelectedIndex])!=mdblPrvLOCMID) {
            //mGoSubStack.PutPosition(39); goto AddToSQLStmt; GoSubReturnPosition39:
            //sSql = sSql+"LOCMID = "+TCLHelper.ConvertToInt(aCreditMasterLines[cboNote[11].SelectedIndex])+"";
            //blnLOCSEQ = true;
            //}
            //if (aCreditSubLines[cboNote[17].SelectedIndex]!=sPrvLCSEQ) {
            //mGoSubStack.PutPosition(40); goto AddToSQLStmt; GoSubReturnPosition40:
            //sSql = sSql+"PLCSEQ = '"+SQLChar(aCreditSubLines[cboNote[17].SelectedIndex])+"'";
            //blnLOCSEQ = true;
            //}
            //if (TCLHelper.UCase(this.NoteMode)=="PIPELINE") {
            //mGoSubStack.PutPosition(41); goto AddToSQLStmt; GoSubReturnPosition41:
            //sSql = sSql+"PCLAMT = "+mskFin[10];
            //sSql = sSql+", PBDAMT = "+mskFin[11];
            //sSql = sSql+", PBEAMT = "+mskFin[12];
            //sSql = sSql+", PCCAMT = "+mskFin[13];
            //sSql = sSql+", PCOAMT = "+mskFin[14];
            //sSql = sSql+", POSSREQD = "+mskFin[15];
            //sSql = sSql+", PCLORG = "+mskFin[10];
            //sSql = sSql+", PBDORG = "+mskFin[11];
            //sSql = sSql+", PBEORG = "+mskFin[12];
            //sSql = sSql+", PCCORG = "+mskFin[13];
            //sSql = sSql+", PCOORG = "+mskFin[14];
            //sSql = sSql+", PCPAMT = "+mskFin[20];
            //} else {
            //if (this.UnitNumber=="000") {
            //mGoSubStack.PutPosition(42); goto AddToSQLStmt; GoSubReturnPosition42:
            //sSql = sSql+"PCLAMT = "+mskFin[10];
            //sSql = sSql+", PBDAMT = "+mskFin[11];
            //sSql = sSql+", PBEAMT = "+mskFin[12];
            //}
            //if (this.Action==1) {
            //mGoSubStack.PutPosition(43); goto AddToSQLStmt; GoSubReturnPosition43:
            //sSql = sSql+"PCLORG = "+mskFin[10];
            //sSql = sSql+", PBDORG = "+mskFin[11];
            //sSql = sSql+", PBEORG = "+mskFin[12];
            //sSql = sSql+", PCCORG = "+mskFin[13];
            //sSql = sSql+", PCOORG = "+mskFin[14];
            //}
            //if (mskFin[19]!=0) {
            //if (TCLHelper.InStr(1, "DMS", this.NoteType, vbTextCompare)>0) {
            //mGoSubStack.PutPosition(44); goto AddToSQLStmt; GoSubReturnPosition44:
            //sSql = sSql+"PCPAMT = "+mskFin[20];
            //}
            //}
            //}
            //if (goSerialInfo.BorrowingBase &&  this.NoteType=="B") {
            //if (chkUseInspPct.Tag==true) {
            //mGoSubStack.PutPosition(45); goto AddToSQLStmt; GoSubReturnPosition45:
            //sSql = Convert.ToString(sSql+"BBUseInspPctComplete = "+chkUseInspPct.Checked);
            //}
            //if (chkBBNewInPipeline.Tag==true) {
            //mGoSubStack.PutPosition(46); goto AddToSQLStmt; GoSubReturnPosition46:
            //sSql = Convert.ToString(sSql+"BBNewInPipeline = "+chkBBNewInPipeline.Checked);
            //}
            //if (VBtoConverter.CBool(ssdAuditLog[0].Tag) && (ssdAuditLog[0].Text!="")) {
            //if (!CompareDates(TCLHelper.ConvertToDateTime(adoSetupModTable.Fields["BBReconcileDate"].ToString()), "=", TCLHelper.ConvertToDateTime(ssdAuditLog[0].Text))) {
            //if (CompareDates(TCLHelper.ConvertToDateTime(adoSetupModTable.Fields["BBReconcileDate"].ToString()), ">", TCLHelper.ConvertToDateTime(ssdAuditLog[0].Text))) {
            //if (MessageBox.Show("The Reconcile Date entered is early than the previous date, are you sure you want to update it?", null, vbQuestion+vbYesNo, vbQuestion+vbYesNo)==vbYes) {
            //sSql = StrUpdChgFld(ssdAuditLog[0], "BBReconcileDate", sSql, 0, Convert.ToInt32(true));
            //} else {
            //ssdAuditLog[0].Tag = false;
            //ssdAuditLog[0].Text = TCLHelper.ConvertToDateTime(adoSetupModTable.Fields["BBReconcileDate"].ToString());
            //}
            //} else {
            //sSql = StrUpdChgFld(ssdAuditLog[0], "BBReconcileDate", sSql, 0, Convert.ToInt32(true));
            //}
            //} else {
            //ssdAuditLog[0].Tag = false;
            //ssdAuditLog[0].Text = "";
            //}
            //} else {
            //}
            //if (VBtoConverter.CBool(ssdAuditLog[1].Tag) && (ssdAuditLog[1].Text!="")) {
            //if (!CompareDates(TCLHelper.ConvertToDateTime(adoSetupModTable.Fields["BBAuditDate"].ToString()), "=", TCLHelper.ConvertToDateTime(ssdAuditLog[1].Text))) {
            //if (CompareDates(TCLHelper.ConvertToDateTime(adoSetupModTable.Fields["BBAuditDate"].ToString()), ">", TCLHelper.ConvertToDateTime(ssdAuditLog[1].Text))) {
            //if (MessageBox.Show("The Audit Date entered is early than the previous date, are you sure you want to update it?", null, vbQuestion+vbYesNo, vbQuestion+vbYesNo)==vbYes) {
            //sSql = StrUpdChgFld(ssdAuditLog[1], "BBAuditDate", sSql, 0, Convert.ToInt32(true));
            //} else {
            //ssdAuditLog[1].Tag = false;
            //ssdAuditLog[1].Text = TCLHelper.ConvertToDateTime(adoSetupModTable.Fields["BBAuditDate"].ToString());
            //}
            //} else {
            //sSql = StrUpdChgFld(ssdAuditLog[1], "BBAuditDate", sSql, 0, Convert.ToInt32(true));
            //}
            //} else {
            //ssdAuditLog[1].Tag = false;
            //ssdAuditLog[1].Text = "";
            //}
            //} else {
            //}
            //} else {
            //}
            //if (cmdVendor.Tag==true) {
            //mGoSubStack.PutPosition(47); goto AddToSQLStmt; GoSubReturnPosition47:
            //sSql = sSql+"PVENDOR = '"+SQLChar(msVendor)+"'";
            //}
            //Update_Now: ;
            //if (sSql!="") {
            //mGoSubStack.PutPosition(48); goto AddToSQLStmt; GoSubReturnPosition48:
            //sSql = sSql+"PTRAN = "+SQLDate(pubGetDateLocalized);
            //sSql = sSql+", PUSER = '"+gsUserName+"'";
            //sSql = "UPDATE "+this.NoteMode+" SET "+sSql;
            //sSql = sSql+" WHERE PNOTENO = '"+SQLChar(this.NoteNumber);
            //sSql = sSql+"' AND PUNIT = '"+SQLChar(this.UnitNumber)+"'";
            //nRows = SQLExec(false, sSql, goUserConnect.Driver);
            //strWork = pubCBALCombo_GetValue(cboNote[1]);
            //sSql = "ACOMPANY = '"+SQLChar(strWork)+"'"; // RFC 28684     SaveNote()
            //mGoSubStack.PutPosition(49); goto AddToSQLStmt; GoSubReturnPosition49:
            //strWork = pubCBALCombo_GetValue(cboNote[3]);
            //sSql = "ABRANCH = '"+SQLChar(strWork)+"'"; // RFC 28684     SaveNote()
            //Debug.WriteLine("ADV: "+TCLHelper.Right(sSql, 45));
            //sSql = "UPDATE ADVANCE SET "+sSql;
            //sSql = sSql+" WHERE ANOTE = '"+SQLChar(this.NoteNumber);
            //sSql = sSql+"' AND AUNIT = '"+SQLChar(this.UnitNumber)+"'";
            //nRows = SQLExec(false, sSql, goUserConnect.Driver);
            //Debug.WriteLine("UPD: "+sSql + Environment.NewLine + Conversion.Str(nRows));
            //}
            //DBCommit();
            this.noteInfoPresenter.SaveNote(FillSaveNoteValues());
            //**********************UPTO THIS CALL SAVENOTE****************************//

            if (this.bLOCIntProfAdd == true)
            {
                //if (adoINOTE.RecordCount==0) 
                //{
                this.noteInfoPresenter.AddLocIntProf(this.BorrowerNo, this.NoteNumber, this.UnitNumber, TCLHelper.ConvertToDouble(this.dblMasterLookupIntProf), this.sSubLookupIntProf, true, this.noteInfoPresenter.GetDateLocalized(false));
                //AddLOCIntProf(TCLHelper.GetData(adoSetupModTable("PBNUMBER")), dblMasterLookupIntProf, sSubLookupIntProf, TCLHelper.GetData(adoSetupModTable("PNOTENO")), TCLHelper.GetData(adoSetupModTable("PUNIT")), pubGetDateLocalized, true);
                //}
            }

            if (this.bLOCEqtProfAdd == true)
            {
                //if (rsEqtyProf.RecordCount==0) 
                //{ 
                this.noteInfoPresenter.AddLocEqtProf(this.BorrowerNo, this.NoteNumber, this.UnitNumber, TCLHelper.ConvertToDouble(this.dblMasterLookupIntProf), this.sSubLookupIntProf, true, this.noteInfoPresenter.GetDateLocalized(false));
                //AddLOCEqtProf(TCLHelper.GetData(adoSetupModTable("PBNUMBER")), dblMasterLookupIntProf, sSubLookupIntProf, TCLHelper.GetData(adoSetupModTable("PNOTENO")), TCLHelper.GetData(adoSetupModTable("PUNIT")), pubGetDateLocalized, true);
                //}
            }

            if (TCLHelper.UCase(this.NoteMode) == "PROPERTY" && (TCLHelper.ConvertToDouble(ddlMLoc.GetSelectedValue()) != TCLHelper.ConvertToDouble(this.dblMasterLookupIntProf) || TCLHelper.ConvertToDouble(ddlSLoc.GetSelectedValue()) != TCLHelper.ConvertToDouble(this.sSubLookupIntProf)))
            {
                DoSubs = Convert.ToInt32(false);
                if (this.UnitNumber == "000" && this.Action == 2)
                {
                    //if (MessageBox.Show("Do you want to attach all sub notes to this new Line-of-Credit?", null, vbCritical+vbYesNo, vbCritical+vbYesNo)==vbYes) 
                    //{
                    //DoSubs = Convert.ToInt32(true);
                    //} 
                    //else 
                    //{
                    //DoSubs = Convert.ToInt32(false);
                    //}
                }
            }

            //CALL SAVENOTETWO

            Dictionary<string, string> dictSaveNo2Params = new Dictionary<string, string>();
            dictSaveNo2Params.Add("gsNoteType", this.NoteMode);
            dictSaveNo2Params.Add("PBNUMBER", this.BorrowerNo);
            dictSaveNo2Params.Add("PNOTENO", this.NoteNumber);
            dictSaveNo2Params.Add("PUNIT", this.UnitNumber);
            dictSaveNo2Params.Add("mdblPrvLOCMID", this.dblMasterLookupIntProf);
            dictSaveNo2Params.Add("sPrvLCSEQ", this.sSubLookupIntProf);
            dictSaveNo2Params.Add("DoSubs", this.DoSubs.ToString());
            dictSaveNo2Params.Add("mskFin10", txtFincTabLoanCommit3.Text);
            dictSaveNo2Params.Add("prsamt_d", this.prsamt_d);
            dictSaveNo2Params.Add("pcfamt", this.pcfamt);
            dictSaveNo2Params.Add("PCLAMT_D", this.PCLAMT_D);
            dictSaveNo2Params.Add("LOCMID", ddlMLoc.GetSelectedValue());
            dictSaveNo2Params.Add("PLCSEQ", ddlSLoc.GetSelectedValue());
            dictSaveNo2Params.Add("mskFin3", txtFincTabLoanCommit2.Text);
            this.noteInfoPresenter.SaveNoteTwo(dictSaveNo2Params);

            //***************************CALL SP uspSaveNotetwo ********************************END HERE
            string sType = string.Empty;
            string sICode = string.Empty;
            string Memo = string.Empty;
            string sEqtyTranType = string.Empty;
            string Off = string.Empty;
            double Amt = 0;
            string vEff = string.Empty;
            string Tran = string.Empty;
            int TranNum = 0;
            int nRows = 0;
            if (TCLHelper.UCase(this.NoteMode) != "PIPELINE" && this.UnitNumber != "000")
            {
                if (this.Action == 1)
                {
                    sType = "2";
                }
                else
                {
                    sType = "4";
                }
                TranNum = 0;
                nRows = 3;
                int[] mskFin = null;
                //while (nRows < 6)
                //{
                //    if (mskFin[nRows] != 0)
                //    {
                //        if (TranNum != 0)
                //        {
                //            //TranNum = SetTranNum();
                //        }
                //        if (this.Action == 1)
                //        {
                //            switch (nRows)
                //            {
                //                case 3:
                //                    {
                //                        sICode = "N";
                //                        Memo = "Commitment Setup";
                //                        sEqtyTranType = "";
                //                        break;
                //                    }
                //                case 4:
                //                    {
                //                        sICode = "O";
                //                        Memo = "Note Level Deposit Setup";
                //                        sEqtyTranType = "D";
                //                        break;
                //                    }
                //                case 5:
                //                    {
                //                        sICode = "P";
                //                        Memo = "Note Level Escrow Setup";
                //                        sEqtyTranType = "D";
                //                        break;
                //                    }
                //            } //end switch
                //        }
                //        else
                //        {
                //            switch (nRows)
                //            {
                //                case 3:
                //                    {
                //                        sICode = "T";
                //                        Memo = "Commitment Adjustment";
                //                        sEqtyTranType = "";
                //                        break;
                //                    }
                //                case 4:
                //                    {
                //                        sICode = "Q";
                //                        Memo = "Note Level Deposit Adjustment";
                //                        sEqtyTranType = "D";
                //                        break;
                //                    }
                //                case 5:
                //                    {
                //                        sICode = "R";
                //                        Memo = "Note Level Escrow Adjustment";
                //                        sEqtyTranType = "D";
                //                        break;
                //                    }
                //            } //end switch
                //        }
                //        Off = "";
                //        Amt = mskFin[nRows];
                //        switch (nRows)
                //        {
                //            case 3:
                //                {
                //                    vEff = txtFincTabEffctDate1.Text; //ssdEffective[0].Text;
                //                    //Tran = cboTranCode[0].Tag;
                //                    break;
                //                }
                //            case 4:
                //                {
                //                    vEff = txtFincTabEffctDate1.Text;
                //                    //Tran = TCLHelper.Trim(cboTranCode[1].Tag);
                //                    break;
                //                }
                //            case 5:
                //                {
                //                    vEff = txtFincTabEffctDate1.Text;
                //                    //Tran = TCLHelper.Trim(cboTranCode[2].Tag);
                //                    break;
                //                }
                //        } //end switch

                //        //if (!PostTrans(this.NoteNumber, this.UnitNumber, sType, sICode, vEff, Off, Amt, Memo, "", "", "", Tran, TranNum, "", false, false, "", "", "", "", "", "", false, sEqtyTranType, "", "", 0, "", "", 0, gsUserName, false, false, ""))
                //        //{
                //        //MessageBox.Show("An error occured trying to post transaction detail for " + Memo);
                //        //}
                //    }

                //    nRows += 1;
                //}
            }

            //*************SAVENOTETHREE*********START
            Dictionary<string, string> inputParams = new Dictionary<string, string>();
            inputParams.Add("gsNoteType", this.NoteMode);
            inputParams.Add("PBNUMBER", this.BorrowerNo);
            inputParams.Add("PNOTENO", this.NoteNumber);
            inputParams.Add("PUNIT", this.UnitNumber);
            inputParams.Add("mskFin10", txtFincTabLoanCommit3.Text);
            inputParams.Add("pcfamt", this.pcfamt);
            inputParams.Add("PCLAMT_D", this.PCLAMT_D);
            inputParams.Add("mskFin9", txtFincTabConstAdjsmnt.Text);
            inputParams.Add("mskFin14", txtFincTabConstRslt.Text);
            inputParams.Add("chkNote8", ddlLoanType.GetSelectedText());
            inputParams.Add("bRenewal", this.bRenewal.ToString());//brenewal need to assign
            inputParams.Add("sRenewal", string.Empty);//need to assign
            inputParams.Add("lblLabel11", string.Empty);//BorrowerName
            inputParams.Add("ssdDates0", txtDateTabPrssNote.Text);
            inputParams.Add("ssdDates1", txtDateTabMturty.Text);
            inputParams.Add("dPrevMatDate", this.dPrevMatDate);//need to assign
            inputParams.Add("nRenewalCount", this.nRenewalCount.ToString());//need to assign
            inputParams.Add("mskNote7", txtPrjctShrtLglDesc.Text);
            inputParams.Add("gsUserName", this.UserName);
            inputParams.Add("chkNote2tag", "1");//need to assign
            inputParams.Add("chkNote2", chkInDefault.GetTCLValueInteger().ToString());
            inputParams.Add("mnPrvInDefault", string.Empty); //chkInDefault.GetTCLValue());//need to check
            inputParams.Add("srtnDate", this.noteInfoPresenter.GetDateLocalized(false));
            inputParams.Add("gsFDate", this.PFDATE);
            inputParams.Add("chkNote3tag", "1"); //need to assign
            inputParams.Add("chkNote3", chkStopFin.GetTCLValueInteger().ToString());
            inputParams.Add("mnPrvStopFinAct", string.Empty);
            inputParams.Add("TranNum", string.Empty); //need to assign values
            inputParams.Add("mskFin16", txtFincTabOutEqtyAdjstmnt.Text);
            inputParams.Add("mskFin8", txtFincTabTotlAdjsmnt.Text);
            inputParams.Add("RecID", new CommonService("ACC04").RecordID().ToString());
            string errormsg = string.Empty;
            noteInfoPresenter.SaveNotethree(inputParams, out errormsg);
            //CALL SAVENOTE THREE ENDS*******************************

            //Status_End();
            if (this.NoteType == "B")
            {
            }

            //if (goDraprof.bln_AUDITRPT)
            //{
            //AuditRpt();
            //}

            //Close();
            return;
        }

        public Dictionary<string, string> FillSaveNoteValues()
        {
            Dictionary<string, string> inputParams = new Dictionary<string, string>();
            inputParams.Add("gsNoteType", this.NoteMode);
            inputParams.Add("PNOTENO", this.NoteNumber);
            inputParams.Add("PUNIT", this.UnitNumber);
            inputParams.Add("PPTYPE", chkRevolving.GetTCLValueString());
            inputParams.Add("PNOACCRUAL", chkNonAccural.GetTCLValueInteger().ToString());
            inputParams.Add("PINDEFAULT", chkInDefault.GetTCLValueInteger().ToString());
            inputParams.Add("STOPPED", chkStopFin.GetTCLValueInteger().ToString());
            inputParams.Add("LPNISCHED", chkAmortizeLoan.GetTCLValueInteger().ToString());
            inputParams.Add("FANNIEMAE", chkFannieMae.GetTCLValueInteger().ToString());
            inputParams.Add("PFORECLOSURE", chkForeclosure.GetTCLValueInteger().ToString());
            inputParams.Add("PBANKRUPTCY", chkBankruptcy.GetTCLValueInteger().ToString());
            inputParams.Add("LOANINDICATOR", chk203K.GetTCLValueInteger().ToString());
            inputParams.Add("PROLLPERM", chkRollToPerm.GetTCLValueInteger().ToString());
            inputParams.Add("PASSETMGT", chkAssetManagementt.GetTCLValueInteger().ToString());
            inputParams.Add("PORIGTERMS", chkOriginal.GetTCLValueInteger().ToString());
            inputParams.Add("PAUTORECAST", chkAutomatic.GetTCLValueInteger().ToString());
            inputParams.Add("AUTOGENBORFUNDS", ddlAutoGenBrwFndFirst.GetSelectedValue());
            inputParams.Add("OutsideLast", chkOutside.GetTCLValueString());
            inputParams.Add("OmitBVOnWebInspFlag", chkOmit.GetTCLValueInteger().ToString());
            inputParams.Add("DualApprovalFlag", chkDualAprvl.GetTCLValueInteger().ToString());
            inputParams.Add("CommitmentRuleID", lblComRule.Text);
            inputParams.Add("RuleOverrideFlag", chkORComRule.GetTCLValueInteger().ToString());
            inputParams.Add("PADDRESS", txtPrjctStrtAddrs.Text);
            inputParams.Add("PCITY", txtPrjctCity.Text);
            inputParams.Add("PCOUNTY", txtPrjctCounty.Text);
            inputParams.Add("PZIP", txtPrjctZipCode.Text);
            inputParams.Add("PGPSCoordinate1", txtPrjctGPS.Text);
            inputParams.Add("PGPSCoordinate2", txtPrjctGPS1.Text);
            inputParams.Add("PLOT", txtPrjctLot.Text);
            inputParams.Add("PBLOCK", txtPrjctBlock.Text);
            inputParams.Add("PSECTION", txtPrjctSection.Text);
            inputParams.Add("PPHASE", txtPrjctPhase.Text);
            inputParams.Add("PSUBDIVIS", txtPrjctSubDiv.Text);
            inputParams.Add("PLEGALDESC", txtPrjctShrtLglDesc.Text);
            inputParams.Add("PCOTRACTOR", txtPrjctPrimryContrct.Text);
            inputParams.Add("ACCOUNTID", txtPrjctAccID.Text);
            inputParams.Add("PCTRACK", txtCensusTest.Text);
            inputParams.Add("PCLNO", txtCommitment.Text);
            inputParams.Add("PSPECINS1", txtPrjctPurchLoan.Text);
            inputParams.Add("PGLTABLE", ddlGLTable.GetSelectedText());
            inputParams.Add("PSTATE", drpdwnlstState.GetSelectedText());
            inputParams.Add("PCOUNTRY", drpdwnlstCountry.GetSelectedText());
            inputParams.Add("PFEDCD", ddlFederalCode.GetSelectedText());
            inputParams.Add("PSTATUS", ddlStatus.GetSelectedText());
            inputParams.Add("X_PSTATUS", ddlStatus.GetSelectedText());
            inputParams.Add("PLCLAS", ddlLoanClass.GetSelectedText());
            inputParams.Add("PCSHEET", ddlLoanGrade.GetSelectedText());
            inputParams.Add("PLOANTYPE", ddlLoanType.GetSelectedText());
            inputParams.Add("PLOANPURPOSE", ddlLoanPurpose.GetSelectedText());
            inputParams.Add("PLOANCOLLATERAL", ddlCollateralCode.GetSelectedText());
            inputParams.Add("PADMINISTRATOR", drpdwnlstAdmin.GetSelectedText());
            inputParams.Add("PNDATE", txtDateTabPrssNote.Text);
            inputParams.Add("PMDATE", txtDateTabMturty.Text);
            inputParams.Add("PAPPLY", txtDateTabApplc.Text);
            inputParams.Add("PCLOSE", txtDateTabClsng.Text);
            inputParams.Add("PAPPROVAL", txtDateTabApprvl.Text);
            inputParams.Add("PCDATE", txtDateTabCnsCmpl.Text);
            inputParams.Add("PSDATE", txtDateTabCnsStart.Text);
            inputParams.Add("PQDATE", txtDateTabQotPayOff.Text);
            inputParams.Add("PPDATE", txtDateTabQotPost.Text);
            inputParams.Add("CONVDATE", txtDateTabconvrs.Text);
            inputParams.Add("RATELOCK", txtDateTabRateLck.Text);
            inputParams.Add("LoanGradeDate", txtLoanGrdDate.Text);
            inputParams.Add("PPCNTFD", txtDateTabPrcntFund.Text);
            inputParams.Add("TenantSpaceSF", txtPrjctNetRntArea.Text);
            inputParams.Add("PCOMPANY", ddlCompany.GetSelectedValue());
            inputParams.Add("PBRANCH", ddlBranch.GetSelectedValue());
            inputParams.Add("PLOCMCLASS", ddlArea.GetSelectedValue());
            inputParams.Add("PLOCSCLASS", ddlLocal.GetSelectedValue());
            inputParams.Add("PLOC", ddlArea.GetSelectedText());

            string mClass = ddlMClass.GetSelectedValue();
            string sClass = ddlSClass.GetSelectedValue();
            if (string.IsNullOrEmpty(sClass) == false) mClass = mClass + sClass;
            inputParams.Add("PCLASS", mClass);

            inputParams.Add("PPRJMCLASS", ddlMClass.GetSelectedValue());
            inputParams.Add("PPRJSCLASS", ddlSClass.GetSelectedValue());
            inputParams.Add("COMPANYID", ddlInspCompany.GetSelectedValue());

            inputParams.Add("PRENEW", this.bRenewal.ToString());

            inputParams.Add("LOCMID", ddlMLoc.GetSelectedValue());
            inputParams.Add("PLCSEQ", ddlSLoc.GetSelectedValue());
            inputParams.Add("PCLAMT", txtFincTabLoanCommit3.Text);
            inputParams.Add("PBDAMT", txtFincTabLnLvlDpst3.Text);
            inputParams.Add("PBEAMT", txtFincTabLnLvlEscrw3.Text);
            inputParams.Add("PCCAMT", txtFincTabTotlRslt.Text);
            inputParams.Add("PCOAMT", txtFincTabConstRslt.Text);
            inputParams.Add("POSSREQD", txtFincTabOutEqtyRslt.Text);
            inputParams.Add("PCLORG", txtFincTabLoanCommit3.Text);
            inputParams.Add("PBDORG", txtFincTabLnLvlDpst3.Text);
            inputParams.Add("PBEORG", txtFincTabLnLvlEscrw3.Text);
            inputParams.Add("PCCORG", txtFincTabTotlRslt.Text);
            inputParams.Add("PCOORG", txtFincTabConstRslt.Text);
            inputParams.Add("PCPAMT", txtFincTabprincBal3.Text);
            inputParams.Add("BBUseInspPctComplete", "");
            inputParams.Add("BBNewInPipeline", "");
            inputParams.Add("BBReconcileDate", "");
            inputParams.Add("BBAuditDate", "");
            inputParams.Add("PVENDOR", "");
            inputParams.Add("PTRAN", "");
            inputParams.Add("PUSER", "");
            inputParams.Add("GNOTEACTION", Convert.ToString(this.Action));
            return inputParams;
        }

        protected void btnSaveDoc_Click(object sender, EventArgs e)
        {
            NewBorrwerPresenter borrowerPresenter = new NewBorrwerPresenter(this);
            try
            {
                int intExists = 0;
                Dictionary<string, string> inputParams = new Dictionary<string, string>();
                inputParams.Add("doc2note", this.NoteNumber);
                inputParams.Add("doc2unit", this.UnitNumber);
                inputParams.Add("doc2id", Convert.ToString(hdnDocId.Value).Trim());
                inputParams.Add("doc2item", Convert.ToString(txtDocGroupId.Text).Trim());
                inputParams.Add("doc2line", "00");
                inputParams.Add("doc1desc", Convert.ToString(txtDocDesc.Text));
                inputParams.Add("doc2pri", txtPri.Text);
                inputParams.Add("doc2days", txtBorDays.Text);
                inputParams.Add("doc2ldate", DBNull.Value.ToString());
                inputParams.Add("doc2ddate", DBNull.Value.ToString());
                inputParams.Add("doc2tran", string.Empty);
                inputParams.Add("doc2user", string.Empty);
                inputParams.Add("vnumber", string.Empty);
                inputParams.Add("flag", (3).ToString());
                inputParams.Add("exists", (-1).ToString());
                borrowerPresenter.uspDocInsert(inputParams, out intExists);
                if (intExists == 1)
                {
                    mpopupDocAdd.Show();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Script", "alert('Group ID already exists, cannot be added.');", true);
                    return;
                }
                mpopupDocAdd.Hide();
                noteInfoPresenter = new NoteInfoPresenter(this);
                this.noteInfoPresenter.LoadDocAttach();
                lstbArtchBrwDocTrack.DataBind();
                imgbtnDocTracEdit.Visible = false;
                imgbtnDocTracAdd.Visible = false;

                string strNewDocId = Convert.ToString(hdnDocId.Value).Trim() + Convert.ToString(txtDocGroupId.Text).Trim();
                sessionProvider.Set("DOCTRACKDOCID", strNewDocId);
                sessionProvider.Set("DOCTRACKNAME", Convert.ToString(txtDocDesc.Text));

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Script", "WDocOpen();", true);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, Level.Error);

            }
        }

        protected void btnDocCancel_Click(object sender, EventArgs e)
        {

        }
        protected void imgbtnDocTracEdit_Click(object sender, ImageClickEventArgs e)
        {
            sessionProvider.Set("DOCTRACKDOCID", Convert.ToString(hdnDocId.Value).Trim());
            sessionProvider.Set("DOCTRACKNAME", Convert.ToString(txtDocDesc.Text));
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Script", "WDocOpen();", true);
        }

        protected void imgbtnDocTracAdd_Click(object sender, ImageClickEventArgs e)
        {
            lblDocNoteNo.Text = this.NoteNumber;
            lblDocUnit.Text = this.UnitNumber;
            txtDocDesc.Text = string.Empty;
            txtPri.Text = string.Empty;
            txtBorDays.Text = string.Empty;
            lblDocId.Text = TCLHelper.GetData(ViewState["DocId"]);
            int intGroupId = 1;
            using (DataSet dsDocMaxId = noteInfoPresenter.GetDocumentMaxId(this.NoteNumber, this.UnitNumber, Convert.ToString(hdnDocId.Value), 1))
            {
                if (TCLHelper.IsValidDataColumn(dsDocMaxId))
                {
                    if (dsDocMaxId.Tables[0].Rows.Count > 0)
                        intGroupId = TCLHelper.ConvertToInt(dsDocMaxId.Tables[0].Rows[0]["MAXITEM"]) + 1;

                }
            }
            txtDocGroupId.Text = intGroupId.ToString("D2");
            mpopupDocAdd.Show();
        }
        #endregion

        private void DisableAllControls()
        {
            foreach (AjaxControlToolkit.TabPanel tabPanel in TabNoteInfo.Tabs)
            {
                foreach (System.Web.UI.Control control in tabPanel.Controls)
                {
                    Disable(control);
                }
            }
        }

        private void Disable(System.Web.UI.Control control)
        {
            if (control.Controls.Count > 0)
            {
                foreach (System.Web.UI.Control ctrl in control.Controls)
                {
                    Disable(ctrl);
                }
            }
            else
            {
                if (control.GetType().GetProperty("Enabled") != null)
                {
                    control.GetType().GetProperty("Enabled").SetValue(control, false, null);
                }
            }
        }
        protected void gvAttachments_GridPaging(object sender, EventArgs e)
        {
            iPageNumber = gvAttachments.PageNumber;
            iRowsPerPage = gvAttachments.RowsPerPage;
            BindBorrowerAttachments();
        }

        protected void CGIntrestProfile_GridRowEditing(object sender, EventArgs e)
        {
            try
            {
                string method = "MOD";
                string source = "INOTE";
                string effectiveDate = string.Empty;

                GridViewRow gvr = ((TCL.Control.CustomGrid.ExtendGrid)(sender)).SelectedRow;
                effectiveDate = ((LinkButton)gvr.Cells[2].Controls[0]).Text;
                sessionProvider.Set("IEFFDATE", effectiveDate);

                int intAmort = chkAmortizeLoan.Checked == true ? 1 : 0;
                int intDefault = chkInDefault.Checked == true ? 1 : 0;
                string script = "LoadIPEQWindow('" + this.NoteNumber + "', '" + this.UnitNumber + "', '" + method + "', '" + source + "', '" + this.BorrowerNo + "', '" + this.LCSeq + "', '" + this.dblLOCMID + "', '" + this.mProp_Inquiry + "', '" + intAmort + "', '" + intDefault + "')";
                InvokeJavascriptFunction(script);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, Level.Error);
                throw;
            }
        }
        protected void CGEquityProfile_GridRowEditing(object sender, EventArgs e)
        {
            try
            {
                string method = "MOD";
                string source = "EQTYPROF";
                string effectiveDate = string.Empty;
                string equityType = string.Empty;
                string budgetID = string.Empty;

                GridViewRow gvr = ((TCL.Control.CustomGrid.ExtendGrid)(sender)).SelectedRow;
                effectiveDate = ((LinkButton)gvr.Cells[5].Controls[0]).Text;
                sessionProvider.Set("IEFFDATE", effectiveDate);
                equityType = gvr.Cells[4].Text;
                budgetID = gvr.Cells[3].Text;

                string script = "LoadIPEQWindow('" + this.NoteNumber + "', '" + this.UnitNumber + "', '" + method + "', '" + source + "', '" + this.BorrowerNo + "', '" + this.LCSeq + "', '" + this.dblLOCMID + "', '" + this.mProp_Inquiry + "', '" + equityType + "', '" + budgetID + "')";
                InvokeJavascriptFunction(script);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, Level.Error);
                throw;
            }
        }

        protected void btnTabIntrstprflTranches_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Modules/DailyProcessing/TrancheTieredPricing.aspx?Mode=Note&Pnoteno=" + this.NoteNumber + "&punit=" + this.UnitNumber + "");

        }
        protected void btnHideKeyContects_Click(object sender, EventArgs e)
        {
            txtFindContact.Text = string.Empty;
            BindKeyContacts();

        }

    }
}
