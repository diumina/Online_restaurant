$(document).ready(function () {
	bindSliderButtons();
	initAutoSlider();
});

function initAutoSlider() {
	setInterval(moveSliderForward, 4000);
}

function bindSliderButtons() {
	$(".slider-arrow-right").click(moveSliderForward);
	$(".slider-arrow-left").click(moveSliderBackward);
}

function moveSliderForward() {
	let currentImage = $(".slider-slide-active");
	let currentImageIndex = $(".slider-slide-active").index();
	let nextImageIndex = currentImageIndex + 1;
	let nextImage = $(".slider-slide").eq(nextImageIndex);

	currentImage.fadeOut(700, () => {
		currentImage.removeClass("slider-slide-active");

		if (nextImageIndex == ($(".slider-slide:last").index() + 1)) {
			$(".slider-slide").eq(0).fadeIn(700);
			$(".slider-slide").eq(0).addClass("slider-slide-active");
		} else {
			nextImage.fadeIn(700);
			nextImage.addClass("slider-slide-active");
		}
	});
}

function moveSliderBackward() {
	let currentImage = $(".slider-slide-active");
	let currentImageIndex = $(".slider-slide-active").index();
	let prevImageIndex = currentImageIndex - 1;
	let prevImage = $(".slider-slide").eq(prevImageIndex);

	currentImage.fadeOut(500, () => {
		currentImage.removeClass("slider-slide-active");

		prevImage.fadeIn(500);
		prevImage.addClass("slider-slide-active");
	});
}


