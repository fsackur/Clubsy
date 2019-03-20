$(function () {

    var ajaxFormSubmit = function () {
        var $form = $(this);

        var options = {
            url: $form.attr("action"),
            type: $form.attr("method"),
            data: $form.serialize()
        };

        $.ajax(options).done(function (data) {
            var $target = $($form.attr("data-ajax-update"));
            var $newHtml = $(data);
            $target.replaceWith($newHtml);
            $newHtml.effect("highlight");
        });

        return false;
    };

    var createAutoComplete = function () {
        var $input = $(this);   // 'this' gets each matching DOM element matching "input[data_clubsy_autocomplete_action]"

        var options = {
            source: $input.attr("data_clubsy_autocomplete_action")
        };

        $input.autocomplete(options);
    };

    $("form[data-clubsy-filter='club']").submit(ajaxFormSubmit);
    $("input[data_clubsy_autocomplete_action]").each(createAutoComplete);
});