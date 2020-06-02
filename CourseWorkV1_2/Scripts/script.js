// change rate page
$('.myRate').on('click', function () {
    $('#container1').show();
    $('#container').hide();
});
$('.auction').on('click', function () {
    $('#container1').hide();
    $('#container').show();
});


//function
$('.top_users-select').on('click', function () {
    $('.top_users-select_dropdown').slideToggle();
});

//add photo
function readURL(input) {

    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#image').attr('src', e.target.result);
        };

        reader.readAsDataURL(input.files[0]);
    }
}

$("#imgInput").change(function () {
    readURL(this);
});

//timer
function getTimeRemaining(endtime) {
    var t = Date.parse(endtime) - Date.parse(new Date());
    var minutes = Math.floor((t / 1000 / 60) % 60);
    var hours = Math.floor((t / (1000 * 60 * 60)) % 24);
    // var days = Math.floor(t / (1000 * 60 * 60 * 24));
    return {
        'total': t,
        // 'days': days,
        'hours': hours,
        'minutes': minutes,
    };
}

function initializeClock(id, endtime) {
    var clock = document.getElementById(id);
    // var daysSpan = clock.querySelector('.days');
    var hoursSpan = clock.querySelector('.hours');
    var minutesSpan = clock.querySelector('.minutes');

    function updateClock() {
        var t = getTimeRemaining(endtime);

        // daysSpan.innerHTML = t.days;
        hoursSpan.innerHTML = ('0' + t.hours).slice(-2);
        minutesSpan.innerHTML = ('0' + t.minutes).slice(-2);

        if (t.total <= 0) {
            clearInterval(timeinterval);
        }
    }

    updateClock();
    var timeinterval = setInterval(updateClock, 1000);
}


var deadline = new Date(Date.parse(new Date()) + 15 * 24 * 60 * 60 * 1000); // for endless timer
initializeClock('countdown', deadline);

//timer for rate
initializeClock('countdown1', deadline);
initializeClock('countdown2', deadline);
initializeClock('countdown3', deadline);
initializeClock('countdown4', deadline);
initializeClock('countdown5', deadline);
initializeClock('countdown6', deadline);
initializeClock('countdown7', deadline);
initializeClock('countdown8', deadline);
initializeClock('countdown9', deadline);
initializeClock('countdown10', deadline);
initializeClock('countdown11', deadline);
initializeClock('countdown12', deadline);

//param
// var params = window
//     .location
//     .search
//     .replace('?','')
//     .split('&')
//     .reduce(
//         function(p,e){
//             var a = e.split('=');
//             p[ decodeURIComponent(a[0])] = decodeURIComponent(a[1]);
//             return p;
//         },
//         {}
//     );
//
// if (params.length) {
//     $("input[name=name]").html(params['name'])
//     $("input[name=group]").html(params['group'])
//     $("input[name=rate]").html(params['rate'])
// }
