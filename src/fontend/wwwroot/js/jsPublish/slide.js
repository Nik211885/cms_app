document.addEventListener("DOMContentLoaded", () => {
    // Search ===========================================================================
    const toggleSearch = document.getElementById("header-nav-search");
    const formSearch = toggleSearch.querySelector("form");

    toggleSearch.addEventListener("click", function () {
        formSearch.classList.toggle("active");
    });
    formSearch.addEventListener("click", function (event) {
        event.stopPropagation();
    });
    // show - hide header-nav-link on max-width: 992px ==================================
    const toggleOpen = document.querySelector(".header-nav-toggle-open");
    const toggleClose = document.querySelector(".header-nav-toggle-close");
    const navLinks = document.querySelector(".header-nav-links");

    toggleOpen.addEventListener("click", function () {
        navLinks.classList.add("active");
        toggleOpen.classList.remove("active");
        toggleClose.classList.add("active");
    });

    toggleClose.addEventListener("click", function () {
        navLinks.classList.remove("active");
        toggleClose.classList.remove("active");
        toggleOpen.classList.add("active");
    });
    // slide =============================================================================
    const slideContentList = document.querySelectorAll(".slide-content");
    const prevBtn = document.getElementById("prevBtn");
    const nextBtn = document.getElementById("nextBtn");
    const indicatorsContainer = document.getElementById("indicators");
    let currentIndex = 0;
    const totalSlides = slideContentList.length;

    function renderIndicators() {
        indicatorsContainer.innerHTML = "";
        slideContentList.forEach((_, index) => {
            const indicator = document.createElement("button");
            indicator.classList.add("slide-indicator");
            if (index === 0) indicator.classList.add("active");
            indicator.addEventListener("click", () => goToSlide(index));
            indicatorsContainer.appendChild(indicator);
        });
    }

    function updateSlide(newIndex) {
        const currentSlide = slideContentList[currentIndex];
        const nextSlide = slideContentList[newIndex];

        const currentSlideImg = currentSlide.querySelector(".slide-content-img");
        const nextSlideImg = nextSlide.querySelector(".slide-content-img");

        const currentText = currentSlide.querySelector(
            ".slide-content-title-container"
        );
        const nextText = nextSlide.querySelector(".slide-content-title-main");

        const nextButton = nextSlide.querySelector(
            ".slide-content-title-action button"
        );

        // Thêm lớp 'hidden' vào ảnh, text và button hiện tại để kích hoạt animation ẩn dần
        currentSlideImg.classList.add("hidden");
        currentText.classList.add("hidden");

        // Sau khi animation ẩn dần kết thúc, chuyển đổi slide
        setTimeout(() => {
            currentSlide.classList.remove("active");
            currentSlideImg.classList.remove("hidden");
            currentText.classList.remove("hidden");

            currentIndex = newIndex;
            nextSlide.classList.add("active");

            // Reset và thêm lại animation cho ảnh, text và button hiện tại
            nextSlideImg.style.animation = "none";
            nextText.style.animation = "none";
            nextButton.style.animation = "none";

            nextSlideImg.offsetHeight; // Trigger reflow
            nextText.offsetHeight; // Trigger reflow
            nextButton.offsetHeight; // Trigger reflow

            nextSlideImg.style.animation = "";
            nextText.style.animation = "";
            nextButton.style.animation = "";

            // Update indicators
            const indicators =
                indicatorsContainer.querySelectorAll(".slide-indicator");
            indicators.forEach((indicator, i) => {
                indicator.classList.toggle("active", i === newIndex);
            });
        }, 800); // Thời gian trùng với thời gian của animation fadeOutSlideImg
    }

    function nextSlide() {
        const newIndex = (currentIndex + 1) % totalSlides;
        updateSlide(newIndex);
    }

    function prevSlide() {
        const newIndex = (currentIndex - 1 + totalSlides) % totalSlides;
        updateSlide(newIndex);
    }

    function goToSlide(index) {
        updateSlide(index);
    }

    nextBtn.addEventListener("click", nextSlide);
    prevBtn.addEventListener("click", prevSlide);

    renderIndicators();
    setInterval(nextSlide, 15000);
});
