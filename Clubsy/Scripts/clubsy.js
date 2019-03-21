$(function () {

    var ajaxFilterChange = _.debounce(
        function () {
            var $input = $(this);
            var $form = $input.parent();

            var options = {
                url: $form.attr("action"),
                type: $form.attr("method"),
                data: $form.serialize()
            };

            $.ajax(options).done(function (data) {
                var $target = $($form.attr("data-ajax-update"));
                var $newHtml = $(data);
                $target.replaceWith($newHtml);
            });

            return false;
        },
        300,
        { leading: true, trailing: true, maxWait: 1000 }
    );

    var getPage = function () {
        var $a = $(this);

        var options = {
            url: $a.attr("href"),
            data: $("form").serialize(),
            type: "get"
        };

        $.ajax(options).done(function (data) {
            var target = $a.parents("div.pagedList").attr("data-clubsy-target");
            $(target).replaceWith(data);
        });

        return false;
    };

    $("input[data-clubsy-filter='club']").on("keyup", ajaxFilterChange);
    $(".body-content").on("click", ".pagedList a", getPage);
});