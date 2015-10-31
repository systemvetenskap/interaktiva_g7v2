var mins = 1; //1 minut tills vidare..
var secs = mins * 60;

var counter = setInterval(timer, 1000); //1000 gör att den körs varje sekund

function timer()
{
    mins = Math.floor(secs/60)
    secs = secs - 1;
    if (secs <= 0)
    {
        clearInterval(counter);
        //counter slut, gör något här
        return;
    }

    document.getElementById("timer").innerHTML = mins +" minuter " +secs + " sekunder";
}

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