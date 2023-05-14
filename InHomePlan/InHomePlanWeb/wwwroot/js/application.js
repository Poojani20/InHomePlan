const prevBtns = document.querySelectorAll(".btn-prev");
const nextBtns = document.querySelectorAll(".btn-next");
const progress = document.getElementById("progress");
const formSteps = document.querySelectorAll(".form-step");
const progressSteps = document.querySelectorAll(".progress-step");

let formStepsNum = 0;

const customErrorMessages = {
    FirstName: 'Please enter your first name.',
    LastName: 'Please enter your last name.',
    email: 'Please enter a valid email address.',
    // Add more field names and error messages as needed
};

function setCustomErrorMessages() {
    const formInputs = document.querySelectorAll('input, select, textarea');
    formInputs.forEach((input) => {
        const fieldName = input.name;
        if (fieldName in customErrorMessages) {
            input.setCustomValidity(customErrorMessages[fieldName]);
        }
    });
}

 //setCustomErrorMessages();

nextBtns.forEach((btn) => {
    btn.addEventListener("click", (e) => {
        e.preventDefault(); // Prevent the default form submission

        if (validateForm()) { // Validate the form before proceeding
            formStepsNum++;
            updateFormSteps();
            updateProgressbar();
            console.log("form is valid!")
        }
        else {
            console.log("form is invalid!")
        }
    });
});

prevBtns.forEach((btn) => {
    btn.addEventListener("click", () => {
        formStepsNum--;
        updateFormSteps();
        updateProgressbar();
    });
});

function updateFormSteps() {
    formSteps.forEach((formStep) => {
        formStep.classList.contains("form-step-active") &&
            formStep.classList.remove("form-step-active");
    });

    formSteps[formStepsNum].classList.add("form-step-active");
}

function updateProgressbar() {
    progressSteps.forEach((progressStep, idx) => {
        if (idx < formStepsNum + 1) {
            progressStep.classList.add("progress-step-active");
        } else {
            progressStep.classList.remove("progress-step-active");
        }
    });

    const progressActive = document.querySelectorAll(".progress-step-active");

    progress.style.width =
        ((progressActive.length - 1) / (progressSteps.length - 1)) * 100 + "%";
}

function validateForm() {
    // Get the current form step
    const currentFormStep = formSteps[formStepsNum];

    // Get the form inputs within the current form step
    const formInputs = currentFormStep.querySelectorAll("input, select, textarea");

    console.log(formInputs);

    // Check if any of the inputs is invalid
    let isValid = true;
    formInputs.forEach((input) => {
        if (!input.checkValidity()) {
            input.classList.add("is-invalid"); // Add "is-invalid" class to inputs with invalid values
            isValid = false;

            // Get the error message element associated with the input
            const errorMessage = input.nextElementSibling;

            // Set the error message text
            errorMessage.innerText = input.validationMessage;
        } else {
            input.classList.remove("is-invalid"); // Remove "is-invalid" class from valid inputs

            // Clear the error message
            const errorMessage = input.nextElementSibling;
            errorMessage.innerText = "";
        }
    });

    return isValid;
}

