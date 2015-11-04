var run = <%=this.timerVar %>;
if (run = 1)
{
    function timer() {
        var minutes = 30;
        var seconds = 00;
        var counter = setInterval(timer, 1000) //körs varje sekund

        seconds = seconds - 1
        if (seconds <= 0) {
            minutes -= 1;
            seconds += 59;
        }
        if (minutes <= 1) {
            minutes == 1;
            seconds = 0;
            clearInterval(counter);
            alert("Tiden är slut");
            //Kod här när tiden är slut
            return;
        }
        document.getElementById("timer").innerHTML = minutes + " minuter " + seconds + " sekunder";
    }



}





//Timern följer med när man scrollar ner
$(document).ready(function () {
    var top = $('#timerbox').offset().top - parseFloat($('#timerbox').css('marginTop').replace(/auto/, 0));
    $(window).scroll(function (event) {
        var y = $(this).scrollTop();
        if (y >= top) {
            var difference = y - top;
            $('#timerbox').css("top", difference);
        }
    });
});