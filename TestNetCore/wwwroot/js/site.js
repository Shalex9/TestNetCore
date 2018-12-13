function showInfo() {
    if ($(".glyphicon").hasClass("glyphicon-eye-open")) {
        $(".glyphicon").removeClass("glyphicon-eye-open");
        $(".glyphicon").addClass("glyphicon-eye-close");
        $(".showInfoBtn").text("Скрыть описание");
    } else {
        $(".glyphicon").removeClass("glyphicon-eye-close");
        $(".glyphicon").addClass("glyphicon-eye-open");
        $(".showInfoBtn").text("Показать описание");
    }
    $(".showInfoText").toggleClass("display-none");
}