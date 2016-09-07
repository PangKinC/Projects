<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #form1 {
            height: 291px;
        }
    </style>
</head>
<body style="height: 574px">

    <form id="form1" runat="server">
    <asp:ScriptManager id="scriptManager" runat="server" />
    <div style="height: 606px">
        <asp:UpdatePanel runat="server" id="timePanel" updatemode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger controlid="timer" EventName="Tick" />
        </Triggers>
        <ContentTemplate>
        <asp:Label ID="wordLbl" runat="server" Font-Size="X-Large"></asp:Label>  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        <br />
            <asp:Label ID="incorrectLbl" runat="server" Text="Incorrect Letters:"></asp:Label>
&nbsp;<asp:Label ID="lettersLbl" runat="server"></asp:Label>
        <br />
        <asp:Label ID="hintLbl" runat="server"></asp:Label>
            <br />
            <asp:Label ID="chainLbl" runat="server"></asp:Label>
        <br />
        <br />
        <asp:Button ID="qBtn" runat="server" Text="Q" CommandArgument="Q" OnCommand="letterGuessed" />
        <asp:Button ID="wBtn" runat="server" Text="W" CommandArgument="W" OnCommand="letterGuessed" />
        <asp:Button ID="eBtn" runat="server" Text="E" CommandArgument="E" OnCommand="letterGuessed" />
        <asp:Button ID="rBtn" runat="server" Text="R" CommandArgument="R" OnCommand="letterGuessed" />
        <asp:Button ID="tBtn" runat="server" Text="T" CommandArgument="T" OnCommand="letterGuessed" />
        <asp:Button ID="yBtn" runat="server" Text="Y" CommandArgument="Y" OnCommand="letterGuessed" />
        <asp:Button ID="uBtn" runat="server" Text="U" CommandArgument="U" OnCommand="letterGuessed" />
        <asp:Button ID="iBtn" runat="server" Text="I" CommandArgument="I" OnCommand="letterGuessed" />
        <asp:Button ID="oBtn" runat="server" Text="O" CommandArgument="O" OnCommand="letterGuessed" />
        <asp:Button ID="pBtn" runat="server" Text="P" CommandArgument="P" OnCommand="letterGuessed" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
        <asp:Label ID="scoreLbl" runat="server"></asp:Label>
        <br />
        <asp:Button ID="aBtn" runat="server" Text="A" CommandArgument="A" OnCommand="letterGuessed" />
        <asp:Button ID="sBtn" runat="server" Text="S" CommandArgument="S" OnCommand="letterGuessed" />
        <asp:Button ID="dBtn" runat="server" Text="D" CommandArgument="D" OnCommand="letterGuessed" />
        <asp:Button ID="fBtn" runat="server" Text="F" CommandArgument="F" OnCommand="letterGuessed" />
        <asp:Button ID="gBtn" runat="server" Text="G" CommandArgument="G" OnCommand="letterGuessed" />
        <asp:Button ID="hBtn" runat="server" Text="H" CommandArgument="H" OnCommand="letterGuessed" />
        <asp:Button ID="jBtn" runat="server" Text="J" CommandArgument="J" OnCommand="letterGuessed" />
        <asp:Button ID="kBtn" runat="server" Text="K" CommandArgument="K" OnCommand="letterGuessed" />
        <asp:Button ID="lBtn" runat="server" Text="L" CommandArgument="L" OnCommand="letterGuessed" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="guessNoLbl" runat="server"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        <asp:Button ID="zBtn" runat="server" Text="Z" CommandArgument="Z" OnCommand="letterGuessed" />
        <asp:Button ID="xBtn" runat="server" Text="X" CommandArgument="X" OnCommand="letterGuessed" />
        <asp:Button ID="cBtn" runat="server" Text="C" CommandArgument="C" OnCommand="letterGuessed" />
        <asp:Button ID="vBtn" runat="server" Text="V" CommandArgument="V" OnCommand="letterGuessed" />
        <asp:Button ID="bBtn" runat="server" Text="B" CommandArgument="B" OnCommand="letterGuessed" />
        <asp:Button ID="nBtn" runat="server" Text="N" CommandArgument="N" OnCommand="letterGuessed" />
        <asp:Button ID="mBtn" runat="server" Text="M" CommandArgument="M" OnCommand="letterGuessed" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;<asp:Label ID="multiLbl" runat="server"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        <br />
        <asp:Label ID="timeLbl" runat="server"></asp:Label>
        </ContentTemplate>
        </asp:UpdatePanel>
        <asp:Timer ID="timer" runat="server" Interval="1000" OnTick="timer_Tick"> </asp:Timer>
    
        <br />
        <asp:Button ID="newBtn" runat="server" Text="Restart" OnClick="newBtn_Click" Visible="False" />
&nbsp;
        <asp:Button ID="exitBtn" runat="server" Text="Exit" Width="62px" OnClick="exitBtn_Click" Visible="False" />
        <br />
        <br />
    

        <br />
        <br />



    </div>
    </form>
</body>
</html>
