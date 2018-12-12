
// for highlight the ACTIVE FILE in gallery
var divImageName = $(".images .boxFooter__FileName");
var divSoundName = $(".sounds .boxFooter__FileName");
for (var a = 0; a < divImageName.length; a++) {
    var elemImg, elemImgLi,
        w = divImageName[a].textContent;
    if (w === imageName) {
        elemImg = divImageName[a];
        elemImg.style.cssText = "font-weight: bold; color: #eeeeee;";
        elemImgLi = elemImg.parentNode.parentNode;
        elemImgLi.style.cssText = "border-color: #eeeeee";
    }
}
for (var a = 0; a < divSoundName.length; a++) {
    var elemSound, elemSoundLi,
        w = divSoundName[a].textContent;
    if (w === soundName) {
        elemSound = divSoundName[a];
        elemSound.style.cssText = "font-weight: bold; color: #eeeeee;";
        elemSoundLi = elemSound.parentNode.parentNode;
        elemSoundLi.style.cssText = "border-color: #eeeeee";
    }
}

// For PLAY/STOP ringtones
function playSound(fileName, self) {
    stopSound();
    $(".playSound").css("display", "inline-block");
    $(".stopSound").css("display", "none");
    $(self).css("display", "none");
    $(self).siblings().css("display", "inline-block");
    $("body").append(
        '<audio class="sound-player" id="donatRington" autoplay="autoplay" style="display:none;">' +
        '<source src="' + fileName + '" />' +
        '<embed src="' + fileName + '" hidden="true" autostart="true" loop="false"/>' + '</audio>');
    $('#donatRington').on('ended',
        function () {
            stopSound();
        });
}
function stopSound() {
    $(".stopSound").css("display", "none");
    $(".playSound").css("display", "inline-block");
    $(".sound-player").remove();
}

// Save Name choosen Image and Sound
function getImgName(name, tab) {
    $("#NewNameImageDon").val(name);
    $("#SelectImageSource").val(tab);
    return true;
}
function getSoundName(name, tab) {
    $("#NewNameSoundDon").val(name);
    $("#SelectSoundSource").val(tab);
    return true;
}


// Border over choosen Image and Sound
$(".galleryList li").click(function () {
    $(this).siblings().css({
        "border-color": "rgb(240, 242, 242)",
        "font-weight": "normal",
        "color": "#333"
    });
    $(this).css({
        "border-color": "#3297DB",
        "font-weight": "bold",
        "color": "#3297DB"
    });
});


// Switch tabs in gallery
$('#imgGallery').click(function () {
    $('#SelectGalleryBox').val('galleryImg');
    $("#modalGallery span.active").removeClass("active");
    $(this).find("span").addClass("active");
    $("#imgGalleryContainer").siblings().css("display", "none");
    $("#imgGalleryContainer").css("display", "flex");
});
$('#soundGallery').click(function () {
    $('#SelectGalleryBox').val('gallerySound');
    $("#modalGallery span.active").removeClass("active");
    $(this).find("span").addClass("active");
    $("#soundGalleryContainer").siblings().css("display", "none");
    $("#soundGalleryContainer").css("display", "flex");
});
$('#imgUpload').click(function () {
    $('#SelectGalleryBox').val('uploadImg');
    $("#modalGallery span.active").removeClass("active");
    $(this).find("span").addClass("active");
    $("#imgUploadContainer").siblings().css("display", "none");
    $("#imgUploadContainer").css("display", "flex");
});
$('#soundUpload').click(function () {
    $('#SelectGalleryBox').val('uploadSound');
    $("#modalGallery span.active").removeClass("active");
    $(this).find("span").addClass("active");
    $("#soundUploadContainer").siblings().css("display", "none");
    $("#soundUploadContainer").css("display", "flex");
});


// MODAL Window  Gallery IMG&SOUND
$('#btnChooseImage').click(function (event) {
    event.preventDefault();
    $('#overlay').fadeIn(400,
        function () {
            $('#modalGallery')
                .css('display', 'block')
                .animate({ opacity: 1, top: '50%' }, 200);
        });
    $("#modalGallery span.active").removeClass("active");
    $("#imgGallery span").addClass("active");
    $("#imgUploadContainer").siblings().css("display", "none");
    $("#imgGalleryContainer").css("display", "flex");
});
$('#btnChooseSound').click(function (event) {
    event.preventDefault();
    $('#overlay').fadeIn(400,
        function () {
            $('#modalGallery')
                .css('display', 'block')
                .animate({ opacity: 1, top: '50%' }, 200);
        });
    $("#modalGallery span.active").removeClass("active");
    $("#soundGallery span").addClass("active");
    $("#soundUploadContainer").siblings().css("display", "none");
    $("#soundGalleryContainer").css("display", "flex");
});
$('#modal_close, #overlay, #modal_cancel').click(function () {
    $('#modalGallery')
        .animate({ opacity: 0, top: '45%' }, 200,
            function () {
                $(this).css('display', 'none');
                $('#overlay').fadeOut(400);
            }
        );
    $("#NewNameImageDon").val("");
    $("#NewNameSoundDon").val("");
    $('#GalleryVisible').val("");
    $('#SelectImageSource').val("");
    $('#SelectSoundSource').val("");
    $('#imgGalleryContainer, #soundGalleryContainer, #imgUploadContainer, #soundUploadContainer').css('display', 'none');
});

$('#modal_ok').click(function () {
    $('#imgGalleryContainer, #soundGalleryContainer, #imgUploadContainer, #soundUploadContainer').css('display', 'none');
    $('#modalGallery')
        .animate({ opacity: 0, top: '45%' }, 200,
            function () {
                $(this).css('display', 'none');
                $('#overlay').fadeOut(400);
            }
        );
    $('#GalleryVisible').val("");
    var newNameImage = $("#NewNameImageDon").val();
    var newSourceImage = $("#SelectImageSource").val();
    var newNameSound = $("#NewNameSoundDon").val();
    if (newNameImage !== "") {
        $("#NameImageDon").val(newNameImage);
        $("#nameChoosenImage").text(newNameImage);
        $("#animationExample").attr("src", function () {
            return "/gallery/" + newSourceImage + "/" + newNameImage;
        });
        $("#animationExample").attr("title", function () {
            return newNameImage;
        });
    } else {
        $("#NameImageDon").val(imageName);
        $("#nameChoosenImage").text(imageName);
    }
    if (newNameSound !== "") {
        $("#NameSoundDon").val(newNameSound);
        $("#nameChoosenSound").text(newNameSound);
    } else {
        $("#NameSoundDon").val(soundName);
        $("#nameChoosenSound").text(soundName);
    }
});


// select box in Modal Gallery after uploadFiles-POST
if (galleryVisible === true) {
    $('#overlay').fadeIn(0,
        function () {
            $('#modalGallery')
                .css('display', 'block')
                .animate({ opacity: 1, top: '50%' }, 0);
        });
}
if (selectGalleryBox === "galleryImg") {
    $('#imgGallery').click();
}
if (selectGalleryBox === "gallerySound") {
    $('#soundGallery').click();
}
if (selectGalleryBox === "uploadImg") {
    $('#imgUpload').click();
}
if (selectGalleryBox === "uploadSound") {
    $('#soundUpload').click();
}


// ADD FILES
var wrapper = $(".file_upload"),
    inp = wrapper.find("input"),
    btn = wrapper.find("button#uploadBtn"),
    push = wrapper.find("button#pushFile"),
    lbl = wrapper.find("div#fileName");
btn.focus(function () {
    wrapper.addClass("focus");
}).blur(function () {
    wrapper.removeClass("focus");
});
btn.add(lbl).click(function () {
    inp.click();
});
var file_api = (window.File && window.FileReader && window.FileList && window.Blob) ? true : false;
inp.change(function () {
    var file_name;
    if (file_api && inp[0].files[0])
        file_name = inp[0].files[0].name;
    else
        file_name = inp.val().replace("C:\\somepath\\", '');
    if (!file_name.length)
        return;
    if (lbl.is(":visible")) {
        lbl.text(file_name);
        btn.text("Загрузка файла");
    } else
        btn.text(file_name);
    // for Get Type file
    var file = $("#files")[0].files[0],
        ext = "не определилось",
        parts = file.type.split('/');
    if (parts.length > 1) ext = parts.shift();
    console.log("MIME тип: ", ext);
    $("#TypeUploadFile").val(ext);

    push.click();
}).change();

