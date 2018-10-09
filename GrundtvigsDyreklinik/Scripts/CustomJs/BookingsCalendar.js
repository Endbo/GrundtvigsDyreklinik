


// datepicker init start...
var selectedDate = new Date();
var selectedDateE = new Date();
var selectedEvent = null;
$(document).ready(function () {
    $('#datepicker').datetimepicker({
        dateFormat: "dd/M/yy",
        timeFormat: 'HH:mm',
        changeMonth: true,
        changeYear: true,
        stepMinute: 15,
        minTime: '09:00',
        maxTime: '18:00',
        inline: true,

        onSelect: function (dateText, inst) {
            var date = new Date(dateText);
            $('#calendar').fullCalendar('gotoDate', date);
            $('#calendar').fullCalendar('select', date);
            selectedDate = date;
        }
    });
});
// Datepicker init end...

//edit Datepicker start 
$(document).ready(function () {
    $('#StartTime').datetimepicker({
        dateFormat: "dd/M/yy",
        timeFormat: 'HH:mm',
        changeMonth: true,
        changeYear: true,
        stepMinute: 15,
        minTime: '09:00',
        maxTime: '18:00',
        inline: true,

        onSelect: function (dateText, inst) {
            var date = new Date(dateText);
            selectedDate = date;
        }
    });  
});
//edit Datepicker end 

//Fullcalendar init Start...

$(document).ready(function () {

    $('#calendar').fullCalendar({
        //theme: true,
        contentHeight: 400,
        defaultDate: new Date(),
        header: {
            left: 'prev,next, today',
            center: 'title',
            right: 'month, agenda, basicWeek, basicDay,'
        },
        defaultView: 'month',
        timeFormat: 'H(:mm)',
        allDayText: 'GrundtvigsDyreklinik',
        displayEventEnd: userName,
        title: true,
        eventLimit: 4,
        firstDay: 1,
        slotLabelFormat: 'HH:mm',
        slotEventOverlap: false,
        maxTime: '18:00:00',
        minTime: '09:00:00',

        businessHours: [
            {
                dow: [1, 2, 3, 4, 5],
                start: '09:00',
                end: '18:00'
            }
        ],

        editable: false,
        events: "/Bookings/GetBookings/",

        eventClick: function (calEvent, jsEvent, view) {
            selectedEvent = calEvent;
            
            if (calEvent.userName == userName)
            {
                var $description = $('<div/>');
                $description.append($('<p/>').html('<b>Pet: <b/>' + calEvent.petName));
                $description.append($('<p/>').html('<b>Behandling: <b/>' + calEvent.treatmentName));
                $description.append($('<p/>').html('<b>Starttidspunkt: <b/>' + calEvent.start.format("DD-MM-YYYY HH:mm ")));
                $description.append($('<p/>').html('<b>Slutttidspunkt: <b/>' + calEvent.end.format("DD-MM-YYYY HH:mm ")));
                $('#DetailModal #pDetails').empty().html($description);
                $('#DetailModal').modal();
            }
            else {
                alert('Du kan kun se dine egne bookings');

            }
        }       
    });
});
//Fullcalendar init end..
//editFunction
$('#btnEdit').click(function () {
    openAddEditForm();
});

//deleteFunction
$('#btnDelete').click(function () {

    if (selectedEvent !== null && confirm('are you sure')) {
        $.ajax({
            type: "POST",
            url: '/Bookings/DeleteBooking/',
            data: { 'eventID': selectedEvent.bookingID },
            success: function (data) {
                if (data.status) {
                    $('#calendar').fullCalendar('refetchEvents');
                    $('#DetailModal').modal('hide');
                    alert('Tiden er blevet slettet');

                }
            },
            error: function () {
                alert('Kunne ikke slette');
            }
        });
    }
});

//Edit and save getting data function
$('#btnEditSave').click(function () {

    var c = $('#StartTime').val();
    var StartTime = moment(c).format('YYYY-MM-DD[T]HH:mm:ss');
    var testDate = new Date(StartTime).toISOString();

    if (validateDate(testDate)!=true) {

        var data = {
            ID: $('#ID').val(),
            TreatmentID: $('#tid').val(),
            PetID: $('#pid').val(),
            StartTime: StartTime,
            EndTime: StartTime
        };
        params = jQuery.param(data);

        SaveBooking(data);
        $('#EditSaveModal').modal('hide');
    }
    else {

        alert('Du kan kun booke fremme i tiden og heller ikke om weekenden');
    }
});

//Add data to save
$('#add').click(function () {

    var TreatmentID = $('#TreatmentID').val();
    var PetID = $('#PetID').val();
    var StartTime = selectedDate.toISOString();

    if (validateDate(StartTime)!=true) {
        
        var data = {
            StartTime: StartTime,
            EndTime: StartTime,
            PetID: PetID,
            TreatmentID: TreatmentID
        };

        params = jQuery.param(data);
        SaveBooking(data);
    }
    else {
        alert('Du kan kun booke fremme i tiden og heller ikke om weekenden');
    }
    
    
});

//Edit form
function openAddEditForm() {
    if (selectedEvent !== null) {
        $('#ID').val(selectedEvent.bookingID);
        $('#TreatmentID').val(selectedEvent.treatmentName);
        $('#PetID').val(selectedEvent.petName);
        $('#StartTime').val(selectedEvent.start);
       
    }
    $('#DetailModal').modal('hide');
    $('#EditSaveModal').modal();
}

//save and edit data
function SaveBooking(data) {
    $.ajax({
        url: '/Bookings/SaveAndEditBookings/',
        type: "POST",
        data: params,
        success: function (msg) {
            if (msg.status) {
                $('#calendar').fullCalendar('refetchEvents');              
                selectedEvent = null;
                
                alert('Din tid er blevet oprettet i systemet');
            }
            else if(msg.status == false)
            {
                alert('Den tid er optaget');
            }
        },
        error: function () {
            alert('Server fejl');
        }
    });
}

//validate date 
function validateDate(dates) {

    dates = new Date(dates);
    var now = new Date();
    var todayAtMidn = new Date(now.getFullYear(), now.getMonth(), now.getDate());
    var datesAtMidn = new Date(dates.getFullYear(), dates.getMonth(), dates.getDate());

    if ((datesAtMidn.getDay() == 0) || (datesAtMidn.getDay() == 6)) {
        return true;
    }
    if (datesAtMidn > todayAtMidn) {
        return false;
    }    

    else {
        return true;
    }
}
