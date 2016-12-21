<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="Game.aspx.cs" Inherits="Game" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TopContent" runat="Server">
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
                document.getElementById('<%=qBtn.ClientID%>').click();
                break;
            case 87:
                document.getElementById('<%=wBtn.ClientID%>').click();
                break;
            case 69: 
                document.getElementById('<%=eBtn.ClientID%>').click();
                break;
            case 82: 
                document.getElementById('<%=rBtn.ClientID%>').click();
                break;
            case 84: 
                document.getElementById('<%=tBtn.ClientID%>').click();
                break;
            case 89:  
                document.getElementById('<%=yBtn.ClientID%>').click();
                break;
            case 85:  
                document.getElementById('<%=uBtn.ClientID%>').click();
                break;
            case 73: 
                document.getElementById('<%=iBtn.ClientID%>').click();
                break;
            case 79: 
                document.getElementById('<%=oBtn.ClientID%>').click();
                break;
            case 80:
                document.getElementById('<%=pBtn.ClientID%>').click();
                break;
            // A-L
            case 65:
                document.getElementById('<%=aBtn.ClientID%>').click();
             break;
            case 83:
                document.getElementById('<%=sBtn.ClientID%>').click();
                break;
            case 68:
                document.getElementById('<%=dBtn.ClientID%>').click();
                break;
            case 70:
                document.getElementById('<%=fBtn.ClientID%>').click();
                break;
            case 71:
                document.getElementById('<%=gBtn.ClientID%>').click();
                break;
            case 72:
                document.getElementById('<%=hBtn.ClientID%>').click();
                break;
            case 74:
                document.getElementById('<%=jBtn.ClientID%>').click();
                break;
            case 75:
                document.getElementById('<%=kBtn.ClientID%>').click();
                break;
            case 76:
                document.getElementById('<%=lBtn.ClientID%>').click();
                break;
            // Z-M
            case 90:
                document.getElementById('<%=zBtn.ClientID%>').click();
                break;
            case 88:
                document.getElementById('<%=xBtn.ClientID%>').click();
                break;
            case 67:
                document.getElementById('<%=cBtn.ClientID%>').click();
                break;
            case 86:
                document.getElementById('<%=vBtn.ClientID%>').click();
                break;
            case 66:
                document.getElementById('<%=bBtn.ClientID%>').click();
                break;
            case 78:
                document.getElementById('<%=nBtn.ClientID%>').click();
                break;
            case 77:
                document.getElementById('<%=mBtn.ClientID%>').click();
                break;
            // [ && ]
            case 219:
                document.getElementById('<%=newBtn.ClientID%>').click();
                break; 
            case 221:
                document.getElementById('<%=nextBtn.ClientID%>').click();
                break;
        }

        event.returnValue = false
        return false;
        }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:ScriptManager id="scriptManager" runat="server" />

    <asp:UpdatePanel runat="server" id="timePanel" updatemode="Conditional">
    <Triggers> <asp:AsyncPostBackTrigger controlid="timer" EventName="Tick" /> </Triggers>
    <ContentTemplate>

    <div class="container">

        <div class="row">
            <div class="col-md-12">
                <div class="text-center">
                    <asp:Label ID="diffLbl" runat="server" Text="Difficulty: (diff)." Font-Bold="True" Font-Names="Lucida Sans Unicode" Font-Size="Large"></asp:Label>
                    <br />
                    <asp:Label ID="subLbl" runat="server" Text="Subject: (subject)." Font-Bold="True" Font-Names="Lucida Sans Unicode" Font-Size="Large"></asp:Label>
                </div>
            </div>
        </div>
        
        <div class="row">
            <div class="col-md-3"> </div>
            <div class="col-md-6">
                <br /> <br />
                <div class="well">
                <div class="text-center">
                    <asp:Label ID="timeMsgLbl" runat="server" Font-Bold="true" Text="Next Word In: " Font-Names="Lucida Sans Unicode" Font-Size="X-Large" Visible="false"> </asp:Label>
                    <asp:Label ID="timeLbl" runat="server" Font-Bold="True" Font-Names="Lucida Sans Unicode" Font-Size="X-Large"></asp:Label>
                </div>
                </div>
            </div>
            <div class="col-md-3"> </div>
        </div>

        <div class="row">
            <div class="col-md-3"> 
                <div class="well">
                <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> 
                <div class="text-center">
                    <asp:Label ID="guessLbl" runat="server" Font-Bold="true" Text="Wrong Letter Count: " Font-Names="Lucida Sans Unicode" Font-Size="Large"> </asp:Label>
                    <asp:Label ID="guessNoLbl" runat="server" Font-Italic="true" Font-Size="Large"> </asp:Label>
                    <br /> <br />
                    <asp:Label ID="wrongLbl" runat="server" Font-Bold="true" Text="Wrong Words: " Font-Names="Lucida Sans Unicode" Font-Size="Large"> </asp:Label>
                    <asp:Label ID="wrongNoLbl" runat="server" Font-Italic="true" Font-Size="Large"> </asp:Label>
                    <br /> <br />
                    <asp:Label ID="countLbl" runat="server" Font-Bold="true" Text="Correct Words: " Font-Names="Lucida Sans Unicode" Font-Size="Large"> </asp:Label>
                    <asp:Label ID="countNoLbl" runat="server" Font-Italic="true" Font-Size="Large"> </asp:Label>
                </div>
                <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br />
                </div>
            </div>

            <div class="col-md-6"> 
                <div class="well">
                <br /> <br /> <br />
                <div class="text-center">
                    <asp:Label ID="wordLbl" runat="server" Font-Size="XX-Large"></asp:Label>  
                    <br /> <br /> <br />
                    <asp:Label ID="hintTextLbl" runat="server" Font-Bold="True" Text="Hint: " Font-Italic="True" Font-Names="Lucida Sans Unicode" Font-Size="Large"></asp:Label>
                    <asp:Label ID="hintLbl" runat="server" Font-Italic="True" Font-Size="Large"></asp:Label>
                    <br /> <br />
                    <asp:Label ID="incorrectLbl" runat="server" Text="Incorrect Letters:" Font-Italic="True" Font-Bold="True" Font-Names="Lucida Sans Unicode" Font-Size="Large"></asp:Label>
                    <asp:Label ID="lettersLbl" runat="server" Font-Italic="True" Font-Size="Large"></asp:Label>
                    <br /> <br /> <br />
                    <asp:Button ID="qBtn" runat="server" Text="Q" OnClick="letterGuessed" Height="40px" Width="40px" />
                    <asp:Button ID="wBtn" runat="server" Text="W" OnClick="letterGuessed" Height="40px" Width="40px"/>
                    <asp:Button ID="eBtn" runat="server" Text="E" OnClick="letterGuessed" Height="40px" Width="40px"/>
                    <asp:Button ID="rBtn" runat="server" Text="R" OnClick="letterGuessed" Height="40px" Width="40px"/>
                    <asp:Button ID="tBtn" runat="server" Text="T" OnClick="letterGuessed" Height="40px" Width="40px"/>
                    <asp:Button ID="yBtn" runat="server" Text="Y" OnClick="letterGuessed" Height="40px" Width="40px"/>
                    <asp:Button ID="uBtn" runat="server" Text="U" OnClick="letterGuessed" Height="40px" Width="40px"/>
                    <asp:Button ID="iBtn" runat="server" Text="I" OnClick="letterGuessed" Height="40px" Width="40px"/>
                    <asp:Button ID="oBtn" runat="server" Text="O" OnClick="letterGuessed" Height="40px" Width="40px"/>
                    <asp:Button ID="pBtn" runat="server" Text="P" OnClick="letterGuessed" Height="40px" Width="40px"/>
                    <br />
                    <asp:Button ID="aBtn" runat="server" Text="A" OnClick="letterGuessed" Height="40px" Width="40px"/>
                    <asp:Button ID="sBtn" runat="server" Text="S" OnClick="letterGuessed" Height="40px" Width="40px"/>
                    <asp:Button ID="dBtn" runat="server" Text="D" OnClick="letterGuessed" Height="40px" Width="40px"/>
                    <asp:Button ID="fBtn" runat="server" Text="F" OnClick="letterGuessed" Height="40px" Width="40px"/>
                    <asp:Button ID="gBtn" runat="server" Text="G" OnClick="letterGuessed" Height="40px" Width="40px"/>
                    <asp:Button ID="hBtn" runat="server" Text="H" OnClick="letterGuessed" Height="40px" Width="40px"/>
                    <asp:Button ID="jBtn" runat="server" Text="J" OnClick="letterGuessed" Height="40px" Width="40px"/>
                    <asp:Button ID="kBtn" runat="server" Text="K" OnClick="letterGuessed" Height="40px" Width="40px"/>
                    <asp:Button ID="lBtn" runat="server" Text="L" OnClick="letterGuessed" Height="40px" Width="40px"/>
                    <br />
                    <asp:Button ID="zBtn" runat="server" Text="Z" OnClick="letterGuessed" Height="40px" Width="40px"/>
                    <asp:Button ID="xBtn" runat="server" Text="X" OnClick="letterGuessed" Height="40px" Width="40px"/>
                    <asp:Button ID="cBtn" runat="server" Text="C" OnClick="letterGuessed" Height="40px" Width="40px"/>
                    <asp:Button ID="vBtn" runat="server" Text="V" OnClick="letterGuessed" Height="40px" Width="40px"/>
                    <asp:Button ID="bBtn" runat="server" Text="B" OnClick="letterGuessed" Height="40px" Width="40px"/>
                    <asp:Button ID="nBtn" runat="server" Text="N" OnClick="letterGuessed" Height="40px" Width="40px"/>
                    <asp:Button ID="mBtn" runat="server" Text="M" OnClick="letterGuessed" Height="40px" Width="40px"/>
                    <br /> <br /> <br />
                    <asp:Button ID="nextBtn" runat="server" Text="Next" OnClick="nextBtn_Click" Height="40px" Width="100px" />
                    &nbsp;&nbsp;
                    <asp:Button ID="newBtn" runat="server" Text="Restart" OnClick="newBtn_Click" Height="40px" Width="100px" Visible="False" /> 
                    &nbsp;&nbsp;                                      
                    <asp:Button ID="backBtn" runat="server" Text="Back" OnClick="backBtn_Click" Height="40px" Width="100px" />
                    <br /> <br />
                </div>
                </div>
            </div>

            <div class="col-md-3"> 
                <div class="well">
                <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> 
                <div class="text-center">
                    <asp:Label ID="livesLbl" runat="server" Font-Bold="true" Text="Lives Remaining: " Font-Names="Lucida Sans Unicode" Font-Size="Large"> </asp:Label>
                    <asp:Label ID="livesNoLbl" runat="server" Font-Italic="true" Font-Size="Large"> </asp:Label>
                    <br /> <br />
                    <asp:Label ID="multiLbl" runat="server" Font-Bold="true" Text="Guess Multiplier: " Font-Names="Lucida Sans Unicode" Font-Size="Large"> </asp:Label>
                    <asp:Label ID="multiNoLbl" runat="server" Font-Italic="true" Font-Size="Large"> </asp:Label>
                    <br /> <br />
                    <asp:Label ID="chainLbl" runat="server" Font-Bold="true" Text="Chain Multiplier: " Font-Names="Lucida Sans Unicode" Font-Size="Large"> </asp:Label>
                    <asp:Label ID="chainNoLbl" runat="server" Font-Italic="true" Font-Size="Large"> </asp:Label>
                </div>
                <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br />
                </div>   
            </div> 
        </div> 
                   
        <div class="row">
            <div class="col-md-3"> </div>
            <div class="col-md-6">
                <div class="well">
                <div class="text-center">
                    <asp:Label ID="scoreLbl" runat="server" Font-Bold="True" Font-Names="Lucida Sans Unicode" Font-Size="X-Large"></asp:Label>
                </div>
                </div>
            </div>
            <div class="col-md-3"> </div>
        </div>

    </div>

    </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Timer ID="timer" runat="server" Interval="1000" OnTick="timer_Tick"> </asp:Timer>   

</asp:Content>

