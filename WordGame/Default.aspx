<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> Word Guessing Game </title>

    <style type="text/css">
        #form1 {
            height: 291px;
        }
    </style>

    <script type="text/javascript">

        if (window.captureEvents) {
            window.captureEvents(Event.KeyUp);
            window.onkeyup = executeCode;
        }

        else if (window.attachEvent) {
            document.attachEvent('onkeyup', executeCode);
        }

        function executeCode(event) {
            if (event == null) {
                event = window.event;
            }
            var key = parseInt(event.keyCode, 10);
       
            switch (key) {
                // Q-P
                case 81: 
                    document.getElementById('qBtn').click();
                    break;
                case 87:
                    document.getElementById('wBtn').click();
                    break;
                case 69: 
                    document.getElementById('eBtn').click();
                    break;
                case 82: 
                    document.getElementById('rBtn').click();
                    break;
                case 84: 
                    document.getElementById('tBtn').click();
                    break;
                case 89:  
                    document.getElementById('yBtn').click();
                    break;
                case 85:  
                    document.getElementById('uBtn').click();
                    break;
                case 73: 
                    document.getElementById('iBtn').click();
                    break;
                case 79: 
                    document.getElementById('oBtn').click();
                    break;
                case 80:
                    document.getElementById('pBtn').click();
                    break;
                // A-L
                case 65:
                    document.getElementById('aBtn').click();
                    break;
                case 83:
                    document.getElementById('sBtn').click();
                    break;
                case 68:
                    document.getElementById('dBtn').click();
                    break;
                case 70:
                    document.getElementById('fBtn').click();
                    break;
                case 71:
                    document.getElementById('gBtn').click();
                    break;
                case 72:
                    document.getElementById('hBtn').click();
                    break;
                case 74:
                    document.getElementById('jBtn').click();
                    break;
                case 75:
                    document.getElementById('kBtn').click();
                    break;
                case 76:
                    document.getElementById('lBtn').click();
                    break;
                // Z-M
                case 90:
                    document.getElementById('zBtn').click();
                    break;
                case 88:
                    document.getElementById('xBtn').click();
                    break;
                case 67:
                    document.getElementById('cBtn').click();
                    break;
                case 86:
                    document.getElementById('vBtn').click();
                    break;
                case 66:
                    document.getElementById('bBtn').click();
                    break;
                case 78:
                    document.getElementById('nBtn').click();
                    break;
                case 77:
                    document.getElementById('mBtn').click();
                    break;
                // [ && ]
                case 219:
                    document.getElementById('newBtn').click();
                    break; 
                case 221:
                    document.getElementById('exitBtn').click();
                    break;
            }

            event.returnValue = false
            return false;
        }

 </script>
</head>
<body style="height: 423px">

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
            <asp:Label ID="incorrectLbl" runat="server" Text="Incorrect Letters:" Font-Italic="True"></asp:Label>
&nbsp;<asp:Label ID="lettersLbl" runat="server"></asp:Label>
        <br />
            <asp:Label ID="hintTextLbl" runat="server" Font-Bold="True" Text="HINT: "></asp:Label>
        <asp:Label ID="hintLbl" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Label ID="chainLbl" runat="server"></asp:Label>
            <br />
            <asp:Label ID="countLbl" runat="server"></asp:Label>
        <br />
        <br />
        <asp:Button ID="qBtn" runat="server" Text="Q" OnClick="letterGuessed" />
        <asp:Button ID="wBtn" runat="server" Text="W" OnClick="letterGuessed" />
        <asp:Button ID="eBtn" runat="server" Text="E" OnClick="letterGuessed" />
        <asp:Button ID="rBtn" runat="server" Text="R" OnClick="letterGuessed" />
        <asp:Button ID="tBtn" runat="server" Text="T" OnClick="letterGuessed" />
        <asp:Button ID="yBtn" runat="server" Text="Y" OnClick="letterGuessed" />
        <asp:Button ID="uBtn" runat="server" Text="U" OnClick="letterGuessed" />
        <asp:Button ID="iBtn" runat="server" Text="I" OnClick="letterGuessed" />
        <asp:Button ID="oBtn" runat="server" Text="O" OnClick="letterGuessed" />
        <asp:Button ID="pBtn" runat="server" Text="P" OnClick="letterGuessed" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
        <asp:Label ID="scoreLbl" runat="server"></asp:Label>
        <br />
        <asp:Button ID="aBtn" runat="server" Text="A" OnClick="letterGuessed" />
        <asp:Button ID="sBtn" runat="server" Text="S" OnClick="letterGuessed" />
        <asp:Button ID="dBtn" runat="server" Text="D" OnClick="letterGuessed" />
        <asp:Button ID="fBtn" runat="server" Text="F" OnClick="letterGuessed" />
        <asp:Button ID="gBtn" runat="server" Text="G" OnClick="letterGuessed" />
        <asp:Button ID="hBtn" runat="server" Text="H" OnClick="letterGuessed" />
        <asp:Button ID="jBtn" runat="server" Text="J" OnClick="letterGuessed" />
        <asp:Button ID="kBtn" runat="server" Text="K" OnClick="letterGuessed" />
        <asp:Button ID="lBtn" runat="server" Text="L" OnClick="letterGuessed" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="guessNoLbl" runat="server"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        <asp:Button ID="zBtn" runat="server" Text="Z" OnClick="letterGuessed" />
        <asp:Button ID="xBtn" runat="server" Text="X" OnClick="letterGuessed" />
        <asp:Button ID="cBtn" runat="server" Text="C" OnClick="letterGuessed" />
        <asp:Button ID="vBtn" runat="server" Text="V" OnClick="letterGuessed" />
        <asp:Button ID="bBtn" runat="server" Text="B" OnClick="letterGuessed" />
        <asp:Button ID="nBtn" runat="server" Text="N" OnClick="letterGuessed" />
        <asp:Button ID="mBtn" runat="server" Text="M" OnClick="letterGuessed" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;<asp:Label ID="multiLbl" runat="server"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br /> <br /> 
            <asp:Label ID="livesLbl" runat="server"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <br />
        <br />
        <asp:Label ID="timeLbl" runat="server"></asp:Label>
            <br />
        <br />
        <asp:Button ID="exitBtn" runat="server" Text="Quit" Width="62px" OnClick="exitBtn_Click" />
            &nbsp;&nbsp;<asp:Button ID="newBtn" runat="server" Text="Restart" OnClick="newBtn_Click" Visible="False" Width="59px" /> &nbsp;
        <br />
        <br />

        </ContentTemplate>
        </asp:UpdatePanel>
        <asp:Timer ID="timer" runat="server" Interval="1000" OnTick="timer_Tick"> </asp:Timer>
    
        <br />
        <br />

    </div>
    </form>
</body>
</html>


