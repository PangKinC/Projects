<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TopContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server"> 
    <div class="row">
        <div class="col-md-4">
            <br /> <br />
            <div class="well"> <br /> 
                <div class="text-center"> 
                    <strong> Game Objectives </strong>
                    <br /> <br />
                    I created this project as a rendition on the popular game: Hangman. <br />
                    After starting the game, you will go through a list of words in which you have a set amount of time 
                    to guess what it is through clicking the relevant letter button. <br />
                    The goal by the end of the game is to try get the highest score possible, this is calculated through various multipliers
                    (wrong letter guesses, how long it took for you guess the word etc.)
                    <br /> <br />
                    <strong> Difficulty Settings </strong>
                    <br /> <br />
                    Changing the difficulty settings affect the following things:
                </div>
                <div class="col-md-11 col-md-offset-1">
                    <ul> <br />
                        <li> Number of words in subject. </li>
                        <li> Time allocated for each word. </li>
                        <li> Correctly guessed word score. </li>
                        <li> Multipliers for scoring. </li>
                        <li> Amount of time decreased on wrong guess. </li>
                        <li> Number of lives & pauses. </li>
                    </ul>
                    <br /> 
                </div>                                              
                <div class="text-center"> 
                    <strong> Game Subject </strong> 
                    <br /> <br />
                    You currently have a selection of three subjects to choose from. <br />
                    Please be aware that after clicking the button you will be immediately transferred and the game will start!
                    <br /> 
                </div>
            </div>
        </div>

        <div class="col-md-4"> 
            <br /> <br />
            <div class="well"> <br /> 
                <div class="text-center">
                <strong> Starting the Game </strong>
                <br /> <br />
                 Follow the below steps to configure and start the game!
                <br /> <br /> <br />
                <strong>Step 1</strong> -  Choose a difficulty setting (Only 1 is applicable).
                <br /> <br /> <br />
                <asp:CheckBox ID="easyTick" runat="server" OnCheckedChanged="easyTick_CheckedChanged" Text="Easy" />
                &nbsp;&nbsp;&nbsp;
                <asp:CheckBox ID="normTick" runat="server" OnCheckedChanged="normTick_CheckedChanged" Text="Normal" />
                &nbsp;&nbsp;&nbsp;
                <asp:CheckBox ID="hardTick" runat="server" OnCheckedChanged="hardTick_CheckedChanged" Text="Hard" />
                <br /> <br /> <br />  
                <strong>Step 2</strong> -  Choose a subject for game, you will be transferred after the button click.
                <br /> <br /> <br /> 
                <asp:Button ID="prgBtn" runat="server" Height="40px" OnClick="prgBtn_Click" Text="Programming" Width="140px" Enabled="False" />
                <br /> <br /> 
                <asp:Button ID="gnlBtn" runat="server" Height="40px" OnClick="gnlBtn_Click" Text="General" Width="140px" Enabled="False" />
                <br /> <br />
                <asp:Button ID="vgmBtn" runat="server" Height="40px" OnClick="vgmBtn_Click" Text="Video Games" Width="140px" Enabled="False" />
                <br /> <br /> <br /> 
                <strong>Step 3</strong> -  Good luck getting the highest score you can!
                <br /> <br /> <br /> <br /> <br />
                </div>
            </div>
        </div>

        <div class="col-md-4"> 
            <br /> <br />
            <div class="well"> <br /> 
                <div class="text-center">
                    <strong> Hints & Tricks </strong>
                    <br /> <br /> 
                    Because there's a lot of text on the screen at once, 
                    here are some description on the things one shold keep an eye out for:
                    <ol> <br />
                        <li> <strong>Keyboard input</strong> is implemented and recommended, 
                            you can press the respective letter key to trigger the event instead of clicking the mouse.</li>
                        <li> For an extra dimension of challenge, 
                            getting a wrong letter guess will <strong>DECREASE</strong> the amount of time remaining.</li>
                        <li> The <strong>score</strong> per word you get varies, 
                            depending on how long it took for you to guess it and the difficulty setting chosen.  </li>
                        <li> First of two multipliers gets up to <strong>+50%</strong> added to your score, 
                            to maintain this multiplier one must minimize the amount of wrong letter guesses for the word.</li>
                        <li> The second multiplier is for getting consecutive correct word guesses in the allocated time, 
                            quicker you are higher the multiplier (up to <strong>+100%</strong>) is. </li>
                        <li> Finally at the end of the game, you have a chance to get <strong>BONUS</strong> points added to your final score.
                            This all depends on how much words in the list was guessed correctly, 
                            with the bonus being higher the less incorrect words there was. </li>
                        <li> You get a limited amount of <strong>pauses</strong> in a single game, depending on what difficulty was chosen. </li>
                    </ol>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
         $(document).ready(function () {
            $('input[id*="<%=easyTick.ClientID%>"]').change(function () {
                var currentRow = $(this).parents('tr')[0];
                $('input[id*="<%=normTick.ClientID%>"]', currentRow).attr('disabled', this.checked);
                });

            $('input[id*="<%=easyTick.ClientID%>"]').change(function () {
                var currentRow = $(this).parents('tr')[0];
                $('input[id*="<%=hardTick.ClientID%>"]', currentRow).attr('disabled', this.checked);
                });

            $('input[id*="<%=normTick.ClientID%>"]').change(function () {
                var currentRow = $(this).parents('tr')[0];
                $('input[id*="<%=easyTick.ClientID%>"]', currentRow).attr('disabled', this.checked);
            });

            $('input[id*="<%=hardTick.ClientID%>"]').change(function () {
                var currentRow = $(this).parents('tr')[0];
                $('input[id*="<%=easyTick.ClientID%>"]', currentRow).attr('disabled', this.checked);
            });

            $('input[id*="<%=hardTick.ClientID%>"]').change(function () {
                var currentRow = $(this).parents('tr')[0];
                $('input[id*="<%=normTick.ClientID%>"]', currentRow).attr('disabled', this.checked);
            });

            $('input[id*="<%=normTick.ClientID%>"]').change(function () {
                var currentRow = $(this).parents('tr')[0];
                $('input[id*="<%=hardTick.ClientID%>"]', currentRow).attr('disabled', this.checked);
                });
         });

        $("#<%=easyTick.ClientID%>").click(function () {
            if ($(this).is(":checked")) {
                $("#<%=gnlBtn.ClientID%>").removeAttr("disabled");            
                $("#<%=prgBtn.ClientID%>").removeAttr("disabled");   
                $("#<%=vgmBtn.ClientID%>").removeAttr("disabled");   
            } else {
                $("#<%=gnlBtn.ClientID%>").attr("disabled", "disabled");
                $("#<%=prgBtn.ClientID%>").attr("disabled", "disabled");
                $("#<%=vgmBtn.ClientID%>").attr("disabled", "disabled");
            }
        });
        
        $("#<%=normTick.ClientID%>").click(function () {
            if ($(this).is(":checked")) {
                $("#<%=gnlBtn.ClientID%>").removeAttr("disabled");            
                $("#<%=prgBtn.ClientID%>").removeAttr("disabled");   
                $("#<%=vgmBtn.ClientID%>").removeAttr("disabled");   
            } else {
                $("#<%=gnlBtn.ClientID%>").attr("disabled", "disabled");
                $("#<%=prgBtn.ClientID%>").attr("disabled", "disabled");
                $("#<%=vgmBtn.ClientID%>").attr("disabled", "disabled");
            }
        });

        $("#<%=hardTick.ClientID%>").click(function () {
            if ($(this).is(":checked")) {
                $("#<%=gnlBtn.ClientID%>").removeAttr("disabled");            
                $("#<%=prgBtn.ClientID%>").removeAttr("disabled");   
                $("#<%=vgmBtn.ClientID%>").removeAttr("disabled");   
            } else {
                $("#<%=gnlBtn.ClientID%>").attr("disabled", "disabled");
                $("#<%=prgBtn.ClientID%>").attr("disabled", "disabled");
                $("#<%=vgmBtn.ClientID%>").attr("disabled", "disabled");
            }
        });
     </script>

</asp:Content>




