$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip({
        trigger: 'hover',
        html: true,
        container: 'body',
        'placement': function (tt, trigger) {
            var $trigger = $(trigger);
            var windowWidth = $(window).width();

            var xs = $trigger.attr('data-placement-xs');
            var sm = $trigger.attr('data-placement-sm');
            var md = $trigger.attr('data-placement-md');
            var lg = $trigger.attr('data-placement-lg');
            var general = $trigger.attr('data-placement');

            return (windowWidth >= 1200 ? lg : undefined) ||
                (windowWidth >= 992 ? md : undefined) ||
                (windowWidth >= 768 ? sm : undefined) ||
                xs ||
                general ||
                "top";
        }
    });
});