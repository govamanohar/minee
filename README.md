<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Common/Site.Master" AutoEventWireup="true"
    CodeBehind="NotesInfo.aspx.cs" Inherits="ISGN.TCL.Web.Modules.DailyProcessing.NotesInfo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="ISGN.TCL.Control.CustomGrid" Namespace="ISGN.TCL.Control.CustomGrid"
    TagPrefix="CustomGid" %>
<%@ Register Assembly="ISGN.TCL.Web.UI.Controls" Namespace="ISGN.TCL.Web.UI.Controls"
    TagPrefix="asp1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../Styles/tab.css" rel="stylesheet" type="text/css" />
    <link type="text/css" href="../../Styles/tclstlyes.css" rel="stylesheet" />
    <link href="../../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link type="text/css" media="all" href="../../Styles/jsDatePick_ltr.min.css" rel="stylesheet" />

    <script type="text/javascript" src="../../Scripts/jsDatePick.min.1.3.js"></script>

    <script type="text/javascript" src="../../Scripts/includes.js"></script>

    <script type="text/javascript" src="../../Scripts/jquery-ui.js"></script>

    <script type="text/javascript" src="../../Scripts/jquery.dateentry.js"></script>

    <script src="/Scripts/CustomDataGrid.js" type="text/javascript"></script>

    <script src="../../Scripts/NoteInfo.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../Scripts/jquery.constrain.js"></script>

    <link href="../../Styles/FloatingStyle.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript">
        var FincTabEffctDate1 = '<%= txtFincTabEffctDate1.ClientID %>';
        var FincTabEffctDate2 = '<%= txtFincTabEffctDate2.ClientID %>';
        var FincTabEffctDate3 = '<%= txtFincTabEffctDate3.ClientID %>';

        var FincTabLoanCommit1 = '<%= txtFincTabLoanCommit1.ClientID %>';
        var FincTabLoanCommit2 = '<%= txtFincTabLoanCommit2.ClientID %>';
        var FincTabLoanCommit3 = '<%= txtFincTabLoanCommit3.ClientID %>';

        var FincTabprincBal1 = '<%= txtFincTabprincBal1.ClientID %>';
        var FincTabprincBal2 = '<%= txtFincTabprincBal2.ClientID %>';
        var FincTabprincBal3 = '<%= txtFincTabprincBal3.ClientID %>';

        var FincTabLnLvlDpst1 = '<%= txtFincTabLnLvlDpst1.ClientID %>';
        var FincTabLnLvlDpst2 = '<%= txtFincTabLnLvlDpst2.ClientID %>';
        var FincTabLnLvlDpst3 = '<%= txtFincTabLnLvlDpst3.ClientID %>';

        var FincTabLnLvlEscrw1 = '<%= txtFincTabLnLvlEscrw1.ClientID %>';
        var FincTabLnLvlEscrw2 = '<%= txtFincTabLnLvlEscrw2.ClientID %>';
        var FincTabLnLvlEscrw3 = '<%= txtFincTabLnLvlEscrw3.ClientID %>';

        var FincTabOutEqtyCurrnt = '<%= txtFincTabOutEqtyCurrnt.ClientID %>'
        var FincTabOutEqtyAdjstmnt = '<%= txtFincTabOutEqtyAdjstmnt.ClientID %>'
        var FincTabOutEqtyRslt = '<%= txtFincTabOutEqtyRslt.ClientID %>'

        var FincTabTotlCurnt = '<%= txtFincTabTotlCurnt.ClientID %>'
        var FincTabTotlAdjsmnt = '<%= txtFincTabTotlAdjsmnt.ClientID %>'
        var FincTabTotlRslt = '<%= txtFincTabTotlRslt.ClientID %>'
        var ItemID = '<%= hdnItemID.ClientID %>'
        var FincTabConstCurrnt = '<%= txtFincTabConstCurrnt.ClientID %>';
        var FincTabConstAdjsmnt = '<%= txtFincTabConstAdjsmnt.ClientID %>';
        var FincTabConstRslt = '<%= txtFincTabConstRslt.ClientID %>';
        var IPEffDate = '<%= hdnIPEffDate.ClientID %>';
        var EPEffDate = '<%= hdnEPEffDate.ClientID %>';

        function messageParse(text) {
            //var obj = $.parseJSON(text);
            if (text.length > 0) {
                for (var i = 0; i < text.length; i++) {
                    alert(text[i]);
                }
            }
        }

        function Open() {
            window.showModalDialog("ReleaseScheduleMulti.aspx", "popup_window", "dialogWidth:970px;dialogHeight:700px;")
        }
        function OpenSingle() {

            window.showModalDialog("ReleaseScheduleUpdate.aspx", "popup_window", "dialogWidth:970px;dialogHeight:700px;")

        }

        function WOpen(type, value) {

            var BNo;
            var AuditDate;
            var url;

            if (type == 'udf' || type == 'sc' || type == 'li')
                BNo = $("#<%= hdnBorrowerNo.ClientID %>").val();
            if (type == 'bbu') {
                switch (value) {
                    case '1':
                        AuditDate = $("#<%= txtAuditLog2.ClientID %>").val();
                        url = 'BBUnitAuditLog.aspx?Category=' + value + '&AuditDate=' + AuditDate.replace("/", "-");
                        break;
                    case '3':
                        AuditDate = $("#<%= txtAuditLog1.ClientID %>").val();
                        url = 'BBUnitAuditLog.aspx?Category=' + value + '&AuditDate=' + AuditDate.replace("/", "-");
                        break;
                    case '2':
                        url = 'BBUnitAuditLog.aspx?Category=' + value;
                        break;
                    default:
                        url = 'BBUnitAuditLog.aspx?RecId=' + value;
                        break;
                }

            }
            if (type == 'ip') {
                if (!confirm('This note is part of a Line of Credit that has its own Interest Profiles, create unit profiles anyway?')) {
                    return;
                }
            }

            var vRetrunValue;
            switch (type) {
                case 'udf':
                    var value = 'Note';
                    window.showModalDialog('UserDefinedFieldNoteInfo.aspx?NB=' + BNo + '&ModeBorr=' + value + '&time=' + new Date().getTime(), '', 'center:yes; modal:yes; edge:Raised; resizable:no;scrollbars:no;menubar=no;status:no;dialogWidth:770px;dialogHeight:400px;');
                    break;
                case 'ai':
                    window.showModalDialog('ApprisalInformation.aspx?time=' + new Date().getTime(), '', 'center:yes; modal:yes; edge:Raised; resizable:no;scrollbars:no;menubar=no;status:no;dialogWidth:1000px;dialogHeight:700px;');
                    break;
                case 'sc':
                    window.showModalDialog('SalescontractInfoNoteInfo.aspx?BNo=' + BNo + '&time=' + new Date().getTime(), '', 'center:yes; modal:yes; edge:Raised; resizable:no;scrollbars:no;menubar=no;status:no;dialogWidth:800px;dialogHeight:600px;');
                    break;
                case 'li':
                    var vli = $("#<%= txtPrjctNetRntArea.ClientID %>").val();
                    var shortDesc = $("#<%= txtPrjctShrtLglDesc.ClientID %>").val();
                    window.showModalDialog('TenantSpaceInfoNote.aspx?id=' + vli + '&BNo=' + BNo + '&time=' + new Date().getTime() + "&ShortDesc=" + shortDesc, '', 'center:yes; modal:yes; edge:Raised; resizable:no;scrollbars:no;menubar=no;status:no;dialogWidth:800px;dialogHeight:600px;');
                    break;
                case 'ip':
                    window.showModalDialog('LineCrdtIntrstPrflNoteinfo.aspx?time=' + new Date().getTime(), '', 'center:yes; modal:yes; edge:Raised; resizable:no;scrollbars:no;menubar=no;status:no;dialogWidth:700px;dialogHeight:500px;');
                    break;
                case 'bbu':
                    window.showModalDialog(url + '&time=' + new Date().getTime(), '', 'center:yes; modal:yes; edge:Raised; resizable:no;scrollbars:no;menubar=no;status:no;dialogWidth:700px;dialogHeight:500px;');
                    $('#<%=btnRedirectUnits.ClientID%>').click();
                    break;
                case 'bbmu':
                    window.showModalDialog('UnitEditor.aspx?time=' + new Date().getTime() + '&Mode=Note', '', 'center:yes; modal:yes; edge:Raised; resizable:no;scrollbars:no;menubar=no;status:no;dialogWidth:700px;dialogHeight:500px;');
                    break;
                case 'bbmt':
                    window.showModalDialog('NotesBB_terms.aspx?time=' + new Date().getTime() + '&Mode=Note', '', 'center:yes; modal:yes; edge:Raised; resizable:no;scrollbars:no;menubar=no;status:no;dialogWidth:980px;dialogHeight:700px;');
                    try { $('#<%=btnRedirectTerms.ClientID%>').click(); } catch (e) { }
                    break;
                case 'bbuf':
                    window.showModalDialog('BBUnitFilter.aspx?time=' + new Date().getTime(), '', 'center:yes; modal:yes; edge:Raised; resizable:no;scrollbars:no;menubar=no;status:no;dialogWidth:700px;dialogHeight:500px;');
                    $('#<%=btnRedirectUnits.ClientID%>').click();
                    break;
                case 'TEEdit':
                    vRetrunValue = window.showModalDialog('NotesBB_terms.aspx?Edit=' + value + '&time=' + new Date().getTime(), '', 'center:yes; modal:yes; edge:Raised; resizable:no;scrollbars:no;menubar=no;status:no;dialogWidth:980px;dialogHeight:700px;');
                    try { $('#<%=btnRedirectTerms.ClientID%>').click(); } catch (e) { }
                    break;
                case 'doc':
                    window.showModalDialog('DocUpdate.aspx?time=' + new Date().getTime(), '', 'center:yes; modal:yes; edge:Raised; resizable:no;scrollbars:no;menubar=no;status:no;dialogWidth:700px;dialogHeight:500px;');

                    break;

            }
            if (vRetrunValue == 'undefined' || vRetrunValue == null) {
                return false;
            }
            else {

                //vRetrunValue                    
            }
        }
        //if INOTE amort, indefault else eqTyp, bdgId
        function LoadIPEQWindow(noteNo, unitNo, method, source, borrNo, locseq, locmId, inquiry, amort, indefault) {
            var vRetrunValue;
            if (source == "INOTE") {
                vRetrunValue = window.showModalDialog("LineCrdtIntrstPrflNoteinfo.aspx?noteNo=" + noteNo + "&unitNo=" + unitNo + "&method=" + method + "&source=" + source + "&borrNo=" + borrNo + "&locseq=" + locseq + "&locmId=" + locmId + "&inquiry=" + inquiry + "&am=" + amort + "&in=" + indefault, '', 'center:yes; modal:yes; edge:Raised; resizable:no;scrollbars:no;menubar=no;status:no;dialogWidth:1000px;dialogHeight:600px;');
            }
            else {
                vRetrunValue = window.showModalDialog("EquityprofileNoteInfo.aspx?noteNo=" + noteNo + "&unitNo=" + unitNo + "&method=" + method + "&source=" + source + "&borrNo=" + borrNo + "&locseq=" + locseq + "&locmId=" + locmId + "&inquiry=" + inquiry + "&eqTyp=" + amort + "&bdgId=" + indefault, '', 'center:yes; modal:yes; edge:Raised; resizable:no;scrollbars:no;menubar=no;status:no;dialogWidth:1000px;dialogHeight:600px;');
            }
            if (vRetrunValue == 'undefined' || vRetrunValue == null) {
                return false;
            }
            else {
                if (source == "INOTE") {
                    $('#<%=btnIPTemp.ClientID%>').click();
                    return false;
                }
                else {
                    $('#<%=btnEPTemp.ClientID%>').click();
                    return false;
                }
            }
        }
        function WBOpen() {
            var vRetrunValue;
            vRetrunValue = window.showModalDialog('BdgtItems Noteinfo.aspx?time=' + new Date().getTime(), '', 'center:yes; modal:yes; edge:Raised; resizable:no;scrollbars:no;menubar=no;status:no;dialogWidth:1200px;dialogHeight:500px;');
            if (vRetrunValue == 'undefined' || vRetrunValue == null) {
                return false;
            }
            else {
                $('#<%=btnEditBudget.ClientID%>').click();

            }
        }

        function WDocOpen() {
            var vRetrunValue;
            vRetrunValue = window.showModalDialog('DocUpdate.aspx?scname=note&time=' + new Date().getTime(), '', 'center:yes; modal:yes; edge:Raised; resizable:no;scrollbars:no;menubar=no;status:no;dialogWidth:850px;dialogHeight:550px;');
            if (vRetrunValue == 'undefined' || vRetrunValue == null) {
                return false;
            }
            //            else {
            //                $('#<%=btnEditBudget.ClientID%>').click();

            //            }
        }

        function wNotePayOffOpen(value, type, Mode) {
            var vRetrunValue;
            vRetrunValue = window.showModalDialog('RulesEdit.aspx?Type=' + type + '&Mode=' + Mode + '&RuleID=' + value + '&time=' + new Date().getTime(), '', 'center:yes; modal:yes; edge:Raised; resizable:no;scrollbars:no;menubar=no;status:no;dialogWidth:850px;dialogHeight:500px;');
            if (vRetrunValue == 'undefined' || vRetrunValue == null) {
                return false;
            }
            else {
                if (type == '1') {
                    $('#<%=btnNotePayOff.ClientID%>').click();
                }
                else if (type == '2') {
                    $('#<%= btnReleaseRules.ClientID%>').click();
                }
            }
        }

        function IsGroupEmpty() {
            var vGroup = $("#<%= txtGroupDesc.ClientID %>").val();
            if (vGroup.trim() == '') {
                alert('Please enter a description.');
                return false;
            }
            return true;
        }


        function ConfirmCancel() {
            if (confirm('Are you sure you want to Cancel?') == true) {
                return true;
            }
            else {
                return false;
            }
        }

        function ConfirmCheck(e) {
            if (document.getElementById(ItemID).value == "") {
                alert('Please select a record to delete.');
                return false;
            }
            else {
                if (confirm('Are you sure you want to delete this release schedule?') == true) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        function DeleteNotePayOff(e) {
            var result = confirm(e);
            if (result == true) {
                $('#<%=btnNPODelete.ClientID%>').click();
            }
            else {
                return false;
            }
        }

        function DeleteNoteReleasePayOff(e) {
            var result = confirm(e);
            if (result == true) {
                $('#<%=btnRelsRulesDelete.ClientID%>').click();
            }
            else {
                return false;
            }
        }        

        function ValidateIP() {
            if (document.getElementById(IPEffDate).value == "") {
                alert('Please select a record to delete.');
                return false;
            }
            else {
                if (confirm("This Will Remove the Highlighted Interest Profile") == true) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        function ValidateCopyIP() {
            if (document.getElementById(IPEffDate).value == "") {
                alert('Please select a record to copy.');
                return false;
            }
            else {
                return true;
            }
        }

        function ValidateCopyEP() {
            if (document.getElementById(EPEffDate).value == "") {
                alert('Please select a record to copy.');
                return false;
            }
            else {
                return true;
            }
        }

        function ValidateEP() {
            if (document.getElementById(EPEffDate).value == "") {
                alert('Please select a record to delete.');
                return false;
            }
            else {
                if (confirm("This Will Delete the Highlighted Profile as well as all Duplicate Profiles.  This Operation is Not Recommened.") == true) {
                    return true;
                }

                else {
                    return false;
                }
            }
        }

        function LoadHandler() {
            datpick();
            DatePicker();
        }

        function removeSpecialCharacter(value) {
            if (value.length > 0) {
                var RetValue = value.replace('$', '');
                return RetValue = RetValue.replace(/,/g, '');
            }
            else {
                return value;
            }
        }

        function Commmitment() {

            var vLoanCommitment = $("#<%= txtFincTabLoanCommit3.ClientID %>").val();
            vLoanCommitment = removeSpecialCharacter(vLoanCommitment);
            var decimalval = vLoanCommitment.split(".")[1];

            if (decimalval == null) decimalval = ".00"
            else {
                decimalval = "." + decimalval;
                vLoanCommitment = vLoanCommitment.split(".")[0];
            }
            vLoanCommitment = formatInteger(vLoanCommitment, '###,###,###,###');
            if (vLoanCommitment != '')
                vLoanCommitment = '$' + vLoanCommitment + decimalval;
            $("input[id$='txtBudgetCommitment']").val(vLoanCommitment);
        }

        function TotalEstimated() {

            var vLoanCommitment = $("#<%= txtFincTabTotlRslt.ClientID %>").val();
            vLoanCommitment = removeSpecialCharacter(vLoanCommitment);
            var decimalval = vLoanCommitment.split(".")[1];

            if (decimalval == null) decimalval = ".00"
            else {
                decimalval = "." + decimalval;
                vLoanCommitment = vLoanCommitment.split(".")[0];
            }
            vLoanCommitment = formatInteger(vLoanCommitment, '###,###,###,###');
            if (vLoanCommitment != '')
                vLoanCommitment = '$' + vLoanCommitment + decimalval;
            $("input[id$='txtTotalEstimated']").val(vLoanCommitment);
        }
        function clientActiveTabChanged(sender, args) {

            if (sender.get_activeTabIndex() == 4) {
                if (document.getElementById('<%= hdnBudgetTemp.ClientID %>') != null) {
                    document.getElementById('<%= hdnBudgetTemp.ClientID %>').value = "1";
                    $('#<%=btnSelectBudget.ClientID%>').click();
                }
            }
            //if (sender.get_activeTabIndex() == 10) {
            $('#<%=btnHideKeyContects.ClientID%>').click();

            // }
        }

        function formatInteger(integer, pattern) {

            var result = '';
            integerIndex = integer.length - 1;
            patternIndex = pattern.length - 1;

            while ((integerIndex >= 0) && (patternIndex >= 0)) {
                var digit = integer.charAt(integerIndex);
                integerIndex--;

                // Skip non-digits from the source integer (eradicate current formatting).
                if ((digit < '0') || (digit > '9')) continue;

                // Got a digit from the integer, now plug it into the pattern.
                while (patternIndex >= 0) {
                    var patternChar = pattern.charAt(patternIndex);
                    patternIndex--;

                    // Substitute digits for '#' chars, treat other chars literally.
                    if (digit == '.')
                        break;
                    else if (patternChar == '#') {
                        result = digit + result;
                        break;
                    }
                    else {
                        result = patternChar + result;
                    }
                }
            }

            return result;
        }

        function calculateAdjustment(text, TextValue1, textValue3) {

            var textbox1 = removeSpecialCharacter(document.getElementById(TextValue1).value);
            var textbox3 = removeSpecialCharacter(document.getElementById(textValue3).value);
            var textbox2 = removeSpecialCharacter(text.value);
            if (textbox2.length > 0) {
                textbox3 = parseFloat(textbox1) + parseFloat(textbox2);
                textbox3 = parseFloat(textbox3);
            }
            else {
                textbox3 = parseFloat(textbox1);
                textbox2 = parseFloat("0");
            }
            text.value = dollars(parseFloat(textbox2));
            document.getElementById(textValue3).value = dollars(textbox3);
            //            document.getElementById(BudgetCommitment).value = document.getElementById(textValue3).value;
        }

        function dollars(num) {
            var string = parseFloat(num).toFixed(2);
            var parts = string.split('.');
            var cents = parts.pop();
            var dollars = parts.shift();
            if (num != undefined && num != null && num != '') {
                dollars = dollars.replace(/(\d{1,2}?)((\d{3})+)$/, "$1,$2");
                dollars = dollars.replace(/(\d{3})(?=\d)/g, "$1,");
            }
            return '$' + dollars + '.' + cents;
        }

        function calculateResult(text, TextValue1, TextValue2) {

            var textbox1 = removeSpecialCharacter(document.getElementById(TextValue1).value);
            var textbox2 = removeSpecialCharacter(document.getElementById(TextValue2).value);
            var textbox3 = removeSpecialCharacter(text.value);
            textbox2 = parseFloat(textbox3) - parseFloat(textbox1);
            textbox2 = parseFloat(textbox2);
            if (textbox3.length == 0) {
                textbox3 = parseFloat("0");
                textbox2 = parseFloat(textbox3) - parseFloat(textbox1);
                textbox2 = parseFloat(textbox2);
            }
            text.value = dollars(parseFloat(textbox3));
            document.getElementById(TextValue2).value = dollars(textbox2);
            //            document.getElementById(BudgetCommitment).value = textbox3;
        }


        function CheckBudget() {
            var CValue = document.getElementById('<%=ddlBudgets.ClientID %>');
            var OValue = document.getElementById('<%=hdnBudget.ClientID %>').value;
            if (!confirm('This will Remove any Currently Attached Budgets and Add the Current Selection.?')) {
                CValue.value = OValue
                return false;
            }
            else {
                $('#<%=btnSelectBudget.ClientID%>').click();

            };
        }

        function DeleteBudget(e) {

            var result = confirm(e);
            if (result == true) {
                $('#<%=btnDeleteBudget.ClientID%>').click();
            }
            else {
                return false;
            }
        }

        function CheckNoteProjectSave() {
            var ddlAdmin = $('#<%=drpdwnlstAdmin.ClientID %>').val();
            if (ddlAdmin == '') {
                $('#<%=tblMsg.ClientID %>').show();
                $('#<%=ErrorFail.ClientID %>').show();
                $('#<%=ErrorSuccess.ClientID %>').hide();
                $('#<%=lblErrorFail.ClientID %>').text('Please Assign the Administrator associated for this record');
                return false;
            }
            else {
                $('#<%=tblMsg.ClientID %>').hide();
                $('#<%=ErrorFail.ClientID %>').hide();
                $('#<%=lblErrorFail.ClientID %>').text('');
                if (confirm("Are you sure you want to Save?") == true) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        $(function textboxformat() {
            $(".constrain-limit").constrain({
                limit: { "p": 1, "\\": 4 }
            });

            $(".constrain-prohibitalpha").constrain({
                prohibit: { regex: "[a-zA-Z]" }
            });

            $(".constrain-prohibitalphachars").constrain({
                prohibit: { chars: "aeioaAEIOU" }
            });

            $(".constrain-allowalpha").constrain({
                allow: { regex: "[a-zA-Z]" }
            });

            $(".constrain-allowalphachars").constrain({
                allow: { chars: "aeioaAEIOU" }
            });

            $(".double").numeric({ format: "0.0" });

            $(".double-keyup").numeric({ format: "0.00", onblur: false });

            $(".integer").numeric();

        });

        function SelectVendor() {
            var Return;
            Return = window.showModalDialog("VendorSearch.aspx", "popup_window", "dialogWidth:670px;dialogHeight:600px;")

            if (Return != null)
                $("#<%=txtPrjctPrimryContrct.ClientID %>").val(Return.VName);
            return false;
        }

        function WVopen() {
            var vRetrunValue;
            if (document.getElementById('<%=ddlBudgets.ClientID %>').value == 0) {
                alert('Please Select a Budget Group.');
                document.getElementById('<%=ddlBudgets.ClientID %>').focus();
                return false;

            }
            else {
                vRetrunValue = window.showModalDialog('MultiVendor.aspx?time=' + new Date().getTime(), '', 'center:yes; modal:yes; edge:Raised; resizable:no;scrollbars:no;menubar=no;status:no;dialogWidth:1000px;dialogHeight:500px;');
                if (vRetrunValue == 'undefined' || vRetrunValue == null) {
                    return false;
                }
                else {
                    return true;

                }
            }
        }


        $(document).ready(function() {
            setDivWidth();
        });
        function setDivWidth() {
            var divWidth = (GetWidth() - 20);
            if (document.getElementById("pnlInterestGrid") != null) {
                document.getElementById("pnlInterestGrid").style.width = divWidth + "px";
            }
            if (document.getElementById("pnlEquityGrid") != null) {
                document.getElementById("pnlEquityGrid").style.width = divWidth + "px";
            }
        }
        function GetWidth() {
            var x = 0;
            if (self.innerHeight) {
                x = self.innerWidth;
            }
            else if (document.documentElement && document.documentElement.clientHeight) {
                x = document.documentElement.clientWidth;
            }
            else if (document.body) {
                x = document.body.clientWidth;
            }
            return x;
        }
        function checkTextAreaMaxLength(textBox, e, length) {

            var maxLength = parseInt(length);
            if (!checkSpecialKeys(e)) {
                if (textBox.value.length > maxLength - 1) {
                    if (window.event)//IE
                    {
                        e.returnValue = false;
                        return false;
                    }
                    else//Firefox
                        e.preventDefault();
                }
            }
        }
        function checkSpecialKeys(e) {
            if (e.keyCode != 8 && e.keyCode != 46 && e.keyCode != 35 && e.keyCode != 36 && e.keyCode != 37 && e.keyCode != 38 && e.keyCode != 39 && e.keyCode != 40)
                return false;
            else
                return true;
        }  
    </script>

    <style type="text/css">
        .modalBackground
        {
            position: absolute;
            background: url(tint20.png) 0 0 repeat;
            background: rgba(0,0,0,0.2);
            border-radius: 14px;
            padding: 8px;
        }
        .pnlBackGround
        {
            border-radius: 8px;
            background: #fff;
            padding: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Maincontent" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel runat="server" ID="upanel">
        <ContentTemplate>
            <table border="0" width="100%" id="tblMsg" runat="server" cellpadding="5" cellspacing="5"
                style="display: none">
                <tr id="ErrorSuccess" runat="server" style="display: none">
                    <td align="left" valign="middle" class="ErrorSuccess">
                        <asp:Label ID="lblErrorSuccess" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="ErrorFail" runat="server" style="display: none">
                    <td align="left" valign="middle" class="ErrorFail">
                        <asp:Label ID="lblErrorFail" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <table align="center" width="100%" cellspacing="0" cellpadding="0" border="0" class="allborder">
                <tr>
                    <td align="left" valign="top">
                        <table border="0" cellspacing="0" cellpadding="0" width="100%" class="allborder">
                            <tr>
                                <td align="left" valign="top" class="gridheaderbg">
                                    <table border="0" cellspacing="0" cellpadding="5" align="left" width="100%">
                                        <tr>
                                            <td>
                                                Edit:
                                                <asp:Label ID="lblBrwName" runat="server" CssClass="blackbfont"></asp:Label>&nbsp;
                                                <asp:ImageButton ID="imgbtnBrwinfo" runat="server" ImageAlign="Middle" ImageUrl="~/Images/Info.gif"
                                                    AlternateText="BrowerInfo" ToolTip="BrwInfo" />
                                            </td>
                                        </tr>
                                        <%--<tr>
                                            <td align="left" valign="middle">
                                                <asp:Panel ID="pnlbrwinformation" runat="server" class="pnlBackGround">
                                                    <div>
                                                        <table width="100%" align="right">
                                                            <tr>
                                                                <td>
                                                                    <asp:ImageButton ID="imgbtnClosepopup" runat="server" AlternateText="Close" ToolTip="Close"
                                                                        ImageUrl="~/Images/ClosePopup.gif" ImageAlign="Right" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div>
                                                        <table border="0" cellspacing="5" cellpadding="0" align="center" width="100%" class="FilterContent">
                                                            <tr>
                                                                <td width="8%" align="left" valign="middle" class="blucolor blackbfont">
                                                                    Name
                                                                </td>
                                                                <td align="left" valign="middle" width="1%">
                                                                    &nbsp;
                                                                </td>
                                                                <td width="20%" align="left" valign="middle">
                                                                    <asp:Label ID="lblBrwName1" runat="server"></asp:Label>
                                                                </td>
                                                                <td width="10%" align="left" valign="middle" class="blucolor blackbfont">
                                                                    Number
                                                                </td>
                                                                <td width="1%" align="left" valign="middle">
                                                                </td>
                                                                <td width="20%" align="left" valign="middle">
                                                                    <asp:Label ID="lblBrwNumber" runat="server"></asp:Label>
                                                                </td>
                                                                <td width="10%" align="left" valign="middle" class="blucolor blackbfont">
                                                                    Legal Desc
                                                                </td>
                                                                <td width="1%" align="left" valign="middle">
                                                                </td>
                                                                <td width="*" align="left" valign="middle">
                                                                    <asp:Label ID="lblLglDesc" runat="server" Text='<%# ProjectShortLglDesc %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" valign="middle">
                                                                    &nbsp;
                                                                </td>
                                                                <td align="left" valign="middle">
                                                                          <asp:Label ID="lblBrwName3" runat="server"></asp:Label>
                                                                </td>
                                                                <td align="left" valign="middle">
                                                                    <asp:Label ID="lblBrwName2" runat="server"></asp:Label>
                                                                </td>
                                                                <td align="left" valign="middle" class="blucolor blackbfont">
                                                                    Telephone
                                                                </td>
                                                                <td align="left" valign="middle">
                                                                </td>
                                                                <td align="left" valign="middle">
                                                                    <asp:Label ID="lblBrwTelNo" runat="server"></asp:Label>
                                                                </td>
                                                                <td align="left" valign="middle" class="blucolor blackbfont">
                                                                    Tax ID
                                                                </td>
                                                                <td align="left" valign="middle">
                                                                </td>
                                                                <td align="left" valign="middle">
                                                                    <asp:Label ID="lblBrwTaxId" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </asp:Panel>
                                            </td>
                                        </tr>--%>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table width="100%" class="allborder" cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <%-- <asp:UpdatePanel runat="server" ID="upanel">
                                <ContentTemplate>--%>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td>
                                                <asp:TabContainer ID="TabNoteInfo" runat="server" ActiveTabIndex="6" CssClass="Tab"
                                                    OnClientActiveTabChanged="clientActiveTabChanged">
                                                    <asp:TabPanel ID="tabPnlProject" runat="server">
                                                        <HeaderTemplate>
                                                            Project</HeaderTemplate>
                                                        <ContentTemplate>
                                                            <table width="100%" class="allgborder" cellpadding="1" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td align="left" valign="top">
                                                                        <table width="100%" align="left" cellpadding="0" cellspacing="0" border="0">
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" valign="top">
                                                                        <table width="100%" align="center" cellpadding="5" cellspacing="0" border="0">
                                                                            <tr>
                                                                                <td width="100%" align="left" valign="top" class="bgcolor6">
                                                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                        <tr>
                                                                                            <td width="52%">
                                                                                                <table width="96%" border="0" align="center" cellpadding="0" cellspacing="8" class="allborder">
                                                                                                    <asp:Panel ID="pnlPurchAdd" runat="server">
                                                                                                        <tr height="24">
                                                                                                            <td align="left" valign="middle" class="blackbfont bgcolor4 pl10" colspan="3">
                                                                                                                Property Address
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td width="29%" align="left" valign="middle" class="blucolor blackbfont">
                                                                                                                Street Address
                                                                                                            </td>
                                                                                                            <td width="1%" align="left" valign="middle">
                                                                                                            </td>
                                                                                                            <td width="*" align="left" valign="middle">
                                                                                                                <asp1:TCLTextBox Text='<%# ProjectStreetAddress %>' ID="txtPrjctStrtAddrs" runat="server"
                                                                                                                    CssClass="txtbox" MaxLength="35" BindingName="ProjectStreetAddress">
                                                                                                                </asp1:TCLTextBox>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                                                City
                                                                                                            </td>
                                                                                                            <td align="left" valign="middle">
                                                                                                            </td>
                                                                                                            <td align="left" valign="middle">
                                                                                                                <asp1:TCLTextBox Text='<%# ProjectCity %>' ID="txtPrjctCity" runat="server" CssClass="txtbox"
                                                                                                                    MaxLength="25" BindingName="ProjectCity"></asp1:TCLTextBox>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                                                County
                                                                                                            </td>
                                                                                                            <td align="left" valign="middle">
                                                                                                            </td>
                                                                                                            <td align="left" valign="middle">
                                                                                                                <asp1:TCLTextBox Text='<%# ProjectCounty %>' ID="txtPrjctCounty" runat="server" CssClass="txtbox"
                                                                                                                    MaxLength="25" BindingName="ProjectCounty"></asp1:TCLTextBox>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                                                State
                                                                                                            </td>
                                                                                                            <td align="left" valign="middle">
                                                                                                            </td>
                                                                                                            <td align="left" valign="middle">
                                                                                                                <asp1:TCLDropDownList ID="drpdwnlstState" runat="server" CssClass="ddlistbox" DataSource='<%# DictStates %>'
                                                                                                                    DataTextField="Value" DataValueField="Key" SetSelectedValue='<%# StateSelectedValue %>'>
                                                                                                                </asp1:TCLDropDownList>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                                                Zip Code
                                                                                                            </td>
                                                                                                            <td align="left" valign="middle">
                                                                                                            </td>
                                                                                                            <td align="left" valign="middle">
                                                                                                                <asp:TextBox Text='<%# ProjectZipCode %>' ID="txtPrjctZipCode" runat="server" CssClass="txtbox"
                                                                                                                    MaxLength="10" BindingName="ProjectZipCode" />
                                                                                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" TargetControlID="txtPrjctZipCode"
                                                                                                                    FilterType="Numbers, Custom" ValidChars="-" Enabled="True" />
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                                                Country
                                                                                                            </td>
                                                                                                            <td align="left" valign="middle">
                                                                                                            </td>
                                                                                                            <td align="left" valign="middle">
                                                                                                                <asp:DropDownList DataSource='<%# DictCountry %>' ID="drpdwnlstCountry" runat="server"
                                                                                                                    CssClass="ddlistbox" DataTextField="Value" DataValueField="Key">
                                                                                                                </asp:DropDownList>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </asp:Panel>
                                                                                                    <tr>
                                                                                                        <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                                            Short legal Description
                                                                                                        </td>
                                                                                                        <td align="left" valign="middle">
                                                                                                        </td>
                                                                                                        <td align="left" valign="middle">
                                                                                                            <asp1:TCLTextBox Text='<%# ProjectShortLglDesc %>' ID="txtPrjctShrtLglDesc" runat="server"
                                                                                                                CssClass="txtbox" MaxLength="45" BindingName="ProjectShortLglDesc"></asp1:TCLTextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <asp:Panel ID="pnlSubDivision" runat="server">
                                                                                                        <tr>
                                                                                                            <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                                                Sub Division
                                                                                                            </td>
                                                                                                            <td align="left" valign="middle">
                                                                                                            </td>
                                                                                                            <td align="left" valign="middle">
                                                                                                                <asp1:TCLTextBox Text='<%# ProjectSubDivision %>' ID="txtPrjctSubDiv" runat="server"
                                                                                                                    CssClass="txtbox" MaxLength="25" BindingName="ProjectSubDivision"></asp1:TCLTextBox>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </asp:Panel>
                                                                                                    <tr>
                                                                                                        <td colspan="3" align="left" valign="middle">
                                                                                                            <table width="100%" border="0" class="allborder" align="center" cellpadding="0" cellspacing="8">
                                                                                                                <tr height="24">
                                                                                                                    <td colspan="4" align="left" valign="middle" class="blackbfont bgcolor4 pl10">
                                                                                                                        Purchase Of Loan
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                                                        Purpose Of Loan
                                                                                                                    </td>
                                                                                                                    <td align="left" valign="middle">
                                                                                                                    </td>
                                                                                                                    <td width="40%" align="left" valign="middle">
                                                                                                                        <asp1:TCLTextBox Text='<%# ProjectPurchaseOfLoan %>' ID="txtPrjctPurchLoan" runat="server"
                                                                                                                            CssClass="txtboxbig" MaxLength="60" BindingName="ProjectPurchaseOfLoan"></asp1:TCLTextBox>
                                                                                                                    </td>
                                                                                                                    <td width="34%" align="left" valign="middle">
                                                                                                                        <asp:Button ID="btnPrjctUseDfndfld" runat="server" Text="User Defined Field" CssClass="btnstylebig"
                                                                                                                            OnClientClick="return WOpen('udf','');" />
                                                                                                                        <asp:HiddenField ID="hdnBorrowerNo" runat="server" />
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="3" align="left" valign="middle">
                                                                                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <table width="100%" border="0" class="allborder" align="center" cellpadding="0" cellspacing="8">
                                                                                                                            <asp:Panel ID="pnlPrimaryContractor" runat="server">
                                                                                                                                <tr height="24">
                                                                                                                                    <td colspan="4" align="left" valign="middle" class="blackbfont bgcolor4 pl10">
                                                                                                                                        Primary Contractor
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                                <tr>
                                                                                                                                    <td width="5%" align="left" valign="middle" class="blucolor blackbfont">
                                                                                                                                        Vendor
                                                                                                                                    </td>
                                                                                                                                    <td width="20%" align="left" valign="middle">
                                                                                                                                        <asp:TextBox Text='<%# ProjectPrimaryContract %>' ID="txtPrjctPrimryContrct" runat="server"
                                                                                                                                            CssClass="txtbox" ReadOnly="True"></asp:TextBox>&nbsp;
                                                                                                                                        <asp:ImageButton ID="imgbtnPrjctVendor" runat="server" AlternateText="Vendor" ToolTip="VendorSearch"
                                                                                                                                            ImageAlign="Middle" ImageUrl="~/Images/Vendor.png" OnClientClick="javascript:return SelectVendor()" />
                                                                                                                                    </td>
                                                                                                                                    <td width="1%" align="left" valign="middle">
                                                                                                                                    </td>
                                                                                                                                    <td width="10%" align="left" valign="middle">
                                                                                                                                        <asp:CheckBox ID="chkDualAprvl" runat="server" Text="Dual Approval Required for Draw Requests"
                                                                                                                                            Checked='<%# ProjectChkDualApproval %>' Visible='<%# ProjectChkDualApprovalVisible %>'
                                                                                                                                            Enabled='<%# ProjectChkDualApprovalEnabled %>' />
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                            </asp:Panel>
                                                                                                                            <tr>
                                                                                                                                <td width="20%" colspan="4" align="left" valign="middle">
                                                                                                                                    <asp:Label ID="lblBorrowingBaseMsg" runat="server" CssClass="lblRight" Text="Most information on the 'Project' Tab is not applicable in Borrowing Base Loans. Fields maintained for optional 'User' input." />
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                        </table>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                            <td width="29%" valign="top">
                                                                                                <table>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <table align="center" border="0" cellpadding="0" cellspacing="8" class="allborder"
                                                                                                                width="100%">
                                                                                                                <asp:Panel ID="pnlGps" runat="server">
                                                                                                                    <tr height="24">
                                                                                                                        <td align="left" class="blackbfont bgcolor4" colspan="3" valign="middle">
                                                                                                                            GPS Coordinates
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td align="left" valign="middle" width="19%" class="blucolor blackbfont">
                                                                                                                            GPS
                                                                                                                        </td>
                                                                                                                        <td align="left" valign="middle" width="4%">
                                                                                                                        </td>
                                                                                                                        <td align="left" valign="middle" width="77%">
                                                                                                                            <asp1:TCLTextBox Text='<%# ProjectGPS %>' ID="txtPrjctGPS" runat="server" CssClass="txtbox"
                                                                                                                                MaxLength="15" BindingName="ProjectGPS"></asp1:TCLTextBox>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td align="left" valign="middle" width="19%" class="blucolor blackbfont">
                                                                                                                        </td>
                                                                                                                        <td align="left" valign="middle" width="4%">
                                                                                                                        </td>
                                                                                                                        <td align="left" valign="middle" width="77%">
                                                                                                                            <asp1:TCLTextBox Text='<%# ProjectGPS1 %>' ID="txtPrjctGPS1" runat="server" CssClass="txtbox"
                                                                                                                                MaxLength="15" BindingName="ProjectGPS1"></asp1:TCLTextBox>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </asp:Panel>
                                                                                                                <tr>
                                                                                                                    <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                                                        Account ID
                                                                                                                    </td>
                                                                                                                    <td align="left" valign="middle">
                                                                                                                    </td>
                                                                                                                    <td align="left" valign="middle">
                                                                                                                        <asp1:TCLTextBox Text='<%# ProjectAccId %>' ID="txtPrjctAccID" runat="server" CssClass="txtbox"
                                                                                                                            MaxLength="5" BindingName="ProjectAccId"></asp1:TCLTextBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                                                        Administrator
                                                                                                                    </td>
                                                                                                                    <td align="left" valign="middle">
                                                                                                                    </td>
                                                                                                                    <td align="left" valign="middle">
                                                                                                                        <asp1:TCLDropDownList ID="drpdwnlstAdmin" runat="server" CssClass="ddlistbox" DataSource='<%# DictProjectAdmin %>'
                                                                                                                            DataTextField="Value" DataValueField="Key" SetSelectedValue='<%# DictProjectAdminSelectedValue %>'>
                                                                                                                        </asp1:TCLDropDownList>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <asp:Panel ID="pnlLotSec" runat="server">
                                                                                                                    <tr>
                                                                                                                        <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                                                            Lot
                                                                                                                        </td>
                                                                                                                        <td align="left" valign="middle">
                                                                                                                        </td>
                                                                                                                        <td align="left" valign="middle">
                                                                                                                            <asp1:TCLTextBox Text='<%# ProjectLot %>' ID="txtPrjctLot" runat="server" CssClass="txtbox"
                                                                                                                                MaxLength="5" BindingName="ProjectLot"></asp1:TCLTextBox>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                                                            Block
                                                                                                                        </td>
                                                                                                                        <td align="left" valign="middle">
                                                                                                                        </td>
                                                                                                                        <td align="left" valign="middle">
                                                                                                                            <asp1:TCLTextBox Text='<%# ProjectBlock %>' ID="txtPrjctBlock" runat="server" CssClass="txtbox"
                                                                                                                                MaxLength="5" BindingName="ProjectBlock"></asp1:TCLTextBox>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                                                            Section
                                                                                                                        </td>
                                                                                                                        <td align="left" valign="middle">
                                                                                                                        </td>
                                                                                                                        <td align="left" valign="middle">
                                                                                                                            <asp1:TCLTextBox Text='<%# ProjectSection %>' ID="txtPrjctSection" runat="server"
                                                                                                                                CssClass="txtbox" MaxLength="5" BindingName="ProjectSection"></asp1:TCLTextBox>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                                                            Phase
                                                                                                                        </td>
                                                                                                                        <td align="left" valign="middle">
                                                                                                                        </td>
                                                                                                                        <td align="left" valign="middle">
                                                                                                                            <asp1:TCLTextBox Text='<%# ProjectPhase %>' ID="txtPrjctPhase" runat="server" CssClass="txtbox"
                                                                                                                                MaxLength="5" BindingName="ProjectPhase"></asp1:TCLTextBox>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </asp:Panel>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <table align="center" border="0" cellpadding="0" cellspacing="8" width="100%" class="allborder">
                                                                                                                <asp:Panel ID="pnlTenantSpace" runat="server">
                                                                                                                    <tr height="24">
                                                                                                                        <td align="left" class="blackbfont bgcolor4" colspan="6" valign="middle">
                                                                                                                            Tenant Space
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td align="left" valign="middle" width="35%" colspan="3" class="blucolor blackbfont">
                                                                                                                            Net Rentable Area (sf)
                                                                                                                        </td>
                                                                                                                        <td align="left" valign="middle" width="3%">
                                                                                                                        </td>
                                                                                                                        <td align="right" valign="middle" width="34%">
                                                                                                                            <asp1:TCLTextBox Text='<%# ProjectNetRentArea %>' ID="txtPrjctNetRntArea" Enabled="true"
                                                                                                                                runat="server" CssClass="txtbox integer" BindingName="ProjectNetRentArea"></asp1:TCLTextBox>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr height="24">
                                                                                                                        <td align="center" valign="middle" colspan="6">
                                                                                                                            <asp:Button ID="btnPjrctLeasingInfo" runat="server" CssClass="btnstylebig" Text="Leasing Info"
                                                                                                                                OnClientClick="return WOpen('li','');" />
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </asp:Panel>
                                                                                                            </table>
                                                                                                            <table>
                                                                                                                <tr align="center">
                                                                                                                    <td align="center" valign="middle" width="50%">
                                                                                                                        <asp:Button ID="btnPrjctApprslInfo" runat="server" CssClass="btnstylebig" Text="Appraisal Info"
                                                                                                                            OnClientClick="return WOpen('ai','');" />
                                                                                                                    </td>
                                                                                                                    <td align="center" valign="middle" width="50%">
                                                                                                                        <asp:Button ID="btnprjctSalescntrct" runat="server" CssClass="btnstylebig" Text="Sales Contract"
                                                                                                                            OnClientClick="return WOpen('sc','');" />
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <%-- <tr>
                                                                    <td align="right" valign="top" class="pr10 greyfooter">
                                                                        <asp:Button runat="server" ID="btnProjectSave" Text="Save" CssClass="btnstyle" />
                                                                        &nbsp;&nbsp;
                                                                        <asp:Button runat="server" ID="btnProjectCancel" Visible="False" Text="Cancel" CssClass="btnstyle" />
                                                                    </td>
                                                                </tr>--%>
                                                            </table>
                                                        </ContentTemplate>
                                                    </asp:TabPanel>
                                                    <asp:TabPanel ID="tabPnlGeneral" runat="server">
                                                        <HeaderTemplate>
                                                            General</HeaderTemplate>
                                                        <ContentTemplate>
                                                            <table width="100%" class="allgborder" cellpadding="1" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td align="left" valign="top">
                                                                        <table width="100%" align="center" cellpadding="5" cellspacing="0" border="0">
                                                                            <tr>
                                                                                <td width="100%" align="left" valign="top">
                                                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="bgcolor6 allgborder">
                                                                                        <tr>
                                                                                            <td width="33%" valign="top">
                                                                                                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="8">
                                                                                                    <tr>
                                                                                                        <td width="29%" align="left" valign="middle" class="blucolor blackbfont">
                                                                                                            Company
                                                                                                        </td>
                                                                                                        <td width="3%" align="left" valign="middle">
                                                                                                        </td>
                                                                                                        <td width="68%" align="left" valign="middle">
                                                                                                            <asp:DropDownList ID="ddlCompany" runat="server" CssClass="ddlistbox" AutoPostBack="true"
                                                                                                                OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                                            Branch
                                                                                                        </td>
                                                                                                        <td align="left" valign="middle">
                                                                                                        </td>
                                                                                                        <td align="left" valign="middle">
                                                                                                            <asp:DropDownList ID="ddlBranch" runat="server" CssClass="ddlistbox">
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                                            Area
                                                                                                        </td>
                                                                                                        <td align="left" valign="middle">
                                                                                                        </td>
                                                                                                        <td align="left" valign="middle">
                                                                                                            <asp:DropDownList ID="ddlArea" runat="server" CssClass="ddlistbox" AutoPostBack="true"
                                                                                                                OnSelectedIndexChanged="ddlArea_SelectedIndexChanged">
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                                            Locale
                                                                                                        </td>
                                                                                                        <td align="left" valign="middle">
                                                                                                        </td>
                                                                                                        <td align="left" valign="middle">
                                                                                                            <asp:DropDownList ID="ddlLocal" runat="server" CssClass="ddlistbox">
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                                            M-Class
                                                                                                        </td>
                                                                                                        <td align="left" valign="middle">
                                                                                                        </td>
                                                                                                        <td align="left" valign="middle">
                                                                                                            <asp:DropDownList ID="ddlMClass" runat="server" CssClass="ddlistbox" AutoPostBack="true"
                                                                                                                OnSelectedIndexChanged="ddlMClass_SelectedIndexChanged">
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                                            S-Class
                                                                                                        </td>
                                                                                                        <td align="left" valign="middle">
                                                                                                        </td>
                                                                                                        <td align="left" valign="middle">
                                                                                                            <asp:DropDownList ID="ddlSClass" runat="server" CssClass="ddlistbox">
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                                            Master LOC
                                                                                                        </td>
                                                                                                        <td align="left" valign="middle">
                                                                                                        </td>
                                                                                                        <td align="left" valign="middle">
                                                                                                            <asp:DropDownList ID="ddlMLoc" runat="server" CssClass="ddlistbox" AutoPostBack="true"
                                                                                                                OnSelectedIndexChanged="ddlMLoc_SelectedIndexChanged">
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                                            Sub LOC
                                                                                                        </td>
                                                                                                        <td align="left" valign="middle">
                                                                                                        </td>
                                                                                                        <td align="left" valign="middle">
                                                                                                            <asp:DropDownList ID="ddlSLoc" runat="server" CssClass="ddlistbox">
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="3" align="left" valign="middle">
                                                                                                            &#160;&#160;
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                            <td width="17%" valign="top">
                                                                                                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="8">
                                                                                                    <tr>
                                                                                                        <td align="left" valign="middle">
                                                                                                            <asp:CheckBox ID="chkRevolving" runat="server" AutoPostBack="true" OnCheckedChanged="chkRevolving_CheckedChanged" />
                                                                                                        </td>
                                                                                                        <td colspan="2" align="left" valign="middle" class="blucolor blackbfont">
                                                                                                            Revolving
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td align="left" valign="middle">
                                                                                                            <asp:CheckBox ID="chkNonAccural" runat="server" />
                                                                                                        </td>
                                                                                                        <td colspan="2" align="left" valign="middle" class="blucolor blackbfont">
                                                                                                            Non Accural
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td align="left" valign="middle">
                                                                                                            <asp:CheckBox ID="chkInDefault" runat="server" />
                                                                                                        </td>
                                                                                                        <td colspan="2" align="left" valign="middle" class="blucolor blackbfont">
                                                                                                            In Default
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td align="left" valign="middle">
                                                                                                            <asp:CheckBox ID="chkStopFin" runat="server" />
                                                                                                        </td>
                                                                                                        <td colspan="2" align="left" valign="middle" class="blucolor blackbfont">
                                                                                                            Stop Fin. Activity
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td width="10%" align="left" valign="middle">
                                                                                                            <asp:CheckBox ID="chkForeclosure" runat="server" />
                                                                                                        </td>
                                                                                                        <td colspan="2" align="left" valign="middle" class="blucolor blackbfont">
                                                                                                            Foreclosure
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td align="left" valign="middle">
                                                                                                            <asp:CheckBox ID="chkBankruptcy" runat="server" />
                                                                                                        </td>
                                                                                                        <td colspan="2" align="left" valign="middle" class="blucolor blackbfont">
                                                                                                            Bankruptcy
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td align="left" valign="middle">
                                                                                                            <asp:CheckBox ID="chkFannieMae" runat="server" AutoPostBack="true" OnCheckedChanged="chkFannieMae_CheckedChanged" />
                                                                                                        </td>
                                                                                                        <td colspan="2" align="left" valign="middle" class="blucolor blackbfont">
                                                                                                            Fannie Mae
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td align="left" valign="middle">
                                                                                                            <asp:CheckBox ID="chk203K" runat="server" />
                                                                                                        </td>
                                                                                                        <td colspan="2" align="left" valign="middle" class="blucolor blackbfont">
                                                                                                            Renovation
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td align="left" valign="middle">
                                                                                                            <asp:CheckBox ID="chkRollToPerm" runat="server" />
                                                                                                        </td>
                                                                                                        <td colspan="2" align="left" valign="middle" class="blucolor blackbfont">
                                                                                                            Roll-To-Perm
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td align="left" valign="middle">
                                                                                                            <asp:CheckBox ID="chkAssetManagementt" runat="server" />
                                                                                                        </td>
                                                                                                        <td colspan="2" align="left" valign="middle" class="blucolor blackbfont">
                                                                                                            Asset Management
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td align="left" valign="middle">
                                                                                                            <asp:CheckBox ID="chkAmortizeLoan" runat="server" AutoPostBack="true" OnCheckedChanged="chkAmortizeLoan_CheckedChanged" />
                                                                                                        </td>
                                                                                                        <td colspan="2" align="left" valign="middle" class="blucolor blackbfont">
                                                                                                            Amortize Loan
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td align="left" valign="middle">
                                                                                                            &#160;&#160;
                                                                                                        </td>
                                                                                                        <td width="7%" align="left" valign="middle">
                                                                                                            <asp:CheckBox ID="chkAutomatic" runat="server" />
                                                                                                        </td>
                                                                                                        <td width="83%" align="left" valign="middle" class="blucolor blackbfont">
                                                                                                            Automatic Recast
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td align="left" valign="middle">
                                                                                                            &#160;&#160;
                                                                                                        </td>
                                                                                                        <td align="left" valign="middle">
                                                                                                            <asp:CheckBox ID="chkOriginal" runat="server" />
                                                                                                        </td>
                                                                                                        <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                                            Original Terms
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                            <td width="50%" valign="top">
                                                                                                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="8">
                                                                                                    <tr>
                                                                                                        <td width="21%" align="left" valign="middle" class="blucolor blackbfont">
                                                                                                            GL Table
                                                                                                        </td>
                                                                                                        <td width="4%" align="left" valign="middle">
                                                                                                        </td>
                                                                                                        <td width="75%" align="left" valign="middle">
                                                                                                            <asp:DropDownList ID="ddlGLTable" runat="server" CssClass="ddlistbox">
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td align="left" valign="top" class="blucolor blackbfont">
                                                                                                            Status
                                                                                                        </td>
                                                                                                        <td align="left" valign="middle">
                                                                                                        </td>
                                                                                                        <td align="left" valign="middle">
                                                                                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="ddlistbox">
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="3" align="left" valign="top">
                                                                                                            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="8" class="allborder">
                                                                                                                <tr>
                                                                                                                    <td align="left" valign="middle" class="blackbfont bgcolor4" colspan="3" style="padding: 5px;">
                                                                                                                        Other Coding
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td width="27%" align="left" valign="middle" class="blucolor blackbfont">
                                                                                                                        Federal Code
                                                                                                                    </td>
                                                                                                                    <td width="3%" align="left" valign="middle">
                                                                                                                    </td>
                                                                                                                    <td width="70%" align="left" valign="middle">
                                                                                                                        <asp:DropDownList ID="ddlFederalCode" runat="server" CssClass="ddlistbox">
                                                                                                                        </asp:DropDownList>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                                                        Loan Grade
                                                                                                                    </td>
                                                                                                                    <td align="left" valign="middle">
                                                                                                                    </td>
                                                                                                                    <td align="left" valign="middle">
                                                                                                                        <asp:DropDownList ID="ddlLoanGrade" runat="server" CssClass="ddlistbox">
                                                                                                                        </asp:DropDownList>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td align="left" valign="middle" class="blucolor blackbfont" cssclass="txtbox">
                                                                                                                        Loan Grade Date
                                                                                                                    </td>
                                                                                                                    <td align="left" valign="middle">
                                                                                                                    </td>
                                                                                                                    <td align="left" valign="middle">
                                                                                                                        <asp:TextBox ID="txtLoanGrdDate" runat="server" CssClass="txtbox"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                                                        Loan Class
                                                                                                                    </td>
                                                                                                                    <td align="left" valign="middle">
                                                                                                                    </td>
                                                                                                                    <td align="left" valign="middle">
                                                                                                                        <asp:DropDownList ID="ddlLoanClass" runat="server" CssClass="ddlistbox">
                                                                                                                        </asp:DropDownList>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                                                        Census Test
                                                                                                                    </td>
                                                                                                                    <td align="left" valign="middle">
                                                                                                                    </td>
                                                                                                                    <td align="left" valign="middle">
                                                                                                                        <asp:TextBox ID="txtCensusTest" runat="server" CssClass="txtbox" MaxLength="10"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                                                        Loan Type
                                                                                                                    </td>
                                                                                                                    <td align="left" valign="middle">
                                                                                                                    </td>
                                                                                                                    <td align="left" valign="middle">
                                                                                                                        <asp:DropDownList ID="ddlLoanType" runat="server" CssClass="ddlistbox">
                                                                                                                        </asp:DropDownList>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                                                        Loan Purpose
                                                                                                                    </td>
                                                                                                                    <td align="left" valign="middle">
                                                                                                                    </td>
                                                                                                                    <td align="left" valign="middle">
                                                                                                                        <asp:DropDownList ID="ddlLoanPurpose" runat="server" CssClass="ddlistbox">
                                                                                                                        </asp:DropDownList>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                                                        Collateral Code
                                                                                                                    </td>
                                                                                                                    <td align="left" valign="middle">
                                                                                                                    </td>
                                                                                                                    <td align="left" valign="middle">
                                                                                                                        <asp:DropDownList ID="ddlCollateralCode" runat="server" CssClass="ddlistbox">
                                                                                                                        </asp:DropDownList>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                                                        Commitment
                                                                                                                    </td>
                                                                                                                    <td align="left" valign="middle">
                                                                                                                    </td>
                                                                                                                    <td align="left" valign="middle">
                                                                                                                        <asp:TextBox ID="txtCommitment" runat="server" CssClass="txtbox" MaxLength="5"></asp:TextBox>
                                                                                                                        <%--<asp:Button ID="btnTest" runat="server" Text="Test Save" OnClick="btnSaveTest_Click" />--%>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                    </asp:TabPanel>
                                                    <asp:TabPanel ID="tabPnlDates" runat="server">
                                                        <HeaderTemplate>
                                                            Dates</HeaderTemplate>
                                                        <ContentTemplate>
                                                            <table width="100%" class="allgborder" cellpadding="1" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td align="left" valign="top">
                                                                        <table width="100%" align="left" cellpadding="0" cellspacing="0" border="0">
                                                                            <%--<tr>
                                                                    <td width="*" align="left" valign="middle" class="pl10 bbggridhead tablehdfont dotbtborder">
                                                                        Dates
                                                                    </td>
                                                                    <td width="110px" height="35px" align="right" valign="top" class="dotbtborder">
                                                                        <img src="../../Images/yobg.png" border="0" vspace="0" hspace="0" />
                                                                    </td>
                                                                </tr>--%>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" valign="top">
                                                                        <table width="100%" align="center" cellpadding="5" cellspacing="0" border="0">
                                                                            <tr>
                                                                                <td width="100%" align="left" valign="middle" class="bgcolor6">
                                                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                        <tr>
                                                                                            <td width="35%" valign="top">
                                                                                                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="8" class="allborder">
                                                                                                    <tr height="24">
                                                                                                        <td colspan="3" align="left" valign="middle" class="bgcolor4 blackbfont tp">
                                                                                                            Pipeline
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td width="34%" align="left" valign="middle" class="blucolor blackbfont">
                                                                                                            Application
                                                                                                        </td>
                                                                                                        <td width="6%" align="left" valign="middle">
                                                                                                        </td>
                                                                                                        <td width="60%" align="left" valign="middle">
                                                                                                            <asp:TextBox ID="txtDateTabApplc" runat="server" Text="<%# application %>" CssClass="txtbox"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                                            Approval
                                                                                                        </td>
                                                                                                        <td align="left" valign="middle">
                                                                                                        </td>
                                                                                                        <td align="left" valign="middle">
                                                                                                            <asp:TextBox ID="txtDateTabApprvl" runat="server" Text="<%# Approval %>" CssClass="txtbox"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                                            Closing
                                                                                                        </td>
                                                                                                        <td align="left" valign="middle">
                                                                                                        </td>
                                                                                                        <td align="left" valign="middle">
                                                                                                            <asp:TextBox ID="txtDateTabClsng" runat="server" Text="<%# Closing %>" CssClass="txtbox"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                                            Percent To Fund
                                                                                                        </td>
                                                                                                        <td align="left" valign="middle">
                                                                                                        </td>
                                                                                                        <td align="left" valign="middle">
                                                                                                            <asp:TextBox ID="txtDateTabPrcntFund" runat="server" Text="<%# PercenttoFund %>"
                                                                                                                CssClass="txtbox double-keyup" onkeypress="if(event.keyCode<48 || event.keyCode>57)event.returnValue=false;">0.00</asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                            <td width="2%">
                                                                                            </td>
                                                                                            <td width="36%">
                                                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="8" class="allborder">
                                                                                                                <tr>
                                                                                                                    <td colspan="3" align="left" valign="middle" class="bgcolor4 blackbfont tp">
                                                                                                                        Construction Estimates
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td width="24%" align="left" valign="middle" class="blucolor blackbfont">
                                                                                                                        Start
                                                                                                                    </td>
                                                                                                                    <td width="1%" align="left" valign="middle">
                                                                                                                    </td>
                                                                                                                    <td width="*" align="left" valign="middle">
                                                                                                                        <asp:TextBox ID="txtDateTabCnsStart" runat="server" Text="<%# Start %>" CssClass="txtbox"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                                                        Completion
                                                                                                                    </td>
                                                                                                                    <td align="left" valign="middle">
                                                                                                                    </td>
                                                                                                                    <td align="left" valign="middle">
                                                                                                                        <asp:TextBox ID="txtDateTabCnsCmpl" runat="server" Text="<%# Completion %>" CssClass="txtbox"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="8" class="allborder">
                                                                                                                <tr>
                                                                                                                    <td colspan="3" align="left" valign="middle" class="blackbfont bgcolor4 tp">
                                                                                                                        Payoff
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td width="24%" align="left" valign="middle" class="blucolor blackbfont">
                                                                                                                        Quoted
                                                                                                                    </td>
                                                                                                                    <td width="1%" align="left" valign="middle">
                                                                                                                    </td>
                                                                                                                    <td align="left" valign="middle">
                                                                                                                        <asp:TextBox ID="txtDateTabQotPayOff" Text="<%# Quoted %>" runat="server" CssClass="txtbox"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                                                        Posted
                                                                                                                    </td>
                                                                                                                    <td align="left" valign="middle">
                                                                                                                    </td>
                                                                                                                    <td align="left" valign="middle">
                                                                                                                        <asp:TextBox ID="txtDateTabQotPost" Text="<%# Posted %>" runat="server" CssClass="txtbox"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td width="29%" valign="top">
                                                                                    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="8" class="allborder">
                                                                                        <tr>
                                                                                            <td colspan="6" align="left" valign="middle" class="bgcolor4 blackbfont tp">
                                                                                                Processing
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td width="19%" align="left" valign="middle" class="blucolor blackbfont">
                                                                                                Note Date
                                                                                            </td>
                                                                                            <td width="1%" align="left" valign="middle">
                                                                                            </td>
                                                                                            <td width="*" align="left" valign="middle">
                                                                                                <asp:TextBox ID="txtDateTabPrssNote" Text="<%# NoteDate %>" runat="server" CssClass="txtbox"></asp:TextBox>
                                                                                            </td>
                                                                                            <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                                Conversion
                                                                                            </td>
                                                                                            <td align="left" valign="middle">
                                                                                            </td>
                                                                                            <td align="left" valign="middle">
                                                                                                <asp:TextBox ID="txtDateTabconvrs" Text="<%# Conversion %>" runat="server" CssClass="txtbox"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                                Maturity
                                                                                            </td>
                                                                                            <td align="left" valign="middle">
                                                                                            </td>
                                                                                            <td align="left" valign="middle">
                                                                                                <asp:TextBox ID="txtDateTabMturty" Text="<%# Maturity %>" runat="server" CssClass="txtbox"></asp:TextBox>
                                                                                            </td>
                                                                                            <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                                Rate Lock
                                                                                            </td>
                                                                                            <td align="left" valign="middle">
                                                                                            </td>
                                                                                            <td align="left" valign="middle">
                                                                                                <asp:TextBox ID="txtDateTabRateLck" Text="<%# RateLock %>" runat="server" CssClass="txtbox"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                    </asp:TabPanel>
                                                    <asp:TabPanel ID="tabPnlRelease" runat="server">
                                                        <HeaderTemplate>
                                                            Release</HeaderTemplate>
                                                        <ContentTemplate>
                                                            <table width="100%" class="allgborder" cellpadding="1" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TabContainer ID="tabContainerRelease" runat="server" ActiveTabIndex="1" CssClass="Tab">
                                                                            <asp:TabPanel ID="tabpnlRlsSch" runat="server">
                                                                                <HeaderTemplate>
                                                                                    Release Schedules
                                                                                </HeaderTemplate>
                                                                                <ContentTemplate>
                                                                                    <table border="0" cellpadding="1" cellspacing="0" class="allgborder" width="100%">
                                                                                        <tr>
                                                                                            <td align="left" valign="top">
                                                                                                <table align="left" border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="left" valign="top">
                                                                                                <table align="center" border="0" cellpadding="5" cellspacing="0" width="100%">
                                                                                                    <tr>
                                                                                                        <td align="left" class="bgcolor6 greybborder" valign="top" width="35%">
                                                                                                            <table align="center" border="0" cellpadding="5" cellspacing="0" width="100%">
                                                                                                                <tr>
                                                                                                                    <td align="left" class="blucolor " valign="middle" width="80%">
                                                                                                                        Number of Additional Release Schedule(s) to Add
                                                                                                                    </td>
                                                                                                                    <td align="left" valign="middle" width="3%">
                                                                                                                    </td>
                                                                                                                    <td align="left" valign="middle" width="39%">
                                                                                                                        <asp1:TCLNumericTextBox ID="txtRlsSchTabNum" runat="server" BindingName="ReleaseSchNo"
                                                                                                                            CssClass="txtbox" MaxLength="5" Text="<%# ReleaseSchNo %>">
                                                                                                                        </asp1:TCLNumericTextBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                        <td align="left" class="bgcolor6 greybborder" valign="top" width="28%">
                                                                                                            &nbsp;&nbsp;
                                                                                                        </td>
                                                                                                        <td align="left" class="bgcolor6 greybborder" valign="top" width="29%">
                                                                                                            &nbsp;&nbsp;
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="left" valign="top">
                                                                                                <table align="center" border="0" cellpadding="5" cellspacing="0" width="100%">
                                                                                                    <tr>
                                                                                                        <td align="left" class="bgcolor6" valign="top">
                                                                                                            <table align="center" border="0" cellpadding="0" cellspacing="1" class="allborder"
                                                                                                                width="100%">
                                                                                                                <tr class="gridheaderbg">
                                                                                                                    <td align="left" class="pl10" valign="middle">
                                                                                                                        <asp:ImageButton ID="imgbtnRelsSchAdd" runat="server" AlternateText="Add" ToolTip="Add"
                                                                                                                            border="0" CssClass="curpointer" hspace="0" ImageAlign="Middle" ImageUrl="~/Images/gicon02.png"
                                                                                                                            OnClick="imgbtnRelsSchAdd_Click" vspace="0" />
                                                                                                                        &nbsp;
                                                                                                                        <asp:ImageButton ID="imgbtnRelsSchEdit" runat="server" align="middle" ToolTip="Multi Edit"
                                                                                                                            AlternateText="Multi Edit" border="0" class="curpointer" hspace="0" ImageUrl="~/Images/MultiEdit.png"
                                                                                                                            OnClick="imgbtnRelsSchEdit_Click" vspace="0" />
                                                                                                                        &nbsp;
                                                                                                                        <asp:ImageButton ID="imgbtnRelsSchDel" runat="server" align="middle" ToolTip="Delete"
                                                                                                                            AlternateText="Delete" border="0" class="curpointer" hspace="0" ImageUrl="~/Images/gicon04.png"
                                                                                                                            OnClick="imgbtnRelsSchDel_Click" OnClientClick="return ConfirmCheck(this);" vspace="0" />
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <CustomGid:ExtendGrid ID="grdRlsSch" runat="server" OnGridCheckedChange="grdRlsSch_GridCheckedChange"
                                                                                                                            OnGridRowEditing="grdRlsSch_GridRowEditing" />
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <asp:HiddenField ID="hdnItemID" runat="server" />
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </ContentTemplate>
                                                                            </asp:TabPanel>
                                                                            <asp:TabPanel ID="tabpnlNotePayOffRules" runat="server">
                                                                                <HeaderTemplate>
                                                                                    Note Payoff Rules
                                                                                </HeaderTemplate>
                                                                                <ContentTemplate>
                                                                                    <table border="0" cellpadding="1" cellspacing="0" class="allgborder" width="100%">
                                                                                        <tr>
                                                                                            <td align="left" valign="top">
                                                                                                <table align="left" border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="left" valign="top">
                                                                                                <table align="center" border="0" cellpadding="5" cellspacing="0" width="100%">
                                                                                                    <tr>
                                                                                                        <td align="right" class="bgcolor6" valign="top" width="10%">
                                                                                                            <asp:CheckBox ID="chkPayoffShwAll" runat="server" AutoPostBack="true" Checked="true"
                                                                                                                OnCheckedChanged="chkPayoffShwAll_Checked" />
                                                                                                            Show All
                                                                                                        </td>
                                                                                                        <td align="center" class="bgcolor6" valign="top" width="31%">
                                                                                                            Borrower Rule Templates
                                                                                                        </td>
                                                                                                        <td align="left" class="bgcolor1" valign="top" width="18%">
                                                                                                            &nbsp;&nbsp;
                                                                                                        </td>
                                                                                                        <td align="center" class="bgcolor6" valign="top" width="41%">
                                                                                                            Attached to Note
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td align="center" class="bgcolor6" colspan="2" valign="top" width="100%">
                                                                                                            <table align="center" border="0" cellpadding="0" cellspacing="0" class="allborder"
                                                                                                                width="100%">
                                                                                                                <tr>
                                                                                                                    <td align="left" valign="top">
                                                                                                                        <CustomGid:ExtendGrid ID="cgNotePayOffRules" runat="server" />
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                        <td align="center" class="bgcolor1" valign="top">
                                                                                                            <table align="center" border="0" cellpadding="5" cellspacing="5" width="23%">
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <asp:Button ID="btnPayoffMoveRight" runat="server" CssClass="btnstyle" OnClick="btnPayoffMoveRight_Click"
                                                                                                                            Text="&gt;" />
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                        <td align="center" class="bgcolor6" colspan="2" valign="top" width="100%">
                                                                                                            <table align="center" border="0" cellpadding="0" cellspacing="0" class="allborder"
                                                                                                                width="100%">
                                                                                                                <tr>
                                                                                                                    <td align="left" class="gridheaderbg allborder" valign="top">
                                                                                                                        <asp:ImageButton ID="imgbtnNtPayoffRulsDelte" runat="server" AlternateText="Delete"
                                                                                                                            border="0" CssClass="curpointer" hspace="0" ImageAlign="Middle" ImageUrl="~/Images/gicon04.png"
                                                                                                                            OnClick="imgbtnNtPayoffRulsDelte_Click" ToolTip="Delete" vspace="0" />
                                                                                                                        <asp:ImageButton ID="imgbtnNotePayOffEdit" runat="server" AlternateText="Edit" ImageAlign="Middle"
                                                                                                                            ImageUrl="~/Images/gicon03.png" OnClick="imgbtnNotePayOffEdit_Click" ToolTip="Edit" />
                                                                                                                        &nbsp;
                                                                                                                        <asp:Button ID="btnNPODelete" runat="server" OnClick="btnNPODelete_Click" Style="visibility: hidden" />
                                                                                                                        <asp:Button ID="btnNotePayOff" runat="server" OnClick="btnNotePayOff_Click" Style="visibility: hidden" />
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <table id="tblNotePayOff" runat="server" border="0" class="bgcolor3 pl10" visible="false"
                                                                                                                            width="100%">
                                                                                                                            <tr>
                                                                                                                                <td align="left" height="5" valign="top">
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                            <tr id="trSuccess" runat="server" visible="false">
                                                                                                                                <td align="left" class="ErrorSuccess" valign="middle">
                                                                                                                                    <asp:Label ID="lblPayOffSuccess" runat="server"></asp:Label>
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                            <tr id="trError" runat="server" visible="false">
                                                                                                                                <td align="left" class="ErrorFail" valign="middle">
                                                                                                                                    <asp:Label ID="lblPayOffError" runat="server" />
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                            <tr>
                                                                                                                                <td align="left" height="5" valign="top">
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                        </table>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td align="left" valign="top">
                                                                                                                        <CustomGid:ExtendGrid ID="cgNotPayOffRulsAttach" runat="server" GridAllowPaging="True"
                                                                                                                            GridSelectedRowStyleCSS="BlueViolet" GridShowFooter="False" GridWidth="100%"
                                                                                                                            ImageAddButtonEnabled="False" ImageAddButtonToolTip="Add" ImageAddButtonURL="/Images/gicon02.png"
                                                                                                                            ImageAdditionalButton1URL="/Images/gicon02.png" ImageAdditionalButton2URL="/Images/ArrowRightSmall.png"
                                                                                                                            ImageAdditionalButton3URL="/Images/gicon04.png" ImageAdditionalButton4URL="/Images/copy.png"
                                                                                                                            ImageAdditionalButton5URL="/Images/gicon02.png" ImageAdditionalButton6URL="/Images/ArrowRightSmall.png"
                                                                                                                            ImageAdditionalButton7URL="/Images/gicon04.png" ImageAdditionalButton8URL="/Images/copy.png"
                                                                                                                            ImageAddtionalButton1Enabled="False" ImageAddtionalButton2Enabled="False" ImageAddtionalButton3Enabled="False"
                                                                                                                            ImageAddtionalButton4Enabled="False" ImageAddtionalButton5Enabled="False" ImageAddtionalButton6Enabled="False"
                                                                                                                            ImageAddtionalButton7Enabled="False" ImageAddtionalButton8Enabled="False" ImageCopyButtonEnabled="False"
                                                                                                                            ImageCopyButtonToolTip="Copy" ImageCopyButtonURL="/Images/copy.png" ImageDeleteButtonEnabled="False"
                                                                                                                            ImageDeleteButtonToolTip="Delete" ImageDeleteButtonURL="/Images/gicon04.png"
                                                                                                                            ImageEditButtonEnabled="False" ImageEditButtonToolTip="Edit" ImageEditButtonURL="/Images/ArrowRightSmall.png"
                                                                                                                            ImageFirstURL="/Images/LeftAllArrow.png" ImageLastURL="/Images/RightAllArrow.png"
                                                                                                                            ImageNextURL="/Images/RightArrow.png" ImagePreviousURL="/Images/LeftArrow.png"
                                                                                                                            PageNumber="1" SortOrder="ASC" />
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="right" class="pr10 greyfooter" valign="middle">
                                                                                                <asp:HiddenField ID="hdnNotePayOffCount" runat="server" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </ContentTemplate>
                                                                            </asp:TabPanel>
                                                                            <asp:TabPanel ID="tabpnlRules" runat="server">
                                                                                <HeaderTemplate>
                                                                                    Release Rules
                                                                                </HeaderTemplate>
                                                                                <ContentTemplate>
                                                                                    <table border="0" cellpadding="1" cellspacing="0" class="allgborder" width="100%">
                                                                                        <tr>
                                                                                            <td align="left" valign="top">
                                                                                                <table align="left" border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="left" valign="top">
                                                                                                <table align="center" border="0" cellpadding="5" cellspacing="0" width="100%">
                                                                                                    <tr>
                                                                                                        <td align="center" class="bgcolor6" valign="top" width="41%">
                                                                                                            Borrower Rule Templates
                                                                                                        </td>
                                                                                                        <td align="left" class="bgcolor1" valign="top" width="18%">
                                                                                                            &nbsp;&nbsp;
                                                                                                        </td>
                                                                                                        <td align="center" class="bgcolor6" valign="top" width="41%">
                                                                                                            Attached to Release Schedule
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td align="right" class="bgcolor6" height="30" valign="middle">
                                                                                                            <asp:CheckBox ID="chkRlsRulsShwoAll" runat="server" AutoPostBack="true" Checked="true"
                                                                                                                OnCheckedChanged="chkRlsRulsShwoAll_CheckedChanged" />
                                                                                                            &nbsp; Show All
                                                                                                        </td>
                                                                                                        <td align="left" class="bgcolor1" valign="top">
                                                                                                            &nbsp;&nbsp;
                                                                                                        </td>
                                                                                                        <td align="center" class="bgcolor6" valign="middle">
                                                                                                            <table border="0" cellpadding="0" cellspacing="8" width="100%">
                                                                                                                <tr>
                                                                                                                    <td class="blucolor blackbfont" width="33%">
                                                                                                                        Release Item
                                                                                                                    </td>
                                                                                                                    <td width="4%">
                                                                                                                        &nbsp;&nbsp;
                                                                                                                    </td>
                                                                                                                    <td width="63%">
                                                                                                                        <asp1:TCLDropDownList ID="drpdwnlstRlsItem" runat="server" AppendDataBoundItems="true"
                                                                                                                            AutoPostBack="true" CssClass="ddlistbox" DataSource="<%# DictReleaseItems %>"
                                                                                                                            DataTextField="Value" DataValueField="Key" OnSelectedIndexChanged="drpdwnlstRlsItem_SelectedIndexChanged"
                                                                                                                            SetSelectedValue="<%# ReleaseItemSelectedValue %>">
                                                                                                                        </asp1:TCLDropDownList>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td align="center" class="bgcolor6" valign="top">
                                                                                                            <table align="center" border="0" cellpadding="0" cellspacing="1" class="allborder"
                                                                                                                width="100%">
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <CustomGid:ExtendGrid ID="cstGrdRlsPayoffTemp" runat="server" />
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                        <td align="left" class="bgcolor1" valign="top">
                                                                                                            <table align="center" border="0" cellpadding="5" cellspacing="5" width="23%">
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <asp:Button ID="btnRlsRulesMoveRight" runat="server" CssClass="btnstyle" OnClick="btnRlsRulesMoveRight_Click"
                                                                                                                            Text="&gt;" />
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                        <td align="center" class="bgcolor6" valign="top">
                                                                                                            <table align="center" border="0" cellpadding="0" cellspacing="1" class="allborder"
                                                                                                                width="100%">
                                                                                                                <tr>
                                                                                                                    <td align="left" class="gridheaderbg" valign="middle">
                                                                                                                        <asp:ImageButton ID="imgbtnReleaseRule" runat="server" AlternateText="Edit" ImageAlign="Middle"
                                                                                                                            ImageUrl="~/Images/gicon03.png" OnClick="imgbtnReleaseRule_Click" ToolTip="Edit" />
                                                                                                                        &nbsp; &nbsp;<asp:ImageButton ID="imgbtnRelsRulesDel" runat="server" AlternateText="Delete"
                                                                                                                            border="0" CssClass="curpointer" hspace="0" ImageAlign="Middle" ImageUrl="~/Images/gicon04.png"
                                                                                                                            OnClick="imgbtnRelsRulesDel_Click" ToolTip="Delete" vspace="0" />
                                                                                                                        <asp:Button ID="btnRelsRulesDelete" runat="server" OnClick="btnRelsRulesDelete_Click"
                                                                                                                            Style="visibility: hidden" />
                                                                                                                        <asp:Button ID="btnReleaseRules" runat="server" OnClick="btnReleaseRules_Click" Style="visibility: hidden" />
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td align="center" cellpadding="0" cellspacing="0" width="100%">
                                                                                                                        <CustomGid:ExtendGrid ID="cstGrdRlsPayoffAtchmnt" runat="server" />
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="right" class="pr10 greyfooter" colspan="6" valign="middle">
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </ContentTemplate>
                                                                            </asp:TabPanel>
                                                                        </asp:TabContainer>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                    </asp:TabPanel>
                                                    <asp:TabPanel ID="tabPnlBdgCntrl" runat="server">
                                                        <HeaderTemplate>
                                                            Budget Control</HeaderTemplate>
                                                        <ContentTemplate>
                                                            <table width="100%" class="allgborder" cellpadding="1" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td align="left" valign="top">
                                                                        <tr>
                                                                            <td align="left" valign="top">
                                                                                <table align="center" border="0" cellpadding="5" cellspacing="0" class="bgcolor6 greybborder"
                                                                                    width="100%">
                                                                                    <tr>
                                                                                        <td align="left" class="blucolor blackbfont" valign="middle" width="8%">
                                                                                            Budgets
                                                                                        </td>
                                                                                        <td align="left" valign="middle" width="1%">
                                                                                        </td>
                                                                                        <td align="left" valign="middle" width="32%">
                                                                                            <asp:DropDownList ID="ddlBudgets" runat="server" CssClass="ddlistbox" onchange="return CheckBudget();">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td align="left" valign="middle" width="59%">
                                                                                            &nbsp;&nbsp;
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="left" class="blucolor blackbfont" valign="middle">
                                                                                            Budget Title
                                                                                        </td>
                                                                                        <td align="left" valign="middle">
                                                                                        </td>
                                                                                        <td align="left" class="blackbfont" valign="middle">
                                                                                            <asp:Label ID="lblBudgetTitle" runat="server"></asp:Label>
                                                                                        </td>
                                                                                        <td align="left" valign="middle">
                                                                                            &nbsp;&nbsp;
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" valign="top">
                                                                                <table align="center" border="0" cellpadding="0" cellspacing="1" class="allborder"
                                                                                    width="100%">
                                                                                    <tr>
                                                                                        <td align="left" class="gridheaderbg" valign="middle">
                                                                                            <span class="pl10">
                                                                                                <asp:ImageButton ID="imgbtnBdgtCntrlAdd" runat="server" AlternateText="Add" border="0"
                                                                                                    CssClass="curpointer" hspace="0" ImageAlign="Middle" ImageUrl="~/Images/gicon02.png"
                                                                                                    ToolTip="Add" vspace="0" OnClick="imgbtnBdgtCntrlAdd_Click" />
                                                                                                &nbsp;
                                                                                                <asp:ImageButton ID="imgbtnEdit" runat="server" AlternateText="Edit" ImageAlign="Middle"
                                                                                                    ImageUrl="~/Images/gicon03.png" ToolTip="Edit" OnClick="imgbtnEdit_Click" />&nbsp;
                                                                                                <asp:ImageButton ID="imgbtnBdgtCntrlView" runat="server" AlternateText="View" border="0"
                                                                                                    CssClass="curpointer" hspace="0" ImageAlign="Middle" ImageUrl="~/Images/gicon01.png"
                                                                                                    ToolTip="View" vspace="0" Visible="false" />
                                                                                                &nbsp;
                                                                                                <asp:ImageButton ID="ImgbtnDelete" runat="server" AlternateText="Delete" border="0"
                                                                                                    CssClass="curpointer" hspace="0" ImageAlign="Middle" ImageUrl="~/Images/gicon04.png"
                                                                                                    ToolTip="Delete" vspace="0" OnClick="ImgbtnDelete_Click" />
                                                                                                &nbsp;
                                                                                                <asp:ImageButton ID="imgbtnBdgtCntrlVendor" runat="server" AlternateText="Vendor"
                                                                                                    border="0" CssClass="curpointer" hspace="0" ImageAlign="Middle" ImageUrl="~/Images/Vendor.png"
                                                                                                    ToolTip="Vendor" vspace="0" OnClientClick="return WVopen();" />
                                                                                            </span>
                                                                                            <asp:Button runat="server" ID="btnDeleteBudget" Style="visibility: hidden" OnClick="btnDeleteBudget_Click" />
                                                                                            <asp:Button runat="server" ID="btnSelectBudget" Style="visibility: hidden" OnClick="btnSelectBudget_Click" />
                                                                                            <asp:HiddenField ID="hdnBudgetTemp" runat="server" />
                                                                                            <asp:Button runat="server" ID="btnEditBudget" Style="visibility: hidden" OnClick="btnEditBudget_Click" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="left" class="gridheaderbg" valign="middle">
                                                                                            <table border="0" width="100%" id="tblMsgMPopup" runat="server" visible="false" class="bgcolor3 pl10">
                                                                                                <tr>
                                                                                                    <td align="left" valign="top" height="5">
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr id="trAlertSuccess" runat="server" visible="false">
                                                                                                    <td align="left" valign="middle" class="ErrorSuccess">
                                                                                                        <asp:Label ID="lblSuccess" runat="server" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr id="ErrorAlert1" runat="server" visible="false">
                                                                                                    <td align="left" valign="middle" class="ErrorFail">
                                                                                                        <asp:Label ID="lblErrorAlert" runat="server"></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td align="left" valign="top" height="5">
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <CustomGid:ExtendGrid ID="cgBudget" runat="server" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" valign="top">
                                                                                <table align="center" border="0" cellpadding="5" cellspacing="0" class="bgcolor6"
                                                                                    width="100%">
                                                                                    <tr>
                                                                                        <td align="left" valign="middle" class="blucolor blackbfont ">
                                                                                            Inspection Company
                                                                                        </td>
                                                                                        <td align="left" valign="middle">
                                                                                        </td>
                                                                                        <td align="left" valign="middle">
                                                                                            <asp:DropDownList ID="ddlInspCompany" runat="server" CssClass="ddlistbox">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td align="left" colspan="3" valign="middle" class="blucolor blackbfont">
                                                                                            <asp:CheckBox ID="chkOmit" runat="server" />
                                                                                            Omit Extended Borrower & Vendor Information From Inspection Requests
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="left" class="blucolor blackbfont" valign="middle" width="15%">
                                                                                            Commitment
                                                                                        </td>
                                                                                        <td align="left" valign="middle" width="1%">
                                                                                        </td>
                                                                                        <td align="left" valign="middle" width="20%">
                                                                                            <asp:TextBox ID="txtBudgetCommitment" runat="server" CssClass="txtbox amount" Enabled="false"></asp:TextBox>
                                                                                        </td>
                                                                                        <td align="left" class="blucolor blackbfont" valign="middle" width="15%">
                                                                                            Allocated Commitment
                                                                                        </td>
                                                                                        <td align="left" valign="middle" width="1%">
                                                                                        </td>
                                                                                        <td align="left" valign="middle" width="*">
                                                                                            <asp:TextBox ID="txtAlctCommit" runat="server" CssClass="txtbox amount" Enabled="false"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="left" class="blucolor blackbfont" valign="middle">
                                                                                            Total Estimated
                                                                                        </td>
                                                                                        <td align="left" valign="middle">
                                                                                        </td>
                                                                                        <td align="left" valign="middle">
                                                                                            <asp:TextBox ID="txtTotalEstimated" runat="server" CssClass="txtbox amount" Enabled="false"></asp:TextBox>
                                                                                        </td>
                                                                                        <td align="left" class="blucolor blackbfont" valign="middle">
                                                                                            Total Allocated
                                                                                        </td>
                                                                                        <td align="left" valign="middle">
                                                                                        </td>
                                                                                        <td align="left" valign="middle">
                                                                                            <asp:TextBox ID="txtTotalAllocated" runat="server" CssClass="txtbox amount" Enabled="false"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="left" class="blucolor blackbfont" class="blucolor blackbfont" valign="middle">
                                                                                            Disbursement Criteria
                                                                                        </td>
                                                                                        <td align="left" valign="middle">
                                                                                            &nbsp;&nbsp;
                                                                                        </td>
                                                                                        <td align="left" valign="middle">
                                                                                            &nbsp;&nbsp;
                                                                                        </td>
                                                                                        <td align="left" valign="middle">
                                                                                            &nbsp;&nbsp;
                                                                                        </td>
                                                                                        <td align="left" valign="middle">
                                                                                            &nbsp;&nbsp;
                                                                                        </td>
                                                                                        <td align="left" valign="middle">
                                                                                            &nbsp;&nbsp;
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                            Auto Generate Borrower Funds First
                                                                                        </td>
                                                                                        <td align="left" valign="middle">
                                                                                        </td>
                                                                                        <td align="left" valign="middle">
                                                                                            <asp:DropDownList ID="ddlAutoGenBrwFndFirst" runat="server" CssClass="ddlistbox">
                                                                                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                                                                                <asp:ListItem Text="Deposit" Value="D"></asp:ListItem>
                                                                                                <asp:ListItem Text="Escrow" Value="E"></asp:ListItem>
                                                                                                <asp:ListItem Text="None" Value="N"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td align="left" colspan="3" valign="middle" class="blucolor blackbfont">
                                                                                            <asp:CheckBox ID="chkOutside" runat="server" />
                                                                                            Outside Equity Last
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </td>
                                                                </tr>
                                                                <asp:HiddenField ID="hdnBudget" runat="server" />
                                                            </table>
                                                        </ContentTemplate>
                                                    </asp:TabPanel>
                                                    <asp:TabPanel ID="tabPnlFinc" runat="server">
                                                        <HeaderTemplate>
                                                            Financial</HeaderTemplate>
                                                        <ContentTemplate>
                                                            <table width="100%" class="allgborder" cellpadding="1" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td align="left" valign="top">
                                                                        <table width="100%" align="left" cellpadding="0" cellspacing="0" border="0">
                                                                        </table>
                                                                        <tr>
                                                                            <td align="left" valign="top">
                                                                                <table align="center" border="0" cellpadding="5" cellspacing="0" class="bgcolor3"
                                                                                    width="100%">
                                                                                    <tr>
                                                                                        <td align="left" class="blackbfont" colspan="5" valign="middle">
                                                                                            Loan Commitment
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="left" valign="middle" width="29%">
                                                                                            &nbsp;&nbsp;
                                                                                        </td>
                                                                                        <td align="center" class="greybborder" valign="middle" width="20%">
                                                                                            &nbsp;&nbsp;
                                                                                        </td>
                                                                                        <td align="center" class="blucolor greybborder" valign="middle" width="18%">
                                                                                            Current
                                                                                        </td>
                                                                                        <td align="center" class="blucolor greybborder" valign="middle" width="16%">
                                                                                            Adjustment
                                                                                        </td>
                                                                                        <td align="center" class="blucolor greybborder" valign="middle" width="17%">
                                                                                            Result
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="left" class="blucolor greybborder" valign="middle">
                                                                                            Loan Commitment
                                                                                        </td>
                                                                                        <td align="center" class="greybborder" valign="middle">
                                                                                            &nbsp;&nbsp;
                                                                                        </td>
                                                                                        <td align="center" class="greybborder" valign="middle">
                                                                                            <asp1:TCLTextBox ID="txtFincTabLoanCommit1" runat="server" CssClass="txtboxAmt" Text='<%# FincTabLoanCommit1 %>'
                                                                                                Enabled="false" BindingName="FincTabLoanCommit1"></asp1:TCLTextBox>
                                                                                        </td>
                                                                                        <td align="center" class="greybborder" valign="middle">
                                                                                            <asp1:TCLTextBox ID="txtFincTabLoanCommit2" runat="server" CssClass="txtboxAmt" Text='<%# FincTabLoanCommit2 %>'
                                                                                                Enabled="<%# txtFincTabLoanCommit2Enabled %>" BindingName="FincTabLoanCommit2"
                                                                                                OnBlur="calculateAdjustment(this,FincTabLoanCommit1,FincTabLoanCommit3);Commmitment();" />
                                                                                            <asp:FilteredTextBoxExtender ID="txtGroupId_FilterIndex" runat="server" TargetControlID="txtFincTabLoanCommit2"
                                                                                                FilterType="Numbers, Custom" ValidChars="-,.$" Enabled="True" />
                                                                                        </td>
                                                                                        <td align="center" class="greybborder" valign="middle">
                                                                                            <asp1:TCLTextBox ID="txtFincTabLoanCommit3" runat="server" CssClass="txtboxAmt" Text='<%# FincTabLoanCommit3 %>'
                                                                                                Enabled="<%# txtFincTabLoanCommit3Enabled %>" OnBlur="calculateResult(this,FincTabLoanCommit1,FincTabLoanCommit2);Commmitment();"
                                                                                                BindingName="FincTabLoanCommit3" />
                                                                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtFincTabLoanCommit3"
                                                                                                FilterType="Numbers, Custom" ValidChars=",.$" Enabled="True" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="left" class="blucolor blackbfont" valign="middle">
                                                                                            Effective Date
                                                                                            <asp1:TCLTextBox ID="txtFincTabEffctDate1" runat="server" CssClass="txtbox" Text='<%# FincTabEffctDate1 %>'
                                                                                                BindingName="FincTabEffctDate1" />
                                                                                        </td>
                                                                                        <td align="left" class="blucolor blackbfont" valign="middle">
                                                                                            Trancode
                                                                                            <asp:DropDownList ID="drpdwnlstTranscode" runat="server" CssClass="ddlistbox" DataSource='<%# DictTransCode1 %>'
                                                                                                DataTextField="Key" DataValueField="Value" AppendDataBoundItems="true">
                                                                                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td align="left" valign="middle">
                                                                                            &nbsp;&nbsp;
                                                                                        </td>
                                                                                        <td align="center" valign="middle">
                                                                                            &nbsp;&nbsp;
                                                                                        </td>
                                                                                        <td align="center" valign="middle">
                                                                                            &nbsp;&nbsp;
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="left" class="greybborder" valign="middle">
                                                                                            Commitment Rule:
                                                                                        </td>
                                                                                        <td align="center" class="greybborder" valign="middle">
                                                                                            <asp:Label ID="lblComRule" Visible='<%# lblComRuleVisible %>' Text='<%# lblComRuleText %>'
                                                                                                runat="server"></asp:Label>
                                                                                        </td>
                                                                                        <td align="center" class="greybborder" valign="middle">
                                                                                            <asp:DropDownList ID="drpdwnlstComRule" runat="server" Visible='<%# drpdwnlstComRuleVisible %>'
                                                                                                Enabled='<%# drpdwnlstComRuleEnabled %>' CssClass="ddlistbox" DataSource='<%# DictComRules %>'
                                                                                                DataTextField="Key" DataValueField="value">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td align="center" class="greybborder" valign="middle">
                                                                                            <asp:Label ID="lblComRulePart0" Text='<%# lblComRulePart0 %>' Visible="false" Width="0px"
                                                                                                runat="server"></asp:Label>
                                                                                            <asp:Label ID="lblComRulePart1" Text='<%# lblComRulePart1 %>' Visible="false" Width="0px"
                                                                                                runat="server"></asp:Label>
                                                                                        </td>
                                                                                        <td align="center" class="greybborder" valign="middle">
                                                                                            <asp:Label ID="lblComRulePart2" Text='<%# lblComRulePart2 %>' Visible="false" Width="0px"
                                                                                                runat="server"></asp:Label>
                                                                                            <asp:Label ID="lblComRulePart3" Text='<%# lblComRulePart3 %>' Visible="false" Width="0px"
                                                                                                runat="server"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="left" class="greybborder" valign="middle">
                                                                                            <asp1:TCLCheckBox ID="chkORComRule" runat="server" Text="Override Rule:" Checked='<%# chkORComRuleChecked %>'
                                                                                                Enabled='<%# chkORComRuleEnabled %>' Visible='<%# chkORComRuleVisible %>' OnCheckedChanged="chkORComRule_Changed" />
                                                                                        </td>
                                                                                        <td align="center" class="greybborder" valign="middle">
                                                                                            <asp:Label ID="lblComRuleLong" Visible='<%# lblComRuleLongVisible %>' runat="server"
                                                                                                Text='<%# lblComRuleLongText %>'></asp:Label>
                                                                                        </td>
                                                                                        <td align="center" class="greybborder" valign="middle">
                                                                                            &nbsp;&nbsp;
                                                                                        </td>
                                                                                        <td align="center" class="greybborder" valign="middle">
                                                                                            &nbsp;&nbsp;
                                                                                        </td>
                                                                                        <td align="center" class="greybborder" valign="middle">
                                                                                            &nbsp;&nbsp;
                                                                                        </td>
                                                                                    </tr>
                                                                                    <asp:Panel ID="pnlPricipal" runat="server">
                                                                                        <tr>
                                                                                            <td align="left" class="blucolor blackbfont" valign="middle">
                                                                                                Principal Bal.Cap
                                                                                            </td>
                                                                                            <td align="center" valign="middle">
                                                                                                &nbsp;&nbsp;
                                                                                            </td>
                                                                                            <td align="center" valign="middle">
                                                                                                <asp1:TCLTextBox ID="txtFincTabprincBal1" runat="server" CssClass="txtboxAmt" Text='<%# FincTabprincBal1 %>'
                                                                                                    Enabled="false" BindingName="FincTabprincBal1" />
                                                                                            </td>
                                                                                            <td align="center" valign="middle">
                                                                                                <span class="greybborder">
                                                                                                    <asp1:TCLTextBox ID="txtFincTabprincBal2" runat="server" CssClass="txtboxAmt" Text='<%# FincTabprincBal2 %>'
                                                                                                        BindingName="FincTabprincBal2" onBlur="calculateAdjustment(this,FincTabprincBal1,FincTabprincBal3);" />
                                                                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtFincTabprincBal2"
                                                                                                        FilterType="Numbers, Custom" ValidChars="-,.$" Enabled="True" />
                                                                                                </span>
                                                                                            </td>
                                                                                            <td align="center" valign="middle">
                                                                                                <asp1:TCLTextBox ID="txtFincTabprincBal3" runat="server" CssClass="txtboxAmt" Text='<%# FincTabprincBal3 %>'
                                                                                                    OnBlur="calculateResult(this,FincTabprincBal1,FincTabprincBal2);" BindingName="FincTabprincBal3" />
                                                                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtFincTabprincBal3"
                                                                                                    FilterType="Numbers, Custom" ValidChars=",.$" Enabled="True" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </asp:Panel>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" valign="top">
                                                                                <table align="center" border="0" cellpadding="5" cellspacing="0" class="bgcolor3"
                                                                                    width="100%">
                                                                                    <tr>
                                                                                        <td align="left" class="blackbfont" colspan="5" valign="middle">
                                                                                            Loan Level Deposit
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="left" valign="middle" width="29%">
                                                                                            &nbsp;&nbsp;
                                                                                        </td>
                                                                                        <td align="center" class="greybborder" valign="middle" width="20%">
                                                                                            &nbsp;&nbsp;
                                                                                        </td>
                                                                                        <td align="center" class="blucolor greybborder" valign="middle" width="18%">
                                                                                            Current
                                                                                        </td>
                                                                                        <td align="center" class="blucolor greybborder" valign="middle" width="16%">
                                                                                            Adjustment
                                                                                        </td>
                                                                                        <td align="center" class="blucolor greybborder" valign="middle" width="17%">
                                                                                            Result
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="left" class="blucolor greybborder" valign="middle">
                                                                                            Loan Level Deposit
                                                                                        </td>
                                                                                        <td align="center" class="greybborder" valign="middle">
                                                                                            &nbsp;&nbsp;
                                                                                        </td>
                                                                                        <td align="center" class="greybborder" valign="middle">
                                                                                            <asp1:TCLTextBox ID="txtFincTabLnLvlDpst1" runat="server" CssClass="txtboxAmt" Text='<%# FincTabLnLvlDpst1 %>'
                                                                                                Enabled="false" BindingName="FincTabprincBal2" />
                                                                                        </td>
                                                                                        <td align="center" class="greybborder" valign="middle">
                                                                                            <asp1:TCLTextBox ID="txtFincTabLnLvlDpst2" runat="server" CssClass="txtboxAmt" Text='<%# FincTabLnLvlDpst2 %>'
                                                                                                BindingName="FincTabprincBal2" onBlur="calculateAdjustment(this,FincTabLnLvlDpst1,FincTabLnLvlDpst3);" />
                                                                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtFincTabLnLvlDpst2"
                                                                                                FilterType="Numbers, Custom" ValidChars="-,.$" Enabled="True" />
                                                                                        </td>
                                                                                        <td align="center" class="greybborder" valign="middle">
                                                                                            <asp1:TCLTextBox ID="txtFincTabLnLvlDpst3" runat="server" CssClass="txtboxAmt" Text='<%# FincTabLnLvlDpst3 %>'
                                                                                                BindingName="FincTabprincBal2" onBlur="calculateResult(this,FincTabLnLvlDpst1,FincTabLnLvlDpst2);" />
                                                                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtFincTabLnLvlDpst3"
                                                                                                FilterType="Numbers, Custom" ValidChars=",.$" Enabled="True" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="left" class="blucolor blackbfont" valign="middle">
                                                                                            Effective Date
                                                                                            <%--<input id="txtdt18" class="txtbox" type="text" value=" " />--%>
                                                                                            <asp1:TCLTextBox ID="txtFincTabEffctDate2" runat="server" CssClass="txtbox" Text='<%# FincTabEffctDate2 %>'
                                                                                                BindingName="FincTabEffctDate2" />
                                                                                        </td>
                                                                                        <td align="left" class="blucolor blackbfont" valign="middle">
                                                                                            Trancode
                                                                                            <asp:DropDownList ID="drpdwnlstLnvlDpstEftvTrncode" runat="server" CssClass="ddlistbox"
                                                                                                AppendDataBoundItems="true" DataSource='<%# DictTransCode2 %>' DataTextField="Key"
                                                                                                DataValueField="value">
                                                                                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td align="left" valign="middle">
                                                                                            &nbsp;&nbsp;
                                                                                        </td>
                                                                                        <td align="center" valign="middle">
                                                                                            &nbsp;&nbsp;
                                                                                        </td>
                                                                                        <td align="center" valign="middle">
                                                                                            &nbsp;&nbsp;
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" valign="top">
                                                                                <table align="center" border="0" cellpadding="5" cellspacing="0" width="100%">
                                                                                    <tr>
                                                                                        <td align="left" class="bgcolor6" valign="middle">
                                                                                            <table align="center" border="0" cellpadding="5" cellspacing="0" class="bgcolor3"
                                                                                                width="100%">
                                                                                                <tr>
                                                                                                    <td align="left" class="blackbfont" colspan="5" valign="middle">
                                                                                                        Loan Level Escrow
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td align="left" valign="middle" width="29%">
                                                                                                        &nbsp;&nbsp;
                                                                                                    </td>
                                                                                                    <td align="center" class="greybborder" valign="middle" width="20%">
                                                                                                        &nbsp;&nbsp;
                                                                                                    </td>
                                                                                                    <td align="center" class="blucolor greybborder" valign="middle" width="18%">
                                                                                                        Current
                                                                                                    </td>
                                                                                                    <td align="center" class="blucolor greybborder" valign="middle" width="16%">
                                                                                                        Adjustment
                                                                                                    </td>
                                                                                                    <td align="center" class="blucolor greybborder" valign="middle" width="17%">
                                                                                                        Result
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td align="left" class="blucolor greybborder" valign="middle">
                                                                                                        Loan Level Escrow
                                                                                                    </td>
                                                                                                    <td align="center" class="greybborder" valign="middle">
                                                                                                        &nbsp;&nbsp;
                                                                                                    </td>
                                                                                                    <td align="center" class="greybborder" valign="middle">
                                                                                                        <asp1:TCLTextBox ID="txtFincTabLnLvlEscrw1" runat="server" CssClass="txtboxAmt" Text='<%# FincTabLnLvlEscrw1 %>'
                                                                                                            Enabled="false" BindingName="FincTabLnLvlEscrw1" />
                                                                                                    </td>
                                                                                                    <td align="center" class="greybborder" valign="middle">
                                                                                                        <asp1:TCLTextBox ID="txtFincTabLnLvlEscrw2" runat="server" CssClass="txtboxAmt" Text='<%# FincTabLnLvlEscrw2 %>'
                                                                                                            onBlur="calculateAdjustment(this,FincTabLnLvlEscrw1,FincTabLnLvlEscrw3);" BindingName="FincTabLnLvlEscrw2" />
                                                                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtFincTabLnLvlEscrw2"
                                                                                                            FilterType="Numbers, Custom" ValidChars="-,.$" Enabled="True" />
                                                                                                    </td>
                                                                                                    <td align="center" class="greybborder" valign="middle">
                                                                                                        <asp1:TCLTextBox ID="txtFincTabLnLvlEscrw3" runat="server" CssClass="txtboxAmt" Text='<%# FincTabLnLvlEscrw3 %>'
                                                                                                            BindingName="FincTabLnLvlEscrw3" onBlur="calculateResult(this,FincTabLnLvlEscrw1,FincTabLnLvlEscrw2);" />
                                                                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtFincTabLnLvlEscrw3"
                                                                                                            FilterType="Numbers, Custom" ValidChars=",.$" Enabled="True" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td align="left" class="blucolor blackbfont" valign="middle">
                                                                                                        Effective Date
                                                                                                        <%--<input id="txtdt17" class="txtbox" type="text" value=" " />--%>
                                                                                                        <asp1:TCLTextBox ID="txtFincTabEffctDate3" runat="server" CssClass="txtbox" Text='<%# FincTabEffctDate3 %>'
                                                                                                            BindingName="FincTabEffctDate3" />
                                                                                                    </td>
                                                                                                    <td align="left" class="blucolor blackbfont" valign="middle">
                                                                                                        Trancode
                                                                                                        <asp:DropDownList ID="drpdwnlstLnLvlEscrwTrncode" runat="server" CssClass="ddlistbox"
                                                                                                            AppendDataBoundItems="true" DataSource='<%# DictTransCode3 %>' DataTextField="Key"
                                                                                                            DataValueField="Value">
                                                                                                            <asp:ListItem Text="" Value=""></asp:ListItem>
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                    <td align="left" valign="middle">
                                                                                                        &nbsp;&nbsp;
                                                                                                    </td>
                                                                                                    <td align="center" valign="middle">
                                                                                                        &nbsp;&nbsp;
                                                                                                    </td>
                                                                                                    <td align="center" valign="middle">
                                                                                                        &nbsp;&nbsp;
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="left" class="bgcolor6" valign="top">
                                                                                            <table align="center" border="0" cellpadding="5" cellspacing="0" class="bgcolor3"
                                                                                                width="100%">
                                                                                                <tr>
                                                                                                    <td align="left" class="blackbfont" colspan="5" valign="middle">
                                                                                                        Cost Estimates
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td align="left" valign="middle" width="29%">
                                                                                                        &nbsp;&nbsp;
                                                                                                    </td>
                                                                                                    <td align="center" class="greybborder" valign="middle" width="20%">
                                                                                                        &nbsp;&nbsp;
                                                                                                    </td>
                                                                                                    <td align="center" class="blucolor greybborder" valign="middle" width="18%">
                                                                                                        Current
                                                                                                    </td>
                                                                                                    <td align="center" class="blucolor greybborder" valign="middle" width="16%">
                                                                                                        Adjustment
                                                                                                    </td>
                                                                                                    <td align="center" class="blucolor greybborder" valign="middle" width="17%">
                                                                                                        Result
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td align="left" class="blucolor greybborder" valign="middle">
                                                                                                        Outside Equity
                                                                                                    </td>
                                                                                                    <td align="center" class="greybborder" valign="middle">
                                                                                                        &nbsp;&nbsp;
                                                                                                    </td>
                                                                                                    <td align="center" class="greybborder" valign="middle">
                                                                                                        <asp1:TCLTextBox ID="txtFincTabOutEqtyCurrnt" runat="server" CssClass="txtboxAmt"
                                                                                                            Text='<%# FincTabOutEqtyCurrnt %>' BindingName="FincTabOutEqtyCurrnt" Enabled="false" />
                                                                                                    </td>
                                                                                                    <td align="center" class="greybborder" valign="middle">
                                                                                                        <asp1:TCLTextBox ID="txtFincTabOutEqtyAdjstmnt" runat="server" CssClass="txtboxAmt"
                                                                                                            onBlur="calculateAdjustment(this,FincTabOutEqtyCurrnt,FincTabOutEqtyRslt);" Text='<%# FincTabOutEqtyAdjstmnt %>'
                                                                                                            BindingName="FincTabOutEqtyAdjstmnt" />
                                                                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txtFincTabOutEqtyAdjstmnt"
                                                                                                            FilterType="Numbers, Custom" ValidChars="-,.$" Enabled="True" />
                                                                                                    </td>
                                                                                                    <td align="center" class="greybborder" valign="middle">
                                                                                                        <asp1:TCLTextBox ID="txtFincTabOutEqtyRslt" runat="server" CssClass="txtboxAmt" Text='<%# FincTabOutEqtyRslt %>'
                                                                                                            BindingName="FincTabOutEqtyRslt" onBlur="calculateResult(this,FincTabOutEqtyCurrnt,FincTabOutEqtyAdjstmnt);" />
                                                                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="txtFincTabOutEqtyRslt"
                                                                                                            FilterType="Numbers, Custom" ValidChars=",.$" Enabled="True" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td align="left" class="blucolor greybborder" valign="middle">
                                                                                                        Total
                                                                                                    </td>
                                                                                                    <td align="left" class="greybborder" valign="middle">
                                                                                                        &nbsp;&nbsp;
                                                                                                    </td>
                                                                                                    <td align="center" class="greybborder" valign="middle">
                                                                                                        <asp1:TCLTextBox ID="txtFincTabTotlCurnt" runat="server" CssClass="txtboxAmt" Text='<%# FincTabTotlCurnt %>'
                                                                                                            BindingName="FincTabTotlCurnt" Enabled="false" />
                                                                                                    </td>
                                                                                                    <td align="center" class="greybborder" valign="middle">
                                                                                                        <asp1:TCLTextBox ID="txtFincTabTotlAdjsmnt" runat="server" CssClass="txtboxAmt" Text='<%# FincTabTotlAdjsmnt %>'
                                                                                                            BindingName="FincTabTotlAdjsmnt" onBlur="calculateAdjustment(this,FincTabTotlCurnt,FincTabTotlRslt);TotalEstimated();" />
                                                                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="txtFincTabTotlAdjsmnt"
                                                                                                            FilterType="Numbers, Custom" ValidChars="-,.$" Enabled="True" />
                                                                                                        </span>
                                                                                                    </td>
                                                                                                    <td align="center" class="greybborder" valign="middle">
                                                                                                        <asp1:TCLTextBox ID="txtFincTabTotlRslt" runat="server" CssClass="txtboxAmt" Text='<%# FincTabTotlRslt %>'
                                                                                                            BindingName="FincTabTotlRslt" onBlur="calculateResult(this,FincTabTotlCurnt,FincTabTotlAdjsmnt);TotalEstimated();" />
                                                                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" TargetControlID="txtFincTabTotlRslt"
                                                                                                            FilterType="Numbers, Custom" ValidChars=",.$" Enabled="True" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td align="left" class="blucolor greybborder" valign="middle">
                                                                                                        Construction
                                                                                                    </td>
                                                                                                    <td align="center" class="greybborder" valign="middle">
                                                                                                        &nbsp;&nbsp;
                                                                                                    </td>
                                                                                                    <td align="center" class="greybborder" valign="middle">
                                                                                                        <asp1:TCLTextBox ID="txtFincTabConstCurrnt" runat="server" CssClass="txtboxAmt" Text='<%# FincTabConstCurrnt %>'
                                                                                                            Enabled="false" BindingName="FincTabConstCurrnt" />
                                                                                                    </td>
                                                                                                    <td align="center" class="greybborder" valign="middle">
                                                                                                        <asp1:TCLTextBox ID="txtFincTabConstAdjsmnt" runat="server" CssClass="txtboxAmt"
                                                                                                            Text='<%# FincTabConstAdjsmnt %>' BindingName="FincTabConstAdjsmnt" onBlur="calculateAdjustment(this,FincTabConstCurrnt,FincTabConstRslt);" />
                                                                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" TargetControlID="txtFincTabConstAdjsmnt"
                                                                                                            FilterType="Numbers, Custom" ValidChars="-,.$" Enabled="True" />
                                                                                                    </td>
                                                                                                    <td align="center" class="greybborder" valign="middle">
                                                                                                        <asp1:TCLTextBox ID="txtFincTabConstRslt" runat="server" CssClass="txtboxAmt" Text='<%# FincTabConstRslt %>'
                                                                                                            BindingName="FincTabConstRslt" onBlur="calculateResult(this,FincTabConstCurrnt,FincTabConstAdjsmnt);" />
                                                                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" TargetControlID="txtFincTabConstRslt"
                                                                                                            FilterType="Numbers, Custom" ValidChars=",.$" Enabled="True" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" class="pr10 greyfooter" valign="middle">
                                                                                <asp:Button ID="btnFincTabPostFee" runat="server" CssClass="btnstyle" Text="Post Fees"
                                                                                    OnClick="btnFincTabPostFee_Click" />
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                    </asp:TabPanel>
                                                    <asp:TabPanel ID="tabPnlProfiles" runat="server">
                                                        <HeaderTemplate>
                                                            Profiles</HeaderTemplate>
                                                        <ContentTemplate>
                                                            <table width="100%" class="allgborder" cellpadding="1" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TabContainer ID="tabcontainterProfiles" runat="server" ActiveTabIndex="0" CssClass="Tab">
                                                                            <asp:TabPanel ID="tabpnlInterestPrfl" runat="server">
                                                                                <HeaderTemplate>
                                                                                    Interest Profiles
                                                                                </HeaderTemplate>
                                                                                <ContentTemplate>
                                                                                    <table border="0" cellpadding="1" cellspacing="0" class="allgborder" width="100%">
                                                                                        <tr>
                                                                                            <td align="left" valign="top">
                                                                                                <table align="center" border="0" cellpadding="5" cellspacing="0" width="100%">
                                                                                                    <tr>
                                                                                                        <td align="left" class="bgcolor6" valign="top">
                                                                                                            <table align="center" border="0" cellpadding="0" cellspacing="1" class="allborder"
                                                                                                                width="100%">
                                                                                                                <tr class="gridheaderbg">
                                                                                                                    <td align="left" class="pl10" valign="middle">
                                                                                                                        <asp:ImageButton ID="imgbtnIntrstPrflsAdd" runat="server" AlternateText="Add" border="0"
                                                                                                                            CssClass="curpointer" hspace="0" ImageAlign="Middle" ImageUrl="~/Images/gicon02.png"
                                                                                                                            OnClick="ImgbtnIntrstPrflsAdd_Click" ToolTip="Add" vspace="0" />
                                                                                                                        &nbsp;<asp:ImageButton ID="imgbtnIntrstPrflsDel" runat="server" AlternateText="Delete"
                                                                                                                            border="0" CssClass="curpointer" hspace="0" ImageAlign="Middle" ImageUrl="~/Images/gicon04.png"
                                                                                                                            OnClick="IPImgbtnDelete_Click" ToolTip="Delete" vspace="0" OnClientClick="ValidateIP();" />
                                                                                                                        &nbsp;<asp:ImageButton ID="imgbtnIntrstPrflsCopy" runat="server" AlternateText="Copy"
                                                                                                                            border="0" CssClass="curpointer" hspace="0" ImageAlign="Middle" ImageUrl="~/Images/copy.png"
                                                                                                                            OnClick="ImgbtnIntrstPrflsCopy_Click" OnClientClick="return ValidateCopyIP();"
                                                                                                                            ToolTip="Copy" vspace="0" />
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <div id="pnlInterestGrid" class="divscrollcss" style="overflow-x: scroll; height: 200px;">
                                                                                                                            <CustomGid:ExtendGrid ID="CGIntrestProfile" runat="server" OnGridCheckedChange="CGIntrestProfile_GridCheckedChange"
                                                                                                                                OnGridRowEditing="CGIntrestProfile_GridRowEditing" GridAllowPaging="True" GridRowIndex="0"
                                                                                                                                GridSelectedRowStyleCSS="BlueViolet" GridShowFooter="False" GridWidth="100%"
                                                                                                                                ImageAddButtonEnabled="False" ImageAddButtonToolTip="Add" ImageAddButtonURL="/Images/gicon02.png"
                                                                                                                                ImageAdditionalButton1URL="/Images/gicon02.png" ImageAdditionalButton2URL="/Images/ArrowRightSmall.png"
                                                                                                                                ImageAdditionalButton3URL="/Images/gicon04.png" ImageAdditionalButton4URL="/Images/copy.png"
                                                                                                                                ImageAdditionalButton5URL="/Images/gicon02.png" ImageAdditionalButton6URL="/Images/ArrowRightSmall.png"
                                                                                                                                ImageAdditionalButton7URL="/Images/gicon04.png" ImageAdditionalButton8URL="/Images/copy.png"
                                                                                                                                ImageAddtionalButton1Enabled="False" ImageAddtionalButton2Enabled="False" ImageAddtionalButton3Enabled="False"
                                                                                                                                ImageAddtionalButton4Enabled="False" ImageAddtionalButton5Enabled="False" ImageAddtionalButton6Enabled="False"
                                                                                                                                ImageAddtionalButton7Enabled="False" ImageAddtionalButton8Enabled="False" ImageCopyButtonEnabled="False"
                                                                                                                                ImageCopyButtonToolTip="Copy" ImageCopyButtonURL="/Images/copy.png" ImageDeleteButtonEnabled="False"
                                                                                                                                ImageDeleteButtonToolTip="Delete" ImageDeleteButtonURL="/Images/gicon04.png"
                                                                                                                                ImageEditButtonEnabled="False" ImageEditButtonToolTip="Edit" ImageEditButtonURL="/Images/ArrowRightSmall.png"
                                                                                                                                ImageFirstURL="/Images/LeftAllArrow.png" ImageLastURL="/Images/RightAllArrow.png"
                                                                                                                                ImageNextURL="/Images/RightArrow.png" ImagePreviousURL="/Images/LeftArrow.png"
                                                                                                                                PageNumber="1" SortOrder="ASC" />
                                                                                                                        </div>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td align="right" class="pr10 greyfooter" valign="middle">
                                                                                                            <asp:Button ID="btnTabIntrstprflTranches" runat="server" CssClass="btnstyle" Text="Tranches"
                                                                                                                OnClick="btnTabIntrstprflTranches_Click" />
                                                                                                            <asp:Button ID="btnIPTemp" runat="server" OnClick="btnIPTemp_Click" Style="visibility: hidden" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </ContentTemplate>
                                                                            </asp:TabPanel>
                                                                            <asp:TabPanel ID="tabPnlEquityPrfl" runat="server">
                                                                                <HeaderTemplate>
                                                                                    Equity Profiles
                                                                                </HeaderTemplate>
                                                                                <ContentTemplate>
                                                                                    <table border="0" cellpadding="1" cellspacing="0" class="allgborder" width="100%">
                                                                                        <tr>
                                                                                            <td align="left" valign="top">
                                                                                                <table align="left" border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                                    <tr>
                                                                                                        <td align="left" class="pl10 bbggridhead tablehdfont dotbtborder" valign="middle"
                                                                                                            width="*">
                                                                                                            Equity Profiles
                                                                                                        </td>
                                                                                                        <td align="right" class="dotbtborder" height="35px" valign="top" width="110px">
                                                                                                            <img border="0" hspace="0" src="../../Images/yobg.png" vspace="0" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="left" valign="top">
                                                                                                <table align="center" border="0" cellpadding="5" cellspacing="0" width="100%">
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="left" valign="top">
                                                                                                <table align="center" border="0" cellpadding="5" cellspacing="0" width="100%">
                                                                                                    <tr>
                                                                                                        <td align="left" class="bgcolor6" valign="top">
                                                                                                            <table align="center" border="0" cellpadding="0" cellspacing="1" class="allborder"
                                                                                                                width="100%">
                                                                                                                <tr class="gridheaderbg">
                                                                                                                    <td align="left" class="pl10" valign="middle">
                                                                                                                        <asp:ImageButton ID="imgbtEqtyPrflsAdd" runat="server" AlternateText="Add" border="0"
                                                                                                                            CssClass="curpointer" hspace="0" ImageAlign="Middle" ImageUrl="~/Images/gicon02.png"
                                                                                                                            OnClick="ImgbtEqtyPrflsAdd_Click" ToolTip="Add" vspace="0" />
                                                                                                                        &nbsp;
                                                                                                                        <asp:ImageButton ID="imgbtEqtyPrflsDel" runat="server" AlternateText="Delete" border="0"
                                                                                                                            CssClass="curpointer" hspace="0" ImageAlign="Middle" ImageUrl="~/Images/gicon04.png"
                                                                                                                            OnClick="imgbtEqtyPrflsDel_Click" OnClientClick="return ValidateEP();" ToolTip="Delete"
                                                                                                                            vspace="0" />
                                                                                                                        &nbsp;<asp:ImageButton ID="imgbtEqtyPrflsCopy" runat="server" AlternateText="Copy"
                                                                                                                            border="0" CssClass="curpointer" hspace="0" ImageAlign="Middle" ImageUrl="~/Images/copy.png"
                                                                                                                            OnClick="imgbtEqtyPrflsCopy_Click" OnClientClick="return ValidateCopyEP();" ToolTip="Copy"
                                                                                                                            vspace="0" />
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <div id="pnlEquityGrid" class="divscrollcss" style="overflow-x: auto; overflow-y: auto;">
                                                                                                                            <CustomGid:ExtendGrid ID="CGEquityProfile" runat="server" OnGridCheckedChange="CGEquityProfile_GridCheckedChange"
                                                                                                                                OnGridRowEditing="CGEquityProfile_GridRowEditing" />
                                                                                                                        </div>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td align="right" class="pr10 greyfooter" valign="middle">
                                                                                                            <asp:Button ID="btnEPTemp" runat="server" OnClick="btnEPTemp_Click" Style="visibility: hidden" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </ContentTemplate>
                                                                            </asp:TabPanel>
                                                                        </asp:TabContainer>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                    </asp:TabPanel>
                                                    <asp:TabPanel ID="tabPnlAtchmnt" runat="server">
                                                        <HeaderTemplate>
                                                            Documents</HeaderTemplate>
                                                        <ContentTemplate>
                                                            <table width="100%" class="allgborder" cellpadding="1" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TabContainer ID="tabcontainerAttachemnts" runat="server" ActiveTabIndex="1"
                                                                            AutoPostBack="True" CssClass="Tab">
                                                                            <asp:TabPanel ID="tabpnlDocTrack" runat="server">
                                                                                <HeaderTemplate>
                                                                                    Doc Tracking
                                                                                </HeaderTemplate>
                                                                                <ContentTemplate>
                                                                                    <table border="0" cellpadding="1" cellspacing="0" class="allgborder" width="100%">
                                                                                        <tr>
                                                                                            <td align="left" valign="top">
                                                                                                <table align="left" border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="left" valign="top">
                                                                                                <table align="center" border="0" cellpadding="5" cellspacing="0" width="100%">
                                                                                                    <tr>
                                                                                                        <td align="center" class="bgcolor6" valign="top" width="41%">
                                                                                                            Standard Document Templates
                                                                                                        </td>
                                                                                                        <td align="left" class="bgcolor1" valign="top" width="18%">
                                                                                                            &nbsp;&nbsp;
                                                                                                        </td>
                                                                                                        <td align="center" class="bgcolor6" valign="top" width="41%">
                                                                                                            Attached to Note
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td align="center" class="bgcolor6" valign="middle">
                                                                                                            <asp:ListBox ID="lstbStndardArtchBrwDocTrack" runat="server" CssClass="textareafont"
                                                                                                                AutoPostBack="true" DataSource="<%# dsDockTracking %>" DataTextField="DESCRIP"
                                                                                                                DataValueField="DESCRIP" OnSelectedIndexChanged="lstbStndardArtchBrwDocTrack_SelectedIndexChanged"
                                                                                                                SelectionMode="Multiple" Style="width: 95%; height: 100px"></asp:ListBox>
                                                                                                        </td>
                                                                                                        <td align="left" class="bgcolor1" valign="middle">
                                                                                                            <table align="center" border="0" cellpadding="5" cellspacing="5" width="23%">
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <asp:Button ID="btnDoctMove" runat="server" CssClass="btnstyle" OnClick="btnDoctMove_Click"
                                                                                                                            Enabled="false" Text="&gt;" />
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <asp:Button ID="btnDocRemove" runat="server" CssClass="btnstyle" OnClick="btnDocRemove_Click"
                                                                                                                            Enabled="false" Text="&lt;" />
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                        <td align="center" class="bgcolor6" valign="middle">
                                                                                                            <asp:ImageButton ID="imgbtnDocTracAdd" runat="server" AlternateText="Add" ImageAlign="Middle"
                                                                                                                ImageUrl="~/Images/gicon02.png" ToolTip="Add" Visible="false" OnClick="imgbtnDocTracAdd_Click" />
                                                                                                            &nbsp;
                                                                                                            <asp:ImageButton ID="imgbtnDocTracEdit" Visible="false" runat="server" AlternateText="Edit"
                                                                                                                ImageAlign="Middle" ImageUrl="~/Images/gicon03.png" ToolTip="Edit" OnClick="imgbtnDocTracEdit_Click" />
                                                                                                            &nbsp;
                                                                                                            <%--   <asp:ImageButton ID="imgbtnDocTracDel" runat="server" AlternateText="Delete" ToolTip="Delete"
                                                                                                                        ImageAlign="Middle" ImageUrl="~/Images/gicon04.png" />&nbsp;--%>
                                                                                                            <br />
                                                                                                            <asp:ListBox ID="lstbArtchBrwDocTrack" runat="server" CssClass="textareafont" DataSource="<%# dsDockTracking2 %>"
                                                                                                                AutoPostBack="true" DataTextField="DOC" DataValueField="DOC" OnSelectedIndexChanged="lstbArtchBrwDocTrack_SelectedIndexChanged"
                                                                                                                SelectionMode="Multiple" Style="width: 95%; height: 100px"></asp:ListBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="right" class="pr10 greyfooter" colspan="6" valign="middle">
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </ContentTemplate>
                                                                            </asp:TabPanel>
                                                                            <asp:TabPanel ID="tblpnlAttachmets" runat="server">
                                                                                <HeaderTemplate>
                                                                                    Attachments
                                                                                </HeaderTemplate>
                                                                                <ContentTemplate>
                                                                                    <table border="0" cellpadding="1" cellspacing="0" class="allgborder" width="100%">
                                                                                        <tr>
                                                                                            <td align="left" valign="top">
                                                                                                <table align="left" border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="left" valign="top">
                                                                                                <table align="center" border="0" cellpadding="0" cellspacing="1" class="allborder"
                                                                                                    width="100%">
                                                                                                    <tr>
                                                                                                        <td align="left" valign="middle">
                                                                                                            <table align="center" border="0" cellpadding="5" cellspacing="1" class="gridheaderbg"
                                                                                                                width="100%">
                                                                                                                <tr>
                                                                                                                    <td align="left" class="pl10" valign="middle">
                                                                                                                        <asp:ImageButton ID="imgbtnAtchmntsAdd" runat="server" AlternateText="Add" border="0"
                                                                                                                            CssClass="curpointer" hspace="0" ImageAlign="Middle" ImageUrl="~/Images/gicon13.png"
                                                                                                                            OnClick="imgbtnAtchmntsAdd_Click" ToolTip="Attach" vspace="0" />
                                                                                                                        &nbsp;
                                                                                                                        <asp:ImageButton ID="imgbtnAtchmntsDel" runat="server" AlternateText="Delete" border="0"
                                                                                                                            CssClass="curpointer" hspace="0" ImageAlign="Middle" ImageUrl="~/Images/gicon04.png"
                                                                                                                            ToolTip="Delete" vspace="0" Enabled="False" OnClick="imgbtnAtchmntsDel_Click" />
                                                                                                                        &nbsp;
                                                                                                                        <asp:ImageButton ID="imgbtnAttachDetails" runat="server" AlternateText="Details"
                                                                                                                            CssClass="curpointer" ImageAlign="AbsMiddle" ImageUrl="~/Images/application_view_list.png"
                                                                                                                            OnClick="imgbtnAttachDetails_Click" ToolTip="Details" Enabled="False" />
                                                                                                                        &nbsp;
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <CustomGid:ExtendGrid ID="gvAttachments" runat="server" GridAllowPaging="True" GridRowIndex="0"
                                                                                                                GridSelectedRowStyleCSS="BlueViolet" GridShowFooter="False" GridWidth="100%"
                                                                                                                ImageAddButtonEnabled="False" ImageAddButtonToolTip="Add" ImageAddButtonURL="/Images/gicon02.png"
                                                                                                                ImageAdditionalButton1URL="/Images/gicon02.png" ImageAdditionalButton2URL="/Images/ArrowRightSmall.png"
                                                                                                                ImageAdditionalButton3URL="/Images/gicon04.png" ImageAdditionalButton4URL="/Images/copy.png"
                                                                                                                ImageAdditionalButton5URL="/Images/gicon02.png" ImageAdditionalButton6URL="/Images/ArrowRightSmall.png"
                                                                                                                ImageAdditionalButton7URL="/Images/gicon04.png" ImageAdditionalButton8URL="/Images/copy.png"
                                                                                                                ImageAddtionalButton1Enabled="False" ImageAddtionalButton2Enabled="False" ImageAddtionalButton3Enabled="False"
                                                                                                                ImageAddtionalButton4Enabled="False" ImageAddtionalButton5Enabled="False" ImageAddtionalButton6Enabled="False"
                                                                                                                ImageAddtionalButton7Enabled="False" ImageAddtionalButton8Enabled="False" ImageCopyButtonEnabled="False"
                                                                                                                ImageCopyButtonToolTip="Copy" ImageCopyButtonURL="/Images/copy.png" ImageDeleteButtonEnabled="False"
                                                                                                                ImageDeleteButtonToolTip="Delete" ImageDeleteButtonURL="/Images/gicon04.png"
                                                                                                                ImageEditButtonEnabled="False" ImageEditButtonToolTip="Edit" ImageEditButtonURL="/Images/ArrowRightSmall.png"
                                                                                                                ImageFirstURL="/Images/LeftAllArrow.png" ImageLastURL="/Images/RightAllArrow.png"
                                                                                                                ImageNextURL="/Images/RightArrow.png" ImagePreviousURL="/Images/LeftArrow.png"
                                                                                                                PageNumber="1" SortOrder="ASC" OnGridCheckedChange="gvAttachments_GridCheckedChange"
                                                                                                                OnGridRowEditing="gvAttachments_GridRowEditing" OnGridRowCreated="gvAttachments_GridRowCreated"
                                                                                                                OnGridPaging="gvAttachments_GridPaging" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="right" class="pr10 greyfooter" valign="middle">
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </ContentTemplate>
                                                                            </asp:TabPanel>
                                                                        </asp:TabContainer>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                    </asp:TabPanel>
                                                    <asp:TabPanel ID="tabPnlBrwBasTerms" runat="server">
                                                        <HeaderTemplate>
                                                            Borrowing Base Terms</HeaderTemplate>
                                                        <ContentTemplate>
                                                            <table width="100%" class="allgborder" cellpadding="1" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td align="left" valign="top">
                                                                        <CustomGid:ExtendGrid ID="CstGrdBrwBaseTerms" GridRowStyleCSS="normalrow" GridHeaderRowCSS="pl10 pr10 gridheader"
                                                                            GridAlternatingRowCSS="altrow" OnGridRowEditing="CstGrdBrwBaseTerms_OnGridRowEditing"
                                                                            runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">
                                                                        <asp:Button ID="btnBrwBaseMngTrms" runat="server" CssClass="btnstylebig" OnClientClick="return WOpen('bbmt','');"
                                                                            Text="Manage Terms" />
                                                                        <asp:Button ID="btnRedirectTerms" runat="server" OnClick="btnRedirectTerms_Click"
                                                                            Style="display: none" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                    </asp:TabPanel>
                                                    <asp:TabPanel ID="tabPnlBrwBasUnits" runat="server">
                                                        <HeaderTemplate>
                                                            Borrowing Base Units
                                                        </HeaderTemplate>
                                                        <ContentTemplate>
                                                            <table width="100%" class="allgborder" cellpadding="1" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td align="left" valign="top">
                                                                        <table width="100%" align="left" cellpadding="0" cellspacing="0" border="0">
                                                                        </table>
                                                                        <tr>
                                                                            <td align="left" valign="top">
                                                                                <table align="center" border="0" cellpadding="0" cellspacing="1" class="allborder"
                                                                                    width="100%">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <CustomGid:ExtendGrid ID="GVUnitTerms" runat="server" GridWidth="100%" GridHeaderRowCSS="pl10 pr10 gridheader"
                                                                                                GridAlternatingRowCSS="altrow" GridRowStyleCSS="normalrow" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                                    <tr>
                                                                                        <td width="25%">
                                                                                            <asp:CheckBox ID="chkPostInspections" runat="server" Text="Post Inspections Using Budget Processing" />
                                                                                        </td>
                                                                                        <td width="20%">
                                                                                            <asp:CheckBox ID="chkStartPipeline" runat="server" Text="New Units start in Pipeline" />
                                                                                        </td>
                                                                                        <td align="right" valign="top">
                                                                                            <asp:Button ID="btnBrwBaseFilter" runat="server" CssClass="btnstylebig" OnClientClick="return WOpen('bbuf',1);"
                                                                                                Text="Filter" />
                                                                                            &nbsp;&nbsp;
                                                                                            <asp:Button ID="btnBrwBaseMangUnits" runat="server" CssClass="btnstylebig" OnClientClick="return WOpen('bbmu','');"
                                                                                                Text="Manage Units" />
                                                                                            <asp:Button ID="btnRedirectUnits" Style="display: none" runat="server" OnClick="btnRedirectUnits_Click">
                                                                                            </asp:Button>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" valign="top">
                                                                                <table align="center" border="0" cellpadding="5" cellspacing="0" class="bgcolor6"
                                                                                    width="100%">
                                                                                    <tr>
                                                                                        <td align="left" class="bgcolor4 blackbfont" colspan="6" valign="middle">
                                                                                            Audit Log
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="left" class="blucolor blackbfont" valign="middle" width="10%">
                                                                                            Reconciled Never
                                                                                        </td>
                                                                                        <td align="left" valign="middle" width="1%">
                                                                                            &nbsp;&nbsp;
                                                                                        </td>
                                                                                        <td align="left" valign="middle" width="*">
                                                                                            <asp:TextBox ID="txtAuditLog1" runat="server" CssClass="txtbox"></asp:TextBox>
                                                                                            <%--<asp:TextBox ID="txtAuditLog1" runat="server" CssClass="txtbox"></asp:TextBox>--%>
                                                                                            &nbsp;<asp:Button ID="btnBrwBstabLogUnit1" runat="server" CssClass="btnstyle" OnClientClick="return WOpen('bbu','3');"
                                                                                                Text="Log" />
                                                                                        </td>
                                                                                        <td align="left" class="blucolor blackbfont" valign="middle" width="15%">
                                                                                            Audited Never
                                                                                        </td>
                                                                                        <td align="left" valign="middle" width="1%">
                                                                                            &nbsp;&nbsp;
                                                                                        </td>
                                                                                        <td align="left" valign="middle" width="*">
                                                                                            <asp:TextBox ID="txtAuditLog2" runat="server" CssClass="txtbox"></asp:TextBox>
                                                                                            &nbsp;&nbsp;<asp:Button ID="btnBrwBstabLogUnit2" runat="server" CssClass="btnstyle"
                                                                                                OnClientClick="return WOpen('bbu','1');" Text="Log" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="left" colspan="6" valign="middle">
                                                                                            <table align="center" border="0" cellpadding="0" cellspacing="1" class="allborder"
                                                                                                width="100%">
                                                                                                <tr>
                                                                                                    <td align="left" class="gridheaderbg" valign="middle">
                                                                                                        <asp:ImageButton ID="ImgAuditAdd" runat="server" ImageAlign="Middle" ImageUrl="~/Images/gicon02.png"
                                                                                                            AlternateText="Add" ToolTip="Add" OnClientClick="return WOpen('bbu','2');" />&nbsp;
                                                                                                        <asp:ImageButton ID="ImgAuditDelete" runat="server" ImageAlign="Middle" ImageUrl="~/Images/gicon04.png"
                                                                                                            OnClick="ImgAuditDelete_Click" AlternateText="Delete" ToolTip="Delete" />
                                                                                                        <asp:HiddenField ID="hdnAuditDelete" runat="server" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <CustomGid:ExtendGrid ID="GVUnitAuditLog" runat="server" GridWidth="100%" GridHeaderRowCSS="pl10 pr10 gridheader"
                                                                                                            GridAlternatingRowCSS="altrow" GridRowStyleCSS="normalrow" OnGridCheckedChange="GVUnitAuditLog_GridCheckedChange"
                                                                                                            OnGridRowEditing="GVUnitAuditLog_GridRowEditing" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" class="pr10 greyfooter" valign="middle">
                                                                            </td>
                                                                        </tr>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                    </asp:TabPanel>
                                                    <asp:TabPanel ID="tabPnlkyCnt" runat="server">
                                                        <HeaderTemplate>
                                                            Key Contacts</HeaderTemplate>
                                                        <ContentTemplate>
                                                            <table width="100%" class="allgborder" cellpadding="1" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Button runat="server" ID="btnHideKeyContects" Style="visibility: hidden" OnClick="btnHideKeyContects_Click" />
                                                                        <table width="100%" class="allgborder" cellpadding="1" cellspacing="0" border="0">
                                                                            <tr>
                                                                                <td align="left" valign="top">
                                                                                    <table width="100%" align="left" cellpadding="0" cellspacing="0" border="0">
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left" valign="top">
                                                                                    <table width="100%" align="center" cellpadding="5" cellspacing="0" border="0">
                                                                                        <tr>
                                                                                            <td colspan="1" align="center" valign="top" class="blackbfont bgcolor6">
                                                                                                Key Contacts Templates
                                                                                            </td>
                                                                                            <td width="18%" align="left" valign="top" class="bgcolor1">
                                                                                                &#160;&#160;
                                                                                            </td>
                                                                                            <td width="41%" align="center" valign="top" class="blackbfont bgcolor6">
                                                                                                Attached Key Contacts
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="left" valign="top" class="bgcolor6">
                                                                                                <table width="100%" border="0" cellspacing="8" cellpadding="0">
                                                                                                    <tr>
                                                                                                        <td width="34%">
                                                                                                            <asp:DropDownList ID="ddlKeyContacts" runat="server" CssClass="ddlistbox" AutoPostBack="True"
                                                                                                                OnSelectedIndexChanged="ddlKeyContacts_SelectedIndexChanged">
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                        <td width="25%" nowrap="nowrap">
                                                                                                            <span class="bgcolor3" class="blucolor blackbfont">Find Contact </span>
                                                                                                        </td>
                                                                                                        <td width="32%" valign="middle">
                                                                                                            <asp:TextBox ID="txtFindContact" runat="server" Style="height: 12px;"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td width="5%" valign="middle">
                                                                                                            <asp:ImageButton ID="btnFind" runat="server" OnClick="btnFind_Click" ToolTip="Find"
                                                                                                                ImageUrl="~/Images/gicon01.png" vspace="0" hspace="0" border="0" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                            <td align="left" valign="top" class="bgcolor1">
                                                                                                &#160;&#160;
                                                                                            </td>
                                                                                            <td align="left" valign="top" class="bgcolor6">
                                                                                                &#160;&#160;
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="center" valign="top" class="bgcolor6">
                                                                                                <table border="0" cellspacing="1" cellpadding="0" width="100%" align="center" class="allborder">
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <CustomGid:ExtendGrid runat="server" ID="csKeyContacts" GridAllowPaging="True" GridSelectedRowStyleCSS="BlueViolet"
                                                                                                                GridShowFooter="False" GridWidth="100%" ImageAddButtonEnabled="False" ImageAddButtonURL="/Images/gicon02.png"
                                                                                                                ImageAdditionalButton1URL="/Images/gicon02.png" ImageAdditionalButton2URL="/Images/ArrowRightSmall.png"
                                                                                                                ImageAdditionalButton3URL="/Images/gicon04.png" ImageAdditionalButton4URL="/Images/copy.png"
                                                                                                                ImageAdditionalButton5URL="/Images/gicon02.png" ImageAdditionalButton6URL="/Images/ArrowRightSmall.png"
                                                                                                                ImageAdditionalButton7URL="/Images/gicon04.png" ImageAdditionalButton8URL="/Images/copy.png"
                                                                                                                ImageAddtionalButton1Enabled="False" ImageAddtionalButton2Enabled="False" ImageAddtionalButton3Enabled="False"
                                                                                                                ImageAddtionalButton4Enabled="False" ImageAddtionalButton5Enabled="False" ImageAddtionalButton6Enabled="False"
                                                                                                                ImageAddtionalButton7Enabled="False" ImageAddtionalButton8Enabled="False" ImageCopyButtonEnabled="False"
                                                                                                                ImageCopyButtonURL="/Images/copy.png" ImageDeleteButtonEnabled="False" ImageDeleteButtonURL="/Images/gicon04.png"
                                                                                                                ImageEditButtonEnabled="False" ImageEditButtonURL="/Images/ArrowRightSmall.png"
                                                                                                                ImageFirstURL="/Images/LeftAllArrow.png" ImageLastURL="/Images/RightAllArrow.png"
                                                                                                                ImageNextURL="/Images/RightArrow.png" ImagePreviousURL="/Images/LeftArrow.png"
                                                                                                                PageNumber="1" ImageAddButtonToolTip="Add" ImageCopyButtonToolTip="Copy" ImageDeleteButtonToolTip="Delete"
                                                                                                                ImageEditButtonToolTip="Edit" SortOrder="ASC" OnGridCheckedChange="csKeyContacts_GridCheckedChange"
                                                                                                                GridRowIndex="0" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <asp:Panel ID="pnlKeyContact" runat="server" Visible="false">
                                                                                                        <tr>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblRec1" runat="server" Text="No Records to Display!!!"></asp:Label>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </asp:Panel>
                                                                                                </table>
                                                                                            </td>
                                                                                            <td align="left" valign="top" class="bgcolor1">
                                                                                                <table width="23%" border="0" align="center" cellpadding="5" cellspacing="5">
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <asp:Button ID="btnKeyCntsMoveRight" runat="server" CssClass="btnstyle" Text="&gt;"
                                                                                                                OnClick="btnKeyCntsMoveRight_Click" Enabled="False" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                            <td align="center" valign="top" class="bgcolor6">
                                                                                                <table border="0" cellspacing="1" cellpadding="0" width="100%" align="center" class="allborder">
                                                                                                    <tr>
                                                                                                        <td align="left" class="gridheaderbg" valign="top">
                                                                                                            <asp:ImageButton ID="imgbtnDKeyContact" runat="server" AlternateText="Delete" border="0"
                                                                                                                CssClass="curpointer" hspace="0" ImageAlign="Middle" ImageUrl="~/Images/gicon04.png"
                                                                                                                ToolTip="Delete" vspace="0" OnClick="imgbtnDKeyContact_Click" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <%-- <tr id="ErrorMsg" style="display: block;">
                                                                                                        <td class="error pl10">
                                                                                                            mhihhih
                                                                                                        </td>
                                                                                                    </tr>--%>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <CustomGid:ExtendGrid ID="csKeyContactAttached" runat="server" GridAllowPaging="True"
                                                                                                                GridSelectedRowStyleCSS="BlueViolet" GridShowFooter="False" GridWidth="100%"
                                                                                                                ImageAddButtonEnabled="False" ImageAddButtonURL="/Images/gicon02.png" ImageAdditionalButton1URL="/Images/gicon02.png"
                                                                                                                ImageAdditionalButton2URL="/Images/ArrowRightSmall.png" ImageAdditionalButton3URL="/Images/gicon04.png"
                                                                                                                ImageAdditionalButton4URL="/Images/copy.png" ImageAdditionalButton5URL="/Images/gicon02.png"
                                                                                                                ImageAdditionalButton6URL="/Images/ArrowRightSmall.png" ImageAdditionalButton7URL="/Images/gicon04.png"
                                                                                                                ImageAdditionalButton8URL="/Images/copy.png" ImageAddtionalButton1Enabled="False"
                                                                                                                ImageAddtionalButton2Enabled="False" ImageAddtionalButton3Enabled="False" ImageAddtionalButton4Enabled="False"
                                                                                                                ImageAddtionalButton5Enabled="False" ImageAddtionalButton6Enabled="False" ImageAddtionalButton7Enabled="False"
                                                                                                                ImageAddtionalButton8Enabled="False" ImageCopyButtonEnabled="False" ImageCopyButtonURL="/Images/copy.png"
                                                                                                                ImageDeleteButtonEnabled="False" ImageDeleteButtonURL="/Images/gicon04.png" ImageEditButtonEnabled="False"
                                                                                                                ImageEditButtonURL="/Images/ArrowRightSmall.png" ImageFirstURL="/Images/LeftAllArrow.png"
                                                                                                                ImageLastURL="/Images/RightAllArrow.png" ImageNextURL="/Images/RightArrow.png"
                                                                                                                ImagePreviousURL="/Images/LeftArrow.png" PageNumber="1" ImageAddButtonToolTip="Add"
                                                                                                                ImageCopyButtonToolTip="Copy" ImageDeleteButtonToolTip="Delete" ImageEditButtonToolTip="Edit"
                                                                                                                SortOrder="ASC" OnGridCheckedChange="csKeyContactAttached_GridCheckedChange"
                                                                                                                GridRowIndex="0" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <asp:Panel ID="pnlKeyAttached" runat="server" Visible="false">
                                                                                                        <tr>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblRec2" runat="server" Text="No Records to Display!!!"></asp:Label>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </asp:Panel>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" valign="middle" class="pr10 greyfooter">
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                    </asp:TabPanel>
                                                </asp:TabContainer>
                                            </td>
                                        </tr>
                                    </table>

                                    <script type="text/javascript" language="javascript">
                                        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(LoadHandler);
                                    </script>

                                    <%--</ContentTemplate>
                            </asp:UpdatePanel>--%>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" class="pr10 greyfooter">
                        <asp:Button runat="server" ID="btnProjectSave" Text="Save" CssClass="btnstyle" OnClick="btnSave_Click" />
                        <asp:Button runat="server" ID="btnProjectCancel" Text="Cancel" CssClass="btnstyle"
                            OnClientClick="return ConfirmCancel();" OnClick="btnProjectCancel_Click" />
                        <asp:UpdatePanel ID="upsave" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="btnSaveProcess" runat="server" Text="" Style="display: none;" OnClick="btnSaveProcess_Click" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <%-- <tr>
            <td align="right" valign="middle" class="pr10 greyfooter" style="height: 40px;" colspan="3">
                <asp:Button ID="btncommitalltab" CssClass="btnstyle" runat="server" Text="Save"
                    OnClick="btncommitalltab_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnCancelalltab" runat="server" CssClass="btnstyle" 
                    Text="Cancel" onclick="btnCancelalltab_Click" />
            </td>
        </tr>--%>
                <tr>
                    <td>
                        <asp:HiddenField ID="hdnfldsPnlUseDfndfld" runat="server" />
                        <asp:HiddenField ID="hdnfldTntSpcInfo" runat="server" />
                        <asp:HiddenField ID="hdnfldSlsContractInfo" runat="server" />
                        <asp:HiddenField ID="hdnfldSalesNewContactInfo" runat="server" />
                        <asp:HiddenField ID="hdnfldTenantNewSpaceNote" runat="server" />
                        <asp:HiddenField ID="hdnfldTenantSort" runat="server" />
                        <asp:HiddenField ID="hdnVendor" runat="server" />
                        <asp:HiddenField ID="hdnRelschUpdate" runat="server" />
                        <asp:HiddenField ID="hdnAuditRpt" runat="server" />
                        <asp:HiddenField ID="hdnNonAccrualStatus" runat="server" />
                        <asp:HiddenField ID="hdnAprisalInfo" runat="server" />
                    </td>
                </tr>
            </table>
            <%--BorrowerInformation--%>
            <%--<asp:ModalPopupExtender ID="mdlBrwinfo" runat="server" TargetControlID="imgbtnBrwinfo"
                PopupControlID="pnlbrwinformation" BackgroundCssClass="modalBackground" CancelControlID="imgbtnClosepopup">--%>
            </asp:ModalPopupExtender>
            <%-- <asp:HoverMenuExtender ID="MouseHowerBrwInfo" runat="server" TargetControlID="lblBrwName" PopupControlID="pnlBrwInfo"
  PopupPosition="Right" >
    </asp:HoverMenuExtender>
 <asp:BalloonPopupExtender ID="BalloonPopupExtender1" runat="server" TargetControlID="lblBrwName2" BalloonPopupControlID="Panel1"
  Position="BottomRight" UseShadow="true"   DisplayOnMouseOver="true" BalloonSize="Large" BalloonStyle="Rectangle" >
    </asp:BalloonPopupExtender> --%>
            <%--BorrowerInformation--%>
            <%--customgrid--%>
            <%-- <asp:ModalPopupExtender ID="MdlPopUpTntSpcInfo" runat="server" BackgroundCssClass="modalBackground"
        TargetControlID="hdnfldTntSpcInfo" PopupControlID="PnlTntSpcInfo" CancelControlID="btnTntSpcInfoCancel">
    </asp:ModalPopupExtender>--%>
            <%--  <asp:ModalPopupExtender ID="MdlPopUseDfndfld" runat="server" BackgroundCssClass="modalBackground"
        TargetControlID="hdnfldsPnlUseDfndfld" PopupControlID="PnlUseDfndfld" CancelControlID="btnUsrDfnFlsCancel">
    </asp:ModalPopupExtender>
    <asp:ModalPopupExtender ID="MdlPopUpSlsContractInfo" runat="server" BackgroundCssClass="modalBackground"
        TargetControlID="hdnfldSlsContractInfo" PopupControlID="PnlSlsContractInfo" CancelControlID="btnSlsCOntractINfoCancel">
    </asp:ModalPopupExtender>
    <asp:ModalPopupExtender ID="MdlPopUpSalesNewContactInfo" runat="server" BackgroundCssClass="modalBackground"
        TargetControlID="hdnfldSalesNewContactInfo" PopupControlID="pnlSalesNewContactInfo"
        CancelControlID="btnNewSalesContractInfoCancel">
    </asp:ModalPopupExtender>
    <asp:ModalPopupExtender ID="MdlPopUpTenantNewSpaceNote" runat="server" BackgroundCssClass="modalBackground"
        TargetControlID="hdnfldTenantNewSpaceNote" PopupControlID="PnlTenantNewSpaceNote"
        CancelControlID="btnTntNewSpaceNoteCancel">
    </asp:ModalPopupExtender>
    <asp:ModalPopupExtender ID="MdlPopUpTenantSort" runat="server" BackgroundCssClass="modalBackground"
        TargetControlID="hdnfldTenantSort" PopupControlID="pnlTenantSort" CancelControlID="btnTntSortMdlPopUpCancel">
    </asp:ModalPopupExtender>
    <asp:ModalPopupExtender ID="MdlPopUpasprslfrmMdlpopup" runat="server" BackgroundCssClass="modalBackground"
        TargetControlID="hdnAprisalInfo" PopupControlID="pnlasprslfrmMdlpopup" CancelControlID="btnaprisalCancel">
    </asp:ModalPopupExtender>--%>
            <asp:ModalPopupExtender ID="mdlPopupVendor" runat="server" TargetControlID="hdnVendor"
                PopupControlID="pnlVendor" CancelControlID="btnCancelVendor" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:ModalPopupExtender ID="mdlpopupiframeuserdefined" runat="server" BackgroundCssClass="modalBackground"
                TargetControlID="hdnfldsPnlUseDfndfld" PopupControlID="PnlUseDfndfld" CancelControlID="btnUsrDfnFlsCancel">
            </asp:ModalPopupExtender>
            <asp:ModalPopupExtender ID="mdlPopupRelschUpdate" runat="server" TargetControlID="hdnRelschUpdate"
                PopupControlID="pnlRelschUpdate" CancelControlID="btnCancelRelschUpdate" BackgroundCssClass="modalBackground" />
            <asp:ModalPopupExtender ID="MdlPopUpNonAccrualStatus" runat="server" TargetControlID="hdnNonAccrualStatus"
                PopupControlID="pnlNonAccrualStatus" CancelControlID="btnNonAccrlStatusNo" BackgroundCssClass="modalBackground" />
            <asp:ModalPopupExtender ID="MdlPopUpAuditRpt" runat="server" TargetControlID="hdnAuditRpt"
                PopupControlID="pnlAuditRpt" CancelControlID="btnAdtRptNo" BackgroundCssClass="modalBackground" />
            <asp:Panel ID="PnlUseDfndfld" runat="server" CssClass="mpLarge" Width="65%" Style="display: none">
                <table border="0" cellspacing="" cellpadding="" align="left" width="100%">
                    <tr>
                        <td align="left" valign="middle">
                            <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
                                <!-- Table Starts Here -->
                                <tr>
                                    <td align="left" valign="top" width="100%">
                                        <asp:TabContainer ID="tagContainerUsrDfndFld" runat="server" CssClass="Tab" ActiveTabIndex="0">
                                            <asp:TabPanel ID="tabAlphaNumFlds" runat="server">
                                                <HeaderTemplate>
                                                    Alpha-Numeric Fields
                                                </HeaderTemplate>
                                                <ContentTemplate>
                                                    <table width="100%" class="allgborder" cellpadding="1" cellspacing="0" border="0">
                                                        <tr>
                                                            <td align="left" valign="top">
                                                                <table width="100%" align="left" cellpadding="0" cellspacing="0" border="0">
                                                                    <tr>
                                                                        <td width="*" align="left" valign="middle" class="pl10 bbggridhead tablehdfont dotbtborder">
                                                                            Alpha-Numeric Fields
                                                                        </td>
                                                                        <td width="110px" height="35px" align="right" valign="top" class="dotbtborder">
                                                                            <img src="/Images/yobg.png" border="0" vspace="0" hspace="0" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" valign="top">
                                                                <table width="100%" align="center" cellpadding="5" cellspacing="0" border="0">
                                                                    <tr>
                                                                        <td align="left" valign="top" class="bgcolor6">
                                                                            <table width="100%" border="0" align="center" cellpadding="5" cellspacing="0">
                                                                                <tr>
                                                                                    <td width="24%" align="left" valign="middle" class="blucolor blackbfont">
                                                                                        Origination File Number
                                                                                    </td>
                                                                                    <td width="1%" align="left" valign="middle">
                                                                                    </td>
                                                                                    <td align="left" valign="middle">
                                                                                        <asp:TextBox runat="server" ID="text5303" CssClass="txtbox" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                        Acquistion File Number
                                                                                    </td>
                                                                                    <td align="left" valign="middle">
                                                                                    </td>
                                                                                    <td align="left" valign="middle">
                                                                                        <asp:TextBox runat="server" ID="text5308" CssClass="txtbox" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                        Land Note Number
                                                                                    </td>
                                                                                    <td align="left" valign="middle">
                                                                                    </td>
                                                                                    <td align="left" valign="middle">
                                                                                        <asp:TextBox runat="server" ID="text5313" CssClass="txtbox" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </asp:TabPanel>
                                            <asp:TabPanel ID="tabAmntFlds" runat="server">
                                                <HeaderTemplate>
                                                    Amount Fields
                                                </HeaderTemplate>
                                                <ContentTemplate>
                                                    <table width="100%" class="allgborder" cellpadding="1" cellspacing="0" border="0">
                                                        <tr>
                                                            <td align="left" valign="top">
                                                                <table width="100%" align="left" cellpadding="0" cellspacing="0" border="0">
                                                                    <tr>
                                                                        <td width="*" align="left" valign="middle" class="pl10 bbggridhead tablehdfont dotbtborder">
                                                                            Amount Fields
                                                                        </td>
                                                                        <td width="110px" height="35px" align="right" valign="top" class="dotbtborder">
                                                                            <img src="/Images/yobg.png" border="0" vspace="0" hspace="0" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" valign="top">
                                                                <table width="100%" align="center" cellpadding="5" cellspacing="0" border="0">
                                                                    <tr>
                                                                        <td align="left" valign="top" class="bgcolor6">
                                                                            <table width="100%" border="0" align="center" cellpadding="5" cellspacing="0">
                                                                                <tr>
                                                                                    <td width="24%" align="left" valign="middle" class="blucolor blackbfont">
                                                                                        Total Closing Costs
                                                                                    </td>
                                                                                    <td width="1%" align="left" valign="middle">
                                                                                    </td>
                                                                                    <td align="left" valign="middle">
                                                                                        <asp:TextBox runat="server" ID="text5344" CssClass="txtbox" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                        Invest Reserve Requirement
                                                                                    </td>
                                                                                    <td align="left" valign="middle">
                                                                                    </td>
                                                                                    <td align="left" valign="middle">
                                                                                        <asp:TextBox runat="server" ID="text5350" CssClass="txtbox" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" valign="top" class="pr10 greyfooter">
                                                                            <asp:Button runat="server" ID="btn5356" CssClass="btnstyle" />
                                                                            &nbsp;&nbsp;<a href="#" class="close"><asp:Button runat="server" ID="btn5357" CssClass="btnstyle" /></a>
                                                                            </a>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </asp:TabPanel>
                                            <asp:TabPanel ID="tabDateFlds" runat="server">
                                                <HeaderTemplate>
                                                    Date Fields
                                                </HeaderTemplate>
                                                <ContentTemplate>
                                                    <table width="100%" class="allgborder" cellpadding="1" cellspacing="0" border="0">
                                                        <tr>
                                                            <td align="left" valign="top">
                                                                <table width="100%" align="left" cellpadding="0" cellspacing="0" border="0">
                                                                    <tr>
                                                                        <td width="*" align="left" valign="middle" class="pl10 bbggridhead tablehdfont dotbtborder">
                                                                            Date Fields
                                                                        </td>
                                                                        <td width="110px" height="35px" align="right" valign="top" class="dotbtborder">
                                                                            <img src="/Images/yobg.png" border="0" vspace="0" hspace="0" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" valign="top">
                                                                <table width="100%" align="center" cellpadding="5" cellspacing="0" border="0">
                                                                    <tr>
                                                                        <td align="left" valign="top" class="bgcolor6">
                                                                            <table align="center" border="0" cellpadding="5" cellspacing="0" width="100%">
                                                                                <tr>
                                                                                    <td align="left" class="blucolor blackbfont" valign="middle" width="24%">
                                                                                        Servicing Released
                                                                                    </td>
                                                                                    <td align="left" valign="middle" width="1%">
                                                                                    </td>
                                                                                    <td align="left" valign="middle">
                                                                                        <asp:TextBox ID="txtServicingReleased" runat="server" CssClass="txtbox"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left" class="blucolor blackbfont" valign="middle">
                                                                                        File Audit Date 1
                                                                                    </td>
                                                                                    <td align="left" valign="middle">
                                                                                    </td>
                                                                                    <td align="left" valign="middle">
                                                                                        <asp:TextBox ID="txtFileAuditDate1" runat="server" CssClass="txtbox" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left" class="blucolor blackbfont" valign="middle">
                                                                                        File Audit Date2
                                                                                    </td>
                                                                                    <td align="left" valign="middle">
                                                                                    </td>
                                                                                    <td align="left" valign="middle">
                                                                                        <asp:TextBox ID="txtFIleAuditDate2" runat="server" CssClass="txtbox" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </asp:TabPanel>
                                        </asp:TabContainer>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top" class="pr10 greyfooter">
                                        <asp:Button runat="server" ID="btnUsrDfnFlsOk" Text="OK" CssClass="btnstyle" />
                                        &nbsp;&nbsp;
                                        <asp:Button runat="server" ID="btnUsrDfnFlsCancel" Text="Cancel" CssClass="btnstyle" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlVendor" runat="server" CssClass="mpXL" Style="display: none">
                <%--  <asp:UpdatePanel ID="upVendor" runat="server">
            <ContentTemplate>--%>
                <table width="100%" align="center" cellpadding="1" cellspacing="0" border="0">
                    <tr>
                        <td width="100%" height="24px" align="left" valign="middle" class="popupheader">
                            Vendor-Budget Relationships
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" height="24px" class="bgcolor3">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td valign="top">
                                        <table width="100%" border="0" cellspacing="5" cellpadding="0">
                                            <tr>
                                                <td align="left" class="blucolor blackbfont">
                                                    Note Number
                                                </td>
                                                <td align="left" colspan="2">
                                                    &nbsp;&nbsp;&nbsp;<asp:Label ID="lblNoteNumber" runat="server" Text="411"></asp:Label></td>
                                                <td class="blucolor blackbfont">
                                                    Unit
                                                </td>
                                                <td colspan="2">
                                                    <asp:Label ID="lblUnit" runat="server" Text="001"></asp:Label>
                                                </td>
                                                <td colspan="2">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" class="blucolor ">
                                                    Vendor Number
                                                </td>
                                                <td align="left" class="blackbfont pl10">
                                                    <asp:TextBox ID="txtVNumber" runat="server" CssClass="txtboxmed"></asp:TextBox>
                                                </td>
                                                <td align="left" width="3%">
                                                    <asp:ImageButton ID="imgbtnSearchVendor" runat="server" ImageUrl="../../Images/gicon01.png"
                                                        vspace="0" hspace="0" border="0" alt="Search Vendor" ImageAlign="Middle" title="Vendor"
                                                        class="curpointer" OnClientClick="javascript:SelectVendor();" ToolTip="Search Vendor" />
                                                    <%-- <img src="../../Images/Vendor.png" alt="Vendor" align="middle" vspace="0" hspace="0" border="0"
                                                        title="Select Vendor" class="curpointer" onclick="javascript:SelectVendor();" />--%>
                                                </td>
                                                <td class="blucolor blackbfont">
                                                    Name
                                                </td>
                                                <td colspan="4">
                                                    <asp:Label ID="lblVName" runat="server" Text="All State" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="12%" class="blucolor blackbfont">
                                                    Address
                                                </td>
                                                <td width="16%">
                                                    <asp:Label ID="lblVAddress1" runat="server" Text="Add1" />
                                                </td>
                                                <td width="12%" class="blucolor blackbfont">
                                                    City
                                                </td>
                                                <td width="12%">
                                                    <asp:Label ID="lblVCity" runat="server" Text="Florida" />
                                                </td>
                                                <td width="12%" class="blucolor blackbfont">
                                                    State
                                                </td>
                                                <td width="12%">
                                                    <asp:Label ID="lblVState" runat="server" Text="TX" />
                                                </td>
                                                <td width="12%" class="blucolor blackbfont">
                                                    Zip
                                                </td>
                                                <td width="12%">
                                                    <asp:Label ID="lblVZip" runat="server" Text="" />
                                                </td>
                                            </tr>
                                            <%--<tr>
                                                <td class="blackbfont">
                                                    Address2
                                                </td>
                                                <td colspan="7">
                                                    <asp:Label ID="lblVAddress2" runat="server" Text="Add2" />
                                                </td>
                                            </tr>--%>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <CustomGid:ExtendGrid ID="egBudget" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table width="100%" border="0" cellspacing="8" cellpadding="0">
                                            <tr>
                                                <td>
                                                    <asp:RadioButtonList ID="rbVendorRel" runat="server">
                                                        <asp:ListItem Text="Relate Vendor to the Selected Budget Line Items as Primary Payee"
                                                            Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Relate Vendor to the Selected Budget Line items as secondary Payee"
                                                            Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Un-relate Vendor to the selected Budget Line Items" Value="2"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td align="right" valign="middle">
                                                    <asp:Button runat="server" ID="btnApply" Text="Apply" CssClass="btnstyle" />
                                                    &nbsp;&nbsp;<asp:Button runat="server" ID="btnCancelVendor" CssClass="btnstyle" Text="Cancel" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <%--</ContentTemplate>
        </asp:UpdatePanel>--%></asp:Panel>
            <asp:Panel ID="pnlRelschUpdate" runat="server" CssClass="mpXL" Style="display: none">
                <div id="Div1" runat="server" style="max-height: 500px; overflow: auto;">
                    <table width="100%" align="center" cellpadding="1" cellspacing="0" border="0">
                        <tr>
                            <td align="left" valign="middle" height="24px" class="popupheader">
                                Edit Release Schedule
                            </td>
                        </tr>
                        <tr>
                            <td width="100%" align="left" valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="48%" valign="top">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td valign="top">
                                                        <table align="left" width="100%" cellpadding="0" cellspacing="5px" border="0" class="allborder">
                                                            <tr class="bgcolor4">
                                                                <td height="24px" colspan="3" align="left" valign="middle" class="pl10 blackbfont">
                                                                    Release Information
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="171" height="24px" align="left" valign="middle" class="blucolor ">
                                                                    Description
                                                                </td>
                                                                <td width="6" align="left" valign="middle">
                                                                    &nbsp;&nbsp;
                                                                </td>
                                                                <td width="151" align="left" valign="middle" class="pl10">
                                                                    <asp:TextBox runat="server" ID="txtDescription" CssClass="txtbox" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" valign="middle" height="24px" class="blucolor ">
                                                                    Status
                                                                </td>
                                                                <td align="left" valign="middle">
                                                                    &nbsp;&nbsp;
                                                                </td>
                                                                <td align="left" valign="middle" class="pl10">
                                                                    <asp:CheckBox runat="server" ID="chkStatus" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" valign="middle" height="24px" class="blucolor ">
                                                                    Minimum Amount
                                                                </td>
                                                                <td align="left" valign="middle">
                                                                    &nbsp;&nbsp;
                                                                </td>
                                                                <td align="left" valign="middle" class="pl10">
                                                                    <asp:TextBox runat="server" ID="txtMinimumAmount" CssClass="txtbox" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" valign="middle" height="24px" class="blucolor ">
                                                                    Release Date
                                                                </td>
                                                                <td align="left" valign="middle">
                                                                    &nbsp;&nbsp;
                                                                </td>
                                                                <td align="left" valign="middle" class="pl10">
                                                                    <asp:TextBox runat="server" ID="txtReleaseDate" CssClass="txtbox" Text=" " />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" valign="middle" height="24px" class="blucolor ">
                                                                    Current Release
                                                                </td>
                                                                <td align="left" valign="middle">
                                                                    &nbsp;&nbsp;
                                                                </td>
                                                                <td align="left" valign="middle" class="pl10">
                                                                    <asp:TextBox runat="server" ID="txtCurrentRelease" CssClass="txtbox" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" valign="middle" height="24px" class="blucolor ">
                                                                    Last Release
                                                                </td>
                                                                <td align="left" valign="middle">
                                                                    &nbsp;
                                                                </td>
                                                                <td align="left" valign="middle" class="pl10">
                                                                    <asp:TextBox runat="server" ID="txtLastRelease" CssClass="txtbox" Text=" " />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" valign="middle" height="24px" class="blucolor ">
                                                                    Paydown Flag
                                                                </td>
                                                                <td align="left" valign="middle">
                                                                    &nbsp;
                                                                </td>
                                                                <td align="left" valign="middle" class="pl10">
                                                                    <asp:CheckBox runat="server" ID="chkPaydownFlag" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td height="24px" colspan="3" align="left" valign="middle">
                                                                    <table width="100%" border="0" align="left" cellpadding="0" cellspacing="5px" class="allborder">
                                                                        <tr class="bgcolor4">
                                                                            <td height="24px" align="left" valign="middle" colspan="3" class="pl10 blackbfont">
                                                                                Sales Information
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" valign="middle" height="24px" class="blucolor ">
                                                                                Amount
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                &nbsp;&nbsp;
                                                                            </td>
                                                                            <td align="left" valign="middle" class="pl10">
                                                                                <asp:TextBox runat="server" ID="txtAmtSales" CssClass="txtbox" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" valign="middle" height="24px" class="blucolor ">
                                                                                Square Feet
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                &nbsp;&nbsp;
                                                                            </td>
                                                                            <td align="left" valign="middle" class="pl10">
                                                                                <asp:TextBox runat="server" ID="txtSqFtSales" CssClass="txtbox" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" valign="middle" height="24px" class="blucolor ">
                                                                                Price/Sq feet
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                &nbsp;&nbsp;
                                                                            </td>
                                                                            <td align="left" valign="middle" class="pl10">
                                                                                <asp:TextBox runat="server" ID="txtPricePerSqFtSales" CssClass="txtbox" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="3%">
                                            &nbsp;
                                        </td>
                                        <td width="49%" valign="top">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td>
                                                        <table width="100%" border="0" align="left" cellpadding="0" cellspacing="5px" class="allborder">
                                                            <tr class="bgcolor4">
                                                                <td height="24px" colspan="3" align="left" valign="middle" class="pl10 blackbfont">
                                                                    Appraisal Information
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" valign="middle" height="24px" class="blucolor ">
                                                                    Amount
                                                                </td>
                                                                <td align="left" valign="middle">
                                                                    &nbsp;&nbsp;
                                                                </td>
                                                                <td align="left" valign="middle" class="pl10">
                                                                    <asp:TextBox runat="server" ID="txtAmtAppraisal" CssClass="txtbox" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" valign="middle" height="24px" class="blucolor ">
                                                                    Square Feet
                                                                </td>
                                                                <td align="left" valign="middle">
                                                                    &nbsp;&nbsp;
                                                                </td>
                                                                <td align="left" valign="middle" class="pl10">
                                                                    <asp:TextBox runat="server" ID="txtSqFtAppraisal" CssClass="txtbox" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" valign="middle" height="24px" class="blucolor ">
                                                                    Price/Sq feet
                                                                </td>
                                                                <td align="left" valign="middle">
                                                                    &nbsp;&nbsp;
                                                                </td>
                                                                <td align="left" valign="middle" class="pl10">
                                                                    <asp:TextBox runat="server" ID="txtPricePerSqFtAppraisal" CssClass="txtbox" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" valign="middle" height="24px" class="blucolor ">
                                                                    Discounted Amount
                                                                </td>
                                                                <td align="left" valign="middle">
                                                                    &nbsp;&nbsp;
                                                                </td>
                                                                <td align="left" valign="middle" class="pl10">
                                                                    <asp:TextBox runat="server" ID="txtDiscountedAmt" CssClass="txtbox" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" valign="middle" height="24px" class=" ">
                                                                    Type
                                                                </td>
                                                                <td align="left" valign="middle">
                                                                    &nbsp;&nbsp;
                                                                </td>
                                                                <td align="left" valign="middle" class="pl10">
                                                                    <asp:CheckBox runat="server" ID="chkType" name="checkbox3" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" valign="middle" height="24px" class="blucolor ">
                                                                    Last Appraisal
                                                                </td>
                                                                <td align="left" valign="middle">
                                                                    &nbsp;&nbsp;
                                                                </td>
                                                                <td align="left" valign="middle" class="pl10">
                                                                    <asp:TextBox runat="server" ID="txtLastAppDate" CssClass="txtbox" Text=" " />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" valign="middle" height="24px" class="blucolor ">
                                                                    Pct Cmp
                                                                </td>
                                                                <td align="left" valign="middle">
                                                                    &nbsp;&nbsp;
                                                                </td>
                                                                <td align="left" valign="middle" class="pl10">
                                                                    <asp:TextBox runat="server" ID="txtPctCmp" CssClass="txtbox" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" valign="middle" height="24px" class="blucolor ">
                                                                    Last Inspection
                                                                </td>
                                                                <td align="left" valign="middle">
                                                                    &nbsp;
                                                                </td>
                                                                <td align="left" valign="middle" class="pl10">
                                                                    <asp:TextBox runat="server" ID="txtLastInspDate" CssClass="txtbox" Text=" " />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <table border="0" cellspacing="1" cellpadding="0" width="100%" align="center" class="allborder">
                                                <tr>
                                                    <td align="left" valign="middle">
                                                        <table border="0" cellspacing="1" cellpadding="5" width="100%" align="center" class="gridheaderbg">
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr class="bgcolor4">
                                                    <td height="24px" align="left" valign="middle">
                                                        <span class="pl10 blackbfont">Payoff Rules</span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td height="24px" align="left" valign="middle" class="pl10">
                                                        <CustomGid:ExtendGrid ID="egPayoffRules" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="middle" height="30px" class="pr10 tborder">
                                <asp:Button runat="server" ID="btnSaveRelschUpdate" CssClass="btnstyle" Text="Save" />
                                &nbsp;&nbsp;<asp:Button runat="server" ID="btnCancelRelschUpdate" CssClass="btnstyle"
                                    Text="Cancel" OnClick="btnCancelRelschUpdate_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlNonAccrualStatus" runat="server" CssClass="mpSmall" Width="45%"
                Style="display: none">
                <table width="100%" align="center" cellpadding="1" cellspacing="0" border="0">
                    <tr>
                        <td align="left" valign="middle" height="24px" class="popupheader">
                            Apply Non-Accrual Status
                        </td>
                    </tr>
                    <tr>
                        <td width="100%" align="left" valign="top">
                            <table border="0" cellspacing="0" cellpadding="0" width="100%" class="allborder">
                                <tr>
                                    <td align="left" valign="top" class="gridheaderbg">
                                        <table border="0" cellspacing="0" cellpadding="5" align="left" width="100%">
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" valign="top">
                                        <table border="0" cellspacing="1" cellpadding="0" width="100%" align="center">
                                            <tr>
                                                <td width="100%" align="left" valign="middle">
                                                    <table width="100%" border="0" cellspacing="8" cellpadding="0">
                                                        <tr>
                                                            <td width="14%" align="left" class="blucolor blackbfont">
                                                                Effective Date
                                                            </td>
                                                            <td width="19%">
                                                                <asp:TextBox runat="server" ID="txtNonAccrlStatusEffctDate" CssClass="txtbox" Text=" YY" />
                                                            </td>
                                                            <td width="67%">
                                                                Loan currently has no 'Open' Interest Receivables
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3" align="left">
                                                                Day End Processing will unbook all past due billed interest and all unbilled accrued
                                                                interest.<br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3" align="left">
                                                                <label for="checkbox">
                                                                    All future billed interest and daily accrual will be carried as 'unbooked'.</label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                &nbsp;
                                                            </td>
                                                            <td colspan="2">
                                                                Do you want to continue with implementing the Non-Accrual Status?
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" valign="middle" class="tborder">
                                        <table border="0" cellspacing="1" cellpadding="5" align="center" width="100%">
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="middle" height="30px" class="pr10 tborder">
                            &nbsp;&nbsp;
                            <asp:Button runat="server" ID="btnNonAccrlStatusYes" Text="Yes" CssClass="btnstyle" />&nbsp;&nbsp;
                            <asp:Button runat="server" ID="btnNonAccrlStatusNo" Text="No" CssClass="btnstyle" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlAuditRpt" runat="server" CssClass="mpSmall" Width="65%" Style="display: none">
                <table width="100%" align="center" cellpadding="1" cellspacing="0" border="0">
                    <tr>
                        <td align="left" valign="middle" height="24px" class="popupheader">
                            Audit Report
                        </td>
                    </tr>
                    <tr>
                        <td width="100%" align="left" valign="top">
                            <table border="0" cellspacing="0" cellpadding="0" width="100%" class="allborder">
                                <tr>
                                    <td align="left" valign="top" class="gridheaderbg">
                                        <table border="0" cellspacing="0" cellpadding="5" align="left" width="100%">
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" valign="top">
                                        <table border="0" cellspacing="1" cellpadding="0" width="100%" align="center">
                                            <tr>
                                                <td width="100%" align="left" valign="middle">
                                                    <table width="100%" border="0" cellspacing="8" cellpadding="0">
                                                        <tr>
                                                            <td align="left">
                                                                <label for="textarea">
                                                                </label>
                                                                <textarea name="textarea" rows="12" id="textarea" style="width: 95%">Note 121FGF14 001 contains the following Potential Issues


                        Legal description is missing

                        Budget Estimated $0.00 - Allocated$7,000.00 = ($7,000.00)

                        Interest Profile or Interest Rate Missing

                        Interest Profile or Billing Cycle Missing</textarea>
                                                                <br />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" valign="middle" class="tborder">
                                        <table border="0" cellspacing="1" cellpadding="5" align="center" width="100%">
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="middle" height="30px" class="pr10 tborder">
                            &nbsp;&nbsp;
                            <asp:Button runat="server" ID="btnAdtRptYes" Text="Yes" CssClass="btnstyle" />&nbsp;&nbsp;
                            <asp:Button runat="server" ID="btnAdtRptNo" Text="No" CssClass="btnstyle" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <%--  <asp:UpdatePanel ID="UpLIProfile" runat="server" UpdateMode="Conditional">
        <ContentTemplate>--%>
            <asp:HiddenField ID="hdnHide1" runat="server" />
            <asp:ModalPopupExtender ID="popupLIProfile" runat="server" PopupControlID="pnlLIProfile"
                TargetControlID="hdnHide1" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlLIProfile" runat="server" CssClass="mpXL" Style="display: none">
                <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <!-- Table Starts Here -->
                    <tr>
                        <td align="left" valign="middle" class="blackbfont" height="27px">
                            New: Line of Credit Interest Profile -
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" width="100%">
                            <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="2" 
                                CssClass="Tab">
                                <asp:TabPanel ID="TabPanel2" runat="server">
                                    <HeaderTemplate>
                                        General Rate Options
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <div id="Div4">
                                            <table width="100%" class="allgborder" cellpadding="1" cellspacing="0" border="0">
                                                <tr>
                                                    <td align="left" valign="top">
                                                        <table width="100%" align="left" cellpadding="0" cellspacing="0" border="0">
                                                            <tr>
                                                                <td width="*" align="left" valign="middle" class="pl10 bbggridhead tablehdfont dotbtborder">
                                                                    General Rate Options
                                                                </td>
                                                                <td width="110px" height="35px" align="right" valign="top" class="dotbtborder">
                                                                    <img src="../../Images/yobg.png" border="0" vspace="0" hspace="0" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" valign="top">
                                                        <table width="100%" align="center" cellpadding="5" cellspacing="0" border="0">
                                                            <tr>
                                                                <td align="left" valign="top" class="bgcolor6">
                                                                    <table width="100%" border="0" align="center" cellpadding="5" cellspacing="0">
                                                                        <tr>
                                                                            <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                Rate Type
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                &#160;&nbsp;
                                                                            </td>
                                                                            <td align="left" valign="middle" colspan="4">
                                                                                <select name="slct" id="Select4" onchange="rohan(this.value)" class="ddlistbox">
                                                                                    <option>Select</option>
                                                                                    <option value="1">Fixed Rate</option>
                                                                                    <option value="2">Variable Rate - VRM</option>
                                                                                    <option value="3">Adjustable Rate - ARM</option>
                                                                                </select>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="20%" align="left" valign="middle" class="blucolor blackbfont">
                                                                                Number of Days - Yearly
                                                                            </td>
                                                                            <td width="1%" align="left" valign="middle">
                                                                                &#160;&nbsp;
                                                                            </td>
                                                                            <td width="25%" align="left" valign="middle">
                                                                                <select name="category" id="Select5" class="ddlistbox">
                                                                                    <option selected="selected">Select</option>
                                                                                    <option>360</option>
                                                                                    <option>365 365365</option>
                                                                                    <option>366 366366</option>
                                                                                </select>
                                                                            </td>
                                                                            <td width="20%" align="left" valign="middle" class="blucolor blackbfont">
                                                                                Actual Days - Monthly
                                                                            </td>
                                                                            <td width="1%" align="left" valign="middle">
                                                                                &#160;&nbsp;
                                                                            </td>
                                                                            <td width="*" align="left" valign="middle">
                                                                                <select name="category2" id="Select6" class="ddlistbox">
                                                                                    <option selected="selected">Select</option>
                                                                                    <option>Actual</option>
                                                                                    <option>30 Days</option>
                                                                                </select>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" valign="middle" class="pl10 blackbfont" colspan="6">
                                                                                Rates
                                                                            </td>
                                                                        </tr>
                                                                        <tr id="Tr1">
                                                                            <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                Fixed Rate
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                &#160;&nbsp;
                                                                            </td>
                                                                            <td align="left" valign="middle" colspan="4">
                                                                                <select name="category" id="Select7" class="ddlistbox">
                                                                                    <option selected="selected">Select</option>
                                                                                    <option>000-Prime</option>
                                                                                    <option>001-List</option>
                                                                                </select><a href="#" class="close"><input type="button" class="btnstyle" value="View" /></a>
                                                                            </td>
                                                                        </tr>
                                                                        <tr id="Tr2">
                                                                            <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                Fixed Rate
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                &#160;&nbsp;
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                <input class="txtbox" type="text" value="" />
                                                                            </td>
                                                                            <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                Default Rate
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                &#160;&nbsp;
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                <input class="txtbox" type="text" value="" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                Effective Date
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                &#160;&nbsp;
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                <input class="txtbox" type="text" value="DD/MM/YYYY" />
                                                                            </td>
                                                                            <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                Maturity Date
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                &#160;&nbsp;
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                <input class="txtbox" type="text" value="DD/MM/YYYY" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                Review Date
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                &#160;&#160;
                                                                            </td>
                                                                            <td align="left" valign="middle" colspan="4">
                                                                                <input type="text" class="txtbox" value="DD/MM/YYYY" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" valign="middle" class="pl10 greyfooter">
                                                                    <asp:Button ID="Button1" runat="server" Text="OK" CssClass="btnstyle" />&nbsp;<asp:Button
                                                                        ID="btnCancelLiprofile" runat="server" CssClass="btnstyle" Text="Cancel" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </ContentTemplate>
                                </asp:TabPanel>
                                <asp:TabPanel ID="TabPanel3" runat="server">
                                    <HeaderTemplate>
                                        Additional Rate Information
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <div id="Div2">
                                            <table width="100%" class="allgborder" cellpadding="1" cellspacing="0" border="0">
                                                <tr>
                                                    <td align="left" valign="top">
                                                        <table width="100%" align="left" cellpadding="0" cellspacing="0" border="0">
                                                            <tr>
                                                                <td width="*" align="left" valign="middle" class="pl10 bbggridhead tablehdfont dotbtborder">
                                                                    Additional Rate Information
                                                                </td>
                                                                <td width="110px" height="35px" align="right" valign="top" class="dotbtborder">
                                                                    <img src="../../Images/yobg.png" border="0" vspace="0" hspace="0" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" valign="top">
                                                        <table width="100%" align="center" cellpadding="5" cellspacing="0" border="0">
                                                            <tr>
                                                                <td align="left" valign="top" class="bgcolor6">
                                                                    <table width="100%" border="0" align="center" cellpadding="5" cellspacing="0">
                                                                        <tr>
                                                                            <td align="left" valign="middle" class="pl10 blackbfont" colspan="6">
                                                                                Variable Rate Information
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="20%" align="left" valign="middle" class="blucolor blackbfont">
                                                                                Margin
                                                                            </td>
                                                                            <td width="1%" align="left" valign="middle">
                                                                                &#160;&nbsp;
                                                                            </td>
                                                                            <td width="25%" align="left" valign="middle">
                                                                                <input type="text" class="txtbox" value="" />
                                                                            </td>
                                                                            <td width="20%" align="left" valign="middle" class="blucolor blackbfont">
                                                                                Dft Margin
                                                                            </td>
                                                                            <td width="1%" align="left" valign="middle">
                                                                                &#160;&nbsp;
                                                                            </td>
                                                                            <td width="*" align="left" valign="middle">
                                                                                <input type="text" class="txtbox" value="" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                Life Cap
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                &#160;&nbsp;
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                <input type="text" class="txtbox" value="" disabled="disabled" />
                                                                            </td>
                                                                            <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                Floor
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                &#160;&nbsp;
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                <input type="text" class="txtbox" value="" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                Ceiling
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                &#160;&nbsp;
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                <input type="text" class="txtbox" value="" />
                                                                            </td>
                                                                            <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                Adj. Cap
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                &#160;&nbsp;
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                <input type="text" class="txtbox" value="" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                Rounding
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                &#160;&nbsp;
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                <input type="text" class="txtbox" value="" />
                                                                                <asp:Button ID="btnRoundPnl" runat="server" Text=".." CssClass="btnstyle" />
                                                                            </td>
                                                                            <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                Interest Rate
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                &#160;&nbsp;
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                <input type="text" class="txtbox" value="" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" valign="middle" class="pl10 blackbfont" colspan="6">
                                                                                Adjustable Rate Information
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" valign="middle" class="blucolor pl10">
                                                                                Prime Eff. Date
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                &#160;&nbsp;
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                <input type="text" class="txtbox" value="DD/MM/YYYY" />
                                                                            </td>
                                                                            <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                Next Eff. Date
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                &#160;&nbsp;
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                <input type="text" class="txtbox" value="DD/MM/YYYY" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                Freq. Factor
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                &#160;&nbsp;
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                <input type="text" class="txtbox" value="" />
                                                                            </td>
                                                                            <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                Freq. Factor
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                &#160;&nbsp;
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                <input type="text" class="txtbox" value="" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                Method
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                &#160;&nbsp;
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                <input type="text" class="txtbox" value="" />
                                                                            </td>
                                                                            <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                Method
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                &#160;&nbsp;
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                <input type="text" class="txtbox" value="" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                Adj.
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                &#160;&nbsp;
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                <input type="text" class="txtbox" value="" />
                                                                            </td>
                                                                            <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                Adj.
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                &#160;&nbsp;
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                <input type="text" class="txtbox" value="" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                Days
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                &#160;&nbsp;
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                <input type="text" class="txtbox" value="" />
                                                                            </td>
                                                                            <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                Days
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                &#160;&nbsp;
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                <input type="text" class="txtbox" value="" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" valign="middle" class="pl10 greyfooter">
                                                                    <a href="#" class="close">
                                                                        <input type="button" class="btnstyle" value="OK" /></a>
                                                                    <asp:Button ID="btnLIP2" runat="server" Text="Cancel" CssClass="btnstyle" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </ContentTemplate>
                                </asp:TabPanel>
                                <asp:TabPanel ID="TabPanel4" runat="server">
                                    <HeaderTemplate>
                                        Billing Options
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <div id="Div3">
                                            <table width="100%" class="allgborder" cellpadding="1" cellspacing="0" border="0">
                                                <tr>
                                                    <td align="left" valign="top">
                                                        <table width="100%" align="left" cellpadding="0" cellspacing="0" border="0">
                                                            <tr>
                                                                <td width="*" align="left" valign="middle" class="pl10 bbggridhead tablehdfont dotbtborder">
                                                                    Billing Options
                                                                </td>
                                                                <td width="110px" height="35px" align="right" valign="top" class="dotbtborder">
                                                                    <img src="../../Images/yobg.png" border="0" vspace="0" hspace="0" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" valign="top">
                                                        <table width="100%" align="center" cellpadding="5" cellspacing="0" border="0">
                                                            <tr>
                                                                <td align="left" valign="top" class="bgcolor6">
                                                                    <table width="100%" border="0" align="center" cellpadding="5" cellspacing="0">
                                                                        <tr>
                                                                            <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                Cycle
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                &#160;&nbsp;
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                <select name="category" id="Select8" class="ddlistbox">
                                                                                    <option selected="selected">Select</option>
                                                                                    <option>360</option>
                                                                                    <option>365</option>
                                                                                    <option>366</option>
                                                                                </select>
                                                                            </td>
                                                                            <td align="left" valign="middle" class=" blackbfont" colspan="3">
                                                                                Late Fees
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="20%" align="left" valign="middle" class="blucolor blackbfont">
                                                                                Day
                                                                            </td>
                                                                            <td width="1%" align="left" valign="middle">
                                                                                &#160;&nbsp;
                                                                            </td>
                                                                            <td width="25%" align="left" valign="middle">
                                                                                <input type="text" class="txtbox" value="" />
                                                                            </td>
                                                                            <td width="20%" align="left" valign="middle" class="blucolor blackbfont">
                                                                                Days
                                                                            </td>
                                                                            <td width="1%" align="left" valign="middle">
                                                                                &#160;&nbsp;
                                                                            </td>
                                                                            <td width="*" align="left" valign="middle">
                                                                                <input type="text" class="txtbox" value="" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                Next
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                &#160;&nbsp;
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                <input type="text" class="txtbox" value="DD/MM/YYYY" />
                                                                            </td>
                                                                            <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                Amount
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                &#160;&nbsp;
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                <input type="text" class="txtbox" value="" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                Fixed P&amp;I Amount
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                &#160;&nbsp;
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                <input type="text" class="txtbox" value="" />
                                                                            </td>
                                                                            <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                Percentage
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                &#160;&nbsp;
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                <input type="text" class="txtbox" value="" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                Next P&amp;I Date
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                &#160;&nbsp;
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                <input type="text" class="txtbox" value="DD/MM/YYYY" />
                                                                            </td>
                                                                            <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                Min. % Amount
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                &#160;&nbsp;
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                <input type="text" class="txtbox" value="" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" valign="middle" class="pl10">
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                            </td>
                                                                            <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                Max. % Amount
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                &#160;&nbsp;
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                <input type="text" class="txtbox" value="" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                Bill Method
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                &#160;&nbsp;
                                                                            </td>
                                                                            <td align="left" valign="middle" colspan="4">
                                                                                <select name="category2" id="Select9" class="ddlistbox">
                                                                                    <option selected="selected">Select</option>
                                                                                    <option>Bill</option>
                                                                                    <option>Auto-Detect</option>
                                                                                </select>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                Auto-Detect
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                &#160;&nbsp;
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                <select name="category3" id="Select10" class="ddlistbox">
                                                                                    <option selected="selected">Select</option>
                                                                                    <option>Commitment</option>
                                                                                    <option>Deposit</option>
                                                                                    <option>Escrow</option>
                                                                                </select>
                                                                            </td>
                                                                            <td align="left" valign="middle" class="blucolor blackbfont">
                                                                                Line Item
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                &#160;&nbsp;
                                                                            </td>
                                                                            <td align="left" valign="middle">
                                                                                <select name="category4" id="Select11" class="ddlistbox">
                                                                                    <option selected="selected">Select</option>
                                                                                    <option>Note Level</option>
                                                                                    <option>Type Line-Item ID</option>
                                                                                </select>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" valign="middle" class="pl10 greyfooter">
                                                                    <input type="button" class="btnstyle" value="OK" />
                                                                    <asp:Button ID="btnCancelLIP3" runat="server" Text="Cancel" CssClass="btnstyle" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </ContentTemplate>
                                </asp:TabPanel>
                            </asp:TabContainer>
                        </td>
                    </tr>
                    <!-- Table Ends Here -->
                </table>
            </asp:Panel>
            <asp:HiddenField ID="hdnHide2" runat="server" />
            <asp:ModalPopupExtender ID="popupRoundOption" runat="server" PopupControlID="PnlRoundOption"
                TargetControlID="hdnHide2" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="PnlRoundOption" runat="server" CssClass="mpSmall" Style="display: none">
                <table border="0" cellspacing="0" cellpadding="0" align="left" width="100%">
                    <tr>
                        <td align="left" valign="middle">
                            <table width="100%" align="left" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td align="left" valign="middle" class="popupheader">
                                        Select Rounding Option
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" valign="middle">
                                        <table width="100%" align="left" cellpadding="0" cellspacing="0" border="0">
                                            <tr>
                                                <td align="left" valign="middle" class="pl10">
                                                    <asp:RadioButtonList ID="rdRound" runat="server">
                                                        <asp:ListItem Text="Up" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Down" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Nearest" Value="2"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" valign="middle">
                                                    <asp:DropDownList ID="ddlRound" runat="server" AutoPostBack="true">
                                                        <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="Half"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="Hole"></asp:ListItem>
                                                        <asp:ListItem Value="3" Text="Quarter"></asp:ListItem>
                                                        <asp:ListItem Value="4" Text="Eighth"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:RadioButton ID="rdRoundOther" runat="server" Text="Other" AutoPostBack="true" />
                                                    <asp:TextBox ID="txtRoundOther" runat="server" CssClass="txtbox"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="pnlOther" runat="server">
                                                        <asp:Label ID="lblOther" runat="server" Text="The Other Rounding Option is for rounding up"></asp:Label>
                                                    </asp:Panel>
                                                    <asp:Panel ID="pnlGroup" runat="server" Visible="false">
                                                        <table>
                                                            <tr>
                                                                <td align="left" valign="middle" class="popupheader" colspan="3">
                                                                    Rounding Result
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblRate" runat="server" Text="Rate : 0.00%"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblMarign" runat="server" Text="Margin : 0.00%"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblRounding" runat="server" Text="No Rounding : 0.000%"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" height="30px" valign="middle" class="pr20 greyfooter">
                                                    <%--<a href="#EditUser1" name="modal" id="A11"><input type="button" value="Ok" class="btnstyle" />
                                </a>--%>
                                                    <asp:Button ID="btnRoundSave" runat="server" Text="OK" CssClass="btnstyle" />&nbsp;
                                                    <asp:Button ID="btnCancelRound" runat="server" Text="Cancel" class="btnstyle" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:HiddenField ID="hdnIPEffDate" runat="server" />
            <asp:HiddenField ID="hdnEPEQtype" runat="server" />
            <asp:HiddenField ID="hdnEPEffDate" runat="server" />
            <asp:HiddenField ID="hdnEPBudgetID" runat="server" />
            <asp:HiddenField ID="hdnAttachDetails" runat="server" />
            <asp:ModalPopupExtender ID="mPopupAttachdetails" runat="server" TargetControlID="hdnAttachDetails"
                PopupControlID="pnlAttachDetails" BackgroundCssClass="modalBackground" CancelControlID="btnCancel018" />
            <asp:Panel ID="pnlAttachDetails" runat="server" CssClass="mpMedium" Style="display: none">
                <table border="0" cellspacing="0" cellpadding="0" align="left" width="100%">
                    <tr>
                        <td align="left" valign="middle">
                            <table width="100%" align="left" cellpadding="0" cellspacing="2" border="0">
                                <tr>
                                    <td>
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" class="allborder">
                                            <tr>
                                                <td align="left" valign="middle" height="24px" class="popupheader" colspan="3">
                                                    Document Details
                                                </td>
                                            </tr>
                                            <tr class="normalrow">
                                                <td width="25%" height="24px" align="left" valign="middle" class="blucolor pl10">
                                                    Note
                                                </td>
                                                <td width="1%" height="24px" align="left" valign="middle">
                                                </td>
                                                <td width="80%" height="24px" align="left" valign="middle">
                                                    <asp:Label ID="lblNoteInfo" runat="server" CssClass="lblRight" />
                                                </td>
                                            </tr>
                                            <tr class="first altrow">
                                                <td width="19%" height="24px" align="left" valign="middle" class="blucolor pl10">
                                                    Attached
                                                </td>
                                                <td width="1%" height="24px" align="left" valign="middle">
                                                </td>
                                                <td width="80%" height="24px" align="left" valign="middle">
                                                    <asp:Label ID="lblAttached" runat="server" CssClass="lblRight" />
                                                </td>
                                            </tr>
                                            <tr class="normalrow">
                                                <td height="24px" align="left" valign="middle" class="blucolor pl10">
                                                    Attachment ID
                                                </td>
                                                <td height="24px" align="left" valign="middle">
                                                </td>
                                                <td height="24px" align="left" valign="middle">
                                                    <asp:Label ID="lblAttachID" runat="server" CssClass="lblRight" />
                                                </td>
                                            </tr>
                                            <tr class="first altrow">
                                                <td height="24px" align="left" valign="middle" class="blucolor pl10">
                                                    Description
                                                </td>
                                                <td height="24px" align="left" valign="middle">
                                                </td>
                                                <td height="24px" align="left" valign="middle">
                                                    <asp:Label ID="lblDescription" runat="server" CssClass="lblRight" />
                                                </td>
                                            </tr>
                                            <%--<tr class="normalrow">
                                                <td height="24px" align="left" valign="middle" class="blucolor pl10">
                                                    File Location
                                                </td>
                                                <td height="24px" align="left" valign="middle">
                                                </td>
                                                <td height="24px" align="left" valign="middle">
                                                    <asp:Label ID="lblFileLocation" runat="server" CssClass="lblRight" />
                                                </td>
                                            </tr>--%>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="middle" height="30px" class="pr10 tborder">
                                        <asp:Button runat="server" ID="btnOk018" Text="OK" CssClass="btnstyle" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:HiddenField ID="hdnAttachDoc" runat="server" />
            <asp:ModalPopupExtender ID="mPopupAttachDoc" runat="server" TargetControlID="hdnAttachDoc"
                PopupControlID="pnlAttachDoc" BackgroundCssClass="modalBackground" />
            <asp:UpdatePanel ID="docPanel1" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="pnlAttachDoc" runat="server" CssClass="mpLarge" Style="display: none">
                        <table width="99%" align="center" cellpadding="1" cellspacing="0" border="0" style="display: block">
                            <tr id="trValidsumm" runat="server">
                                <td align="left" valign="middle">
                                    <asp:ValidationSummary class="ErrorSummary" ID="ValidationSummaryAttachments" runat="server"
                                        ValidationGroup="Attachment" ShowSummary="true" />
                                </td>
                            </tr>
                        </table>
                        <table width="99%" align="center" cellpadding="1" cellspacing="0" border="0">
                            <tr>
                                <td align="left" valign="middle" height="24px" class="pl5 bgcolor4 blackbfont">
                                    Attach Document
                                </td>
                            </tr>
                            <tr>
                                <td width="100%" align="left" valign="top">
                                    <table align="left" width="100%" cellpadding="1" cellspacing="0" border="0" class="allborder">
                                        <tr>
                                            <td height="24px" align="left" valign="middle" class="blucolor">
                                                Enter the path and filename of the document you want to attach<span class="colorerr">*</span>
                                            </td>
                                            <td width="10" align="left" valign="middle">
                                            </td>
                                            <td align="left" valign="middle">
                                                <asp:FileUpload ID="fuAttachments" runat="server" Width="330px" />
                                                <asp:RequiredFieldValidator ID="reqFlValAttach" runat="server" ValidationGroup="Attachment"
                                                    ControlToValidate="fuAttachments" Display="None" ErrorMessage="Please select a record to attach." />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" valign="middle" class="blucolor">
                                                Document Description
                                            </td>
                                            <td width="10" align="left" valign="middle">
                                            </td>
                                            <td align="left" valign="top">
                                                <asp:TextBox ID="txtDocument" runat="server" CssClass="txtboxMultiLine" TextMode="MultiLine"
                                                    onkeyDown="return checkTextAreaMaxLength(this,event,'250');" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="middle" height="30px">
                                    <asp:Button ID="btnAttachmentOk" runat="server" Text="OK" CssClass="btnstyle" ValidationGroup="Attachment"
                                        OnClick="btnAttachmentOk_Click" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btnAttachmentCancel" runat="server" Text="Cancel" CssClass="btnstyle" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:HiddenField ID="hdnAddBudget" runat="server" />
            <asp:ModalPopupExtender ID="mpeAddBudget" runat="server" TargetControlID="hdnAddBudget"
                PopupControlID="pnlAddBudget" BackgroundCssClass="modalBackground" CancelControlID="btnBudgetCancel" />
            <asp:Panel ID="pnlAddBudget" runat="server" CssClass="mpMedium" Style="display: none">
                <table width="100%" align="center" cellpadding="1" cellspacing="0" border="0">
                    <tr>
                        <td align="left" valign="middle" height="24px" class="popupheader" colspan="6">
                            Budget Interest
                        </td>
                    </tr>
                    <tr>
                        <td height="24px" align="left" valign="middle" class="blucolor pl10">
                            Note:
                        </td>
                        <td align="left" valign="left">
                        </td>
                        <td align="left" valign="left" class="pl10">
                            <asp:Label ID="lblNoteNo" runat="server"></asp:Label>
                        </td>
                        <td align="left" valign="left" class="pl10">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td height="24px" align="left" valign="middle" class="blucolor pl10">
                            Unit:
                        </td>
                        <td align="left" valign="left">
                        </td>
                        <td align="left" valign="left" class="pl10">
                            <asp:Label ID="lblUnitNumber" runat="server"></asp:Label>
                        </td>
                        <td align="left" valign="left" class="pl10">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td height="24px" align="left" valign="middle" class="blucolor pl10">
                            ID:
                        </td>
                        <td align="left" valign="left">
                        </td>
                        <td colspan="2" align="left" valign="left" class="pl10">
                            <asp:Label ID="lblBudgetId" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%" height="24px" align="left" valign="middle" class="blucolor pl10">
                            Group ID
                        </td>
                        <td width="1%" align="left" valign="left">
                        </td>
                        <td width="15%" align="left" valign="left" class="pl10">
                            <asp:TextBox runat="server" ID="txtGroupID" CssClass="txtbox" MaxLength="2" />
                        </td>
                        <td width="*" align="left" valign="left" class="pl10">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="middle" height="24px" class="blucolor pl10">
                            Description
                        </td>
                        <td align="left" valign="middle">
                        </td>
                        <td align="left" valign="middle" class="pl10" colspan="2">
                            <asp:TextBox runat="server" ID="txtGroupDesc" CssClass="txtbox" MaxLength="50" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="middle" height="30px" class="pr10 tborder" colspan="6">
                            <asp:Button runat="server" ID="btnAddBudgetOK" Text="OK" CssClass="btnstyle" OnClick="btnAddBudget_Click"
                                OnClientClick="return IsGroupEmpty();" />&nbsp;&nbsp;
                            <asp:Button ID="btnBudgetCancel" runat="server" Text="Cancel" CssClass="btnstyle" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:ModalPopupExtender ID="mpopupDocAdd" runat="server" TargetControlID="hdnDocAdd"
                PopupControlID="pnlDocAdd" BackgroundCssClass="modalBackground" CancelControlID="btnDocCancel" />
            <asp:Panel ID="pnlDocAdd" runat="server" CssClass="mpMedium" Style="display: none">
                <table width="100%" cellpadding="1" cellspacing="0" border="0">
                    <tr>
                        <td align="left" valign="middle" height="24px" class="popupheader">
                            Document Insert
                        </td>
                    </tr>
                    <tr>
                        <td width="100%" align="left" valign="top">
                            <table align="left" width="100%" cellpadding="0" cellspacing="8" border="0" class="allborder">
                                <tr>
                                    <td colspan="3">
                                        <table align="left" width="100%" cellpadding="0" cellspacing="8" border="0" class="allgborder">
                                            <tr>
                                                <td align="left" valign="middle" class="blucolor pl10" style="width: 5%">
                                                    Note:
                                                </td>
                                                <td align="left" style="width: 45%">
                                                    <asp:Label runat="server" ID="lblDocNoteNo"></asp:Label>
                                                </td>
                                                <td align="left" valign="middle" class="blucolor pl10" style="width: 5%">
                                                    Unit:
                                                </td>
                                                <td align="left" style="width: 45%">
                                                    <asp:Label runat="server" ID="lblDocUnit"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" valign="middle" class="blucolor pl10">
                                                    ID
                                                </td>
                                                <td align="left">
                                                    <asp:Label runat="server" ID="lblDocId"></asp:Label>
                                                </td>
                                                <td align="left" valign="middle" class="pl10">
                                                </td>
                                                <td align="left" valign="middle">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" valign="middle" class="blucolor pl10">
                                        Group ID
                                    </td>
                                    <td width="18" align="left" valign="middle">
                                    </td>
                                    <td align="left" valign="middle" class="pl10">
                                        <asp:TextBox runat="server" ID="txtDocGroupId" CssClass="txtbox" MaxLength="2" />
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server" TargetControlID="txtGroupId"
                                            FilterType="Numbers" Enabled="True" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" valign="middle" class="blucolor pl10">
                                        Priority
                                    </td>
                                    <td align="left" valign="middle">
                                    </td>
                                    <td align="left" valign="middle" class="pl10">
                                        <asp:TextBox runat="server" ID="txtPri" CssClass="txtbox" Text="" MaxLength="2" Width="43px" />
                                        <asp:FilteredTextBoxExtender ID="txtPri_FilteredTextBoxExtender1" runat="server"
                                            TargetControlID="txtPri" FilterType="Numbers" Enabled="True" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" valign="middle" class="blucolor pl10">
                                        Number of Days
                                    </td>
                                    <td align="left" valign="middle">
                                    </td>
                                    <td align="left" valign="middle" class="pl10">
                                        <asp:TextBox runat="server" ID="txtBorDays" CssClass="txtbox" Text="" MaxLength="3"
                                            Height="16px" Width="43px" />
                                        <asp:FilteredTextBoxExtender ID="txtBorDays_FilteredTextBoxExtender1" runat="server"
                                            TargetControlID="txtBorDays" FilterType="Numbers" Enabled="True" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" valign="middle" class="blucolor pl10">
                                        Description
                                    </td>
                                    <td align="left" valign="middle">
                                    </td>
                                    <td align="left" valign="middle" class="pl10">
                                        <asp:TextBox ID="txtDocDesc" runat="server" TextMode="MultiLine" Width="278px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="middle" height="30px" class="pr10 tborder">
                            <asp:Button runat="server" ID="btnDocIdMain" Text="OK" CssClass="btnstyle" OnClick="btnSaveDoc_Click"
                                OnClientClick="return CheckDoc();" />&nbsp;&nbsp;
                            <asp:Button runat="server" ID="btnDocCancel" Text="Cancel" CssClass="btnstyle" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:HiddenField ID="hdnDocEdit" runat="server" />
            <asp:HiddenField ID="hdnDocAdd" runat="server" />
            <asp:HiddenField ID="hdnDocId" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnAttachmentOk" />
        </Triggers>
    </asp:UpdatePanel>

    <script language="javascript" type="text/javascript">
        //Save Start

        function CheckDoc() {
            var vDesc = $("#<%= txtDocDesc.ClientID %>").val();
            if (vDesc == '') {
                alert('Please enter a description.');
                return false;
            }

        }
        var btnBackendSave = '<%= btnSaveProcess.ClientID %>';
        function Save1(mProp_Inquiry, adminFlag, noteAction, bRenewal, noteMode, nRenewalCount, dPrevMatDate, dPrvCompDate) {
            var bFeePostCompDate;
            var bFeePostMaturity;
            if (mProp_Inquiry == true) {
                return false;
            }

            if (confirm("Are you sure you want to save note edits?") == false) {
                return false;
            }

            var chkRevolving = document.getElementById('<%= chkRevolving.ClientID %>');
            var chkForeclosure = document.getElementById('<%= chkForeclosure.ClientID %>');
            var btnSave = document.getElementById('<%= btnProjectSave.ClientID %>');
            var tbNoteInfo = null;
            if (chkRevolving.checked == true && chkForeclosure.checked == true) {
                alert("This loan is flagged as FannieMae and should not revolve.");
                SetActiveTab(1); //General TAB
                chkRevolving.checked = false;
                chkRevolving.disabled = true;

                if (btnSave.disabled == false) {
                    btnSave.focus();
                    return false;
                }
            } //PASSED

            var drpdwnlstAdmin = document.getElementById('<%= drpdwnlstAdmin.ClientID %>');
            if (adminFlag == "Y" && drpdwnlstAdmin.value == "") {
                tbNoteInfo.ActiveTab = 0; // Project Tab
                alert("Please Assign the Administrator associated for this record");
                drpdwnlstAdmin.focus();
                return false;
            } //PASSED

            var txtDateTabMturty = document.getElementById('<%= txtDateTabMturty.ClientID %>');
            var sRenewal;
            if (noteAction == 2 && bRenewal == false && noteMode != "PIPELINE") {
                if (ConvertToDateTime(dPrevMatDate) != ConvertToDateTime(txtDateTabMturty.value)) {
                    bRenewal = true;
                    bFeePostMaturity = 0;
                    if (confirm("Are you renewing this note?", "Maturity Date Change") == true) {
                        sRenewal = "Loan Renewal";
                        nRenewalCount += 1;

                        if (confirm("Do you want to post a Renewal Fee?", "Fee Posting") == true) {
                            bFeePostMaturity = -1;
                        }
                    }
                    else {
                        sRenewal = "Maturity Date Changed";
                    }
                }
            }

            var chkFannieMae = document.getElementById('<%= chkFannieMae.ClientID %>');
            //alert(chkFannieMae.checked + ": Checked");
            //alert(dPrvCompDate + ": dPrvCompDate");
            if (chkFannieMae.checked == true && dPrvCompDate != "") {
                /*NEED TO FIX BELOW LINE*/
                var dateOne = new Date(dPrvCompDate);
                var txtDateTabCnsCmpl = new Date(document.getElementById('<%= txtDateTabCnsCmpl.ClientID %>').value);
                if (dateOne < txtDateTabCnsCmpl && DateDiff(dPrvCompDate, txtDateTabCnsCmpl) > 0) {
                    if (confirm("Do you want to post an Extension Fee?") == true) {
                        bFeePostCompDate = -1;
                    }
                }
            }

            this.document.forms[0].__EVENTTARGET.value = "{\"bFeePostCompDate\":\"" + bFeePostCompDate + "\",\"bFeePostMaturity\":\"" + bFeePostMaturity + "\",\"nRenewalCount\":\"" + nRenewalCount + "\",\"bRenewal\":\"" + bRenewal + "\"}";
            this.document.forms[0].__EVENTARGUMENT.value = "save1";
            document.getElementById(btnBackendSave).click();
            //raise button click for save cont..with values.
        }

        function DateDiff(dtFrom, dtTo) {
            var dt1 = new Date(dtFrom);
            var dt2 = new Date(dtTo);
            var diff = dt2.getTime() - dt1.getTime();
            var days = diff / (1000 * 60 * 60 * 24);
            return days;
        }

        function ConvertToDateTime(dateVal) {
            var myDate = dateVal;
            var regExp = /(\d{1,2})\/(\d{1,2})\/(\d{2,4})/;
            return myDate.replace(regExp, "$3$1$2");
        }

        function Save2(cEst, cAlloc) {
            var cDiff = "";
            if (cEst != cAlloc) {
                cDiff = cEst - cAlloc;
                var sMsg = "Budget fields 'Total Estimated' and 'Total Allocated' do not equal!" + "\n" + "This may cause problems with funding and balancing your loan budget." + "\n" + "\n" + "Continue and Exit anyway?";
                sMsg = sMsg + "\r" + "\r" + "Press YES to continue, NO to modify.";
                if (confirm(sMsg) == false) {
                    cAlloc = 0;
                    cEst = 0;
                    SetActiveTab(5); //tbNoteInfo.ActiveTab = GetTabIndex("FINANCIALS"); // 5
                    return false;
                }
                else {
                    this.document.forms[0].__EVENTTARGET.value = "{\"cDiff\":\"" + cDiff + "\",\"cEst\":\"" + cEst + "\",\"cAlloc\":\"" + cAlloc + "\"}";
                    this.document.forms[0].__EVENTARGUMENT.value = "save2";
                    document.getElementById(btnBackendSave).click();
                    return false;
                }
            }
            else {
                this.document.forms[0].__EVENTTARGET.value = "{\"cDiff\":\"" + cDiff + "\",\"cEst\":\"" + cEst + "\",\"cAlloc\":\"" + cAlloc + "\"}";
                this.document.forms[0].__EVENTARGUMENT.value = "save2";
                document.getElementById(btnBackendSave).click();
                return false;
            }
        }

        function Save3(dblFincTabprincBal3, dblFincTabLoanCommit3) {
            //alert("dblFincTabprincBal3=" + dblFincTabprincBal3 + ":  dblFincTabLoanCommit3=" + dblFincTabLoanCommit3);
            if ((dblFincTabprincBal3 > dblFincTabLoanCommit3) || dblFincTabprincBal3 < 0) {
                alert("Invalid amount.  The Principal Balance Cap cannot exceed the Loan Commitment amount nor can it be a negative amount.");
                SetActiveTab(5); //tbNoteInfo.ActiveTab = GetTabIndex("FINANCIALS");
                var txtFincTabprincBal2 = document.getElementById('<%= txtFincTabprincBal2.ClientID %>');
                if (txtFincTabprincBal2.disabled == false) {
                    txtFincTabprincBal2.focus();
                }
                return false;
            }
            else {
                this.document.forms[0].__EVENTTARGET.value = "";
                this.document.forms[0].__EVENTARGUMENT.value = "save3";
                document.getElementById(btnBackendSave).click();
                return false;
            }
        }

        function Save4(sMsg) {
            if (confirm(sMsg) == false) {
                return false;
            }
            this.document.forms[0].__EVENTTARGET.value = "";
            this.document.forms[0].__EVENTARGUMENT.value = "save4";
            document.getElementById(btnBackendSave).click();
            return false;
        }

        function Save5(sMsg) {
            if (confirm(sMsg) == false) {
                return false;
            }
            this.document.forms[0].__EVENTTARGET.value = "";
            this.document.forms[0].__EVENTARGUMENT.value = "save5";
            document.getElementById(btnBackendSave).click();
            return false;
        }

        //ValidateAdmin
        function Save6(sMsg, mobjAdministratorsNotFound, mobjAdministratorsInitalValue) {
            if (confirm(sMsg) == false) {
                SetActiveTab(6); //tbNoteInfo.ActiveTab = GetTabIndex("EQPROF");
                //bRateChange = Convert.ToInt32(false);
                return false;
            }
            else {
                if (mobjAdministratorsNotFound) { // Initial Value NOT Found; INVALID
                    var drpdwnlstAdmin = document.getElementById('<%= drpdwnlstAdmin.ClientID %>');
                    if (drpdwnlstAdmin.value == mobjAdministratorsInitalValue) { // Still Same
                        alert("Current 'Administrator' is not Valid, Please select a valid user");
                        SetActiveTab(0);
                        return false;
                    }
                    else {
                        return true;
                    }
                }
            }
        }

        function Save7(blnPostExtFee, nFeeAmount, bFeePostCompDate, bFeePostMaturity, isValidProp, noteType) {
            if (bFeePostCompDate != 0) {
                if (blnPostExtFee == false) {
                    alert("Extension fee cannot be calculated.  The loan is not participated or the participant interest profile is missing.");
                    //goto Continue;
                }

                if (nFeeAmount <= 0) {
                    alert("No Extension Fee Required.");
                    //goto Continue;
                }
            }

            if (bFeePostMaturity != 0) {
                //PostFee(this.NoteNumber, this.UnitNumber, cboNote[0], "NI:Renewal", , , "Renewal Fee", true);
            }

            var ddlMLoc = document.getElementById('<%= ddlMLoc.ClientID %>');
            var ddlSLoc = document.getElementById('<%= ddlSLoc.ClientID %>');
            if (ddlMLoc.selectedIndex > 0) {
                if (ddlSLoc.selectedIndex == 0) {
                    alert("If a Master LOC is selected then a Sub LOC must be selected.");
                    return false;
                }
            }
            else {
                if (ddlSLoc.selectedIndex > 0) {
                    alert("If a Sub LOC is selected then a Master LOC must be selected.");
                    return false;
                }
            }

            if (this.mblnLOCProblemOnLoad) {
                if (ddlMLoc.selectedIndex <= 0) {
                    sMsg = "While Loading this notes information, the Master/Sub LOC originally assigned could not be located" + "\n" + "\n";
                    sMsg = sMsg + "A Line of Credit has not been set for this loan and the assignemt will be removed if you save now" + "\n" + "\n";
                    sMsg = sMsg + "Press YES to continue, NO to modify.";
                    if (confirm(sMsg) == false) {
                        return false;
                    }
                }
                else {
                }
            }

            var txtDateTabQotPost = document.getElementById('<%= txtDateTabQotPost.ClientID %>');
            if (isValidProp == -1) {
                alert("There are Sub Notes attached to this Master Note which are not paid-off. You cannot input a Payoff Posted date until all Sub Notes are paid-off.");
                SetActiveTab(2);
                txtDateTabQotPost.focus();
                return false;
            }


            this.document.forms[0].__EVENTTARGET.value = "";
            this.document.forms[0].__EVENTARGUMENT.value = "save7";
            document.getElementById(btnBackendSave).click();
            return false;
            //call post fee function. Then save will be called
        }

        function LoadNonAccural() {
            var returnVal;
            var chkNonAccural = document.getElementById('<%= chkNonAccural.ClientID %>');
            var noteNo = '<%= NoteNumber %>';
            var unitNo = '<%= UnitNumber %>';
            var borrNo = '<%= BorrowerNo %>';
            returnVal = window.showModalDialog("ApplyNonAccuralNoteInfo.aspx?N=" + noteNo + "&U=" + unitNo + "&B=" + borrNo + "&CHKNONACCURAL=" + chkNonAccural.checked, "popup_window", "dialogWidth:750px;dialogHeight:280px;");
            if (returnVal == undefined) {
                //this.document.forms[0].__EVENTTARGET.value = "";
                //this.document.forms[0].__EVENTARGUMENT.value = "savefinal";
                //document.getElementById(btnBackendSave).click();
                //alert("NonAccural Failed!");
                return false;
            }
            //alert(returnVal.EffDate);
            //alert(returnVal.OldEffDate);
            this.document.forms[0].__EVENTTARGET.value = "effdate:" + returnVal.EffDate + ",oldeffdate:" + returnVal.OldEffDate;
            this.document.forms[0].__EVENTARGUMENT.value = "savefinal";
            document.getElementById(btnBackendSave).click();
            return false;
        }

        function SetActiveTab(tabNumber) {
            var tabControl = '<%= TabNoteInfo.ClientID %>';
            //var ctrl = $find(tabControl);
            //ctrl.set_activeTab(ctrl.get_tabs()[tabNumber]);
        }

        function WOpenAuditTrail(note, unit, msg) {
            window.showModalDialog('AuditReportNoteInfo.aspx?NoteNo=' + note + '&UnitNo=' + unit + '&ErrMsg=' + msg, '', 'center:yes; modal:yes; edge:Raised; resizable:no;scrollbars:no;menubar=no;status:no;dialogWidth:700px;dialogHeight:400px;');
            this.document.forms[0].__EVENTTARGET.value = "";
            this.document.forms[0].__EVENTARGUMENT.value = "auditresponse";
            document.getElementById(btnBackendSave).click();
            return false;
        }

        //Save End

        $(function() {
            $('#' + '<%# txtPrjctCity.ClientID %>').keydown(function(e) {
                if (e.shiftKey || e.ctrlKey || e.altKey) {
                    e.preventDefault();
                } else {
                    var key = e.keyCode;
                    if (!((key == 8) || (key == 32) || (key == 46) || (key >= 35 && key <= 40) || (key >= 65 && key <= 90))) {
                        e.preventDefault();
                    }
                }
            });
        });
        
    </script>

</asp:Content>
