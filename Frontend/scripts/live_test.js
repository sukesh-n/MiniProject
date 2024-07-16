function LiveQuizz(QuestionDto, OptionsDTO) {
  console.log('LiveQuizz:', QuestionDto);
  const quizzLiveContainer = document.querySelector('.custom-quizz-live-container');
  const customQuizzGenerator = document.querySelector('.custom-quizz-generator');
  
  customQuizzGenerator.style.display = "none";
  quizzLiveContainer.style.display = "block";
  const timerContainer = document.querySelector('.time-board span');
  const questionId = document.querySelector('.question-number span');
  const questionDescription = document.querySelector('.question-description');
  const QuestionDifficulty = document.querySelector('.difficulty-level span');
  const type = document.querySelector('.type span');
  const mcq = document.querySelector('.mcq');
  const optionsContainer = document.querySelector('.options');
  const trueFalse = document.querySelector('.true-false');
  const numerical = document.querySelector('.numerical');
  let next = document.querySelector('.next');
  let previous = document.querySelector('.previous');
  let submit = document.querySelector('.submit');
  let totalTime = Math.floor(0.833333*30);
  let TimeForQuestion = totalTime/QuestionDto.length;
  let currentQuestionIndex = 0; // Track the currently displayed question
  let visitedQuestions = new Set(); 
  let answeredQuestions = new Map(); // Store answered questions and their selected options
  let trueOption, falseOption; 

  previous.addEventListener('click', () => {
    console.log('Previous button clicked');
    if (currentQuestionIndex > 0) {
      handleQuestionClick(currentQuestionIndex - 1); 
    }
  });
  
  next.addEventListener('click', () => {
    console.log('Next button clicked'); 
    if (currentQuestionIndex < QuestionDto.length - 1) {
      handleQuestionClick(currentQuestionIndex + 1); 
    }
  });
  submit.addEventListener('click', () => {

  });
  function displayTimer() {
    if(currentQuestionIndex === 0){
      previous.style.display = "none";
    }
    else{
      previous.style.display = "block";
    }
    if(currentQuestionIndex === QuestionDto.length - 1){
      next.style.display = "none";
    }
    else{
      next.style.display = "block";
    }
    if(answeredQuestions.size === QuestionDto.length){
      submit.style.display = "block";
    }
    else{
      submit.style.display = "none";
    }
    const hours = Math.floor(totalTime / 3600);
    const minutes = Math.floor((totalTime % 3600) / 60);
    const remainingSeconds = totalTime % 60;

    timerContainer.textContent = 
      `${String(hours).padStart(2, '0')}:${String(minutes).padStart(2, '0')}:${String(remainingSeconds).padStart(2, '0')}`;

    // Decrease time
    if (totalTime > 0) {
      totalTime--;
      setTimeout(displayTimer, 1000); // Call this function again in 1 second
    } else {
      quizzLiveContainer.style.filter = "blur(5px)";
      const masterSubmit = document.createElement('button');
      masterSubmit.textContent = "Submit";
      masterSubmit.style.position = "absolute";
      masterSubmit.style.top = "50%";
      masterSubmit.style.left = "50%";
      masterSubmit.style.width = "20vh";
      masterSubmit.style.height = "10vh";
      masterSubmit.style.background = "green";
      masterSubmit.style.transform = "translate(-50%, -50%)";

      // !!! IMPORTANT: Append to a visible parent element !!! 
      document.body.appendChild(masterSubmit); 
    }
  }

  displayTimer();
  displayQuestionCluster(QuestionDto, currentQuestionIndex);
  displayQuestion(currentQuestionIndex); // Display the first question initially

  // Function to display a specific question
  async function displayQuestion(i) {

    currentQuestionIndex = i;
    if (i < QuestionDto.length) {
      currentQuestionIndex = i; // Update the current question index
      const question = QuestionDto[i];
      console.log('QuestionDto:', i);
      questionId.textContent = i + 1;
      questionDescription.textContent = question.questionDescription;
      QuestionDifficulty.textContent = question.difficultyLevel;

      const currentOptions = OptionsDTO.filter(option => option.questionId === question.questionId);
      renderOptions(question.questionType, currentOptions);

      // If the question was previously answered, restore the selected option
      if (answeredQuestions.has(i)) {
        const selectedOption = answeredQuestions.get(i);
        if (question.questionType === "MCQ" || question.questionType === "True/False") {
          const optionInput = document.querySelector(`input[name="question${i}"][value="${selectedOption}"]`);
          if (optionInput) {
            optionInput.checked = true;
          }
        } else if (question.questionType === "Numerical") {
          const numericalInput = document.getElementById('numericalInput');
          if (numericalInput) {
            numericalInput.value = selectedOption;
          }
        }
      }

      visitedQuestions.add(i); // Mark current question as visited
      updateQuestionClusterColors(); // Update the color of question cluster
    }
  }

  function renderOptions(questionType, currentOptions) {
    optionsContainer.innerHTML = ''; 
    if (questionType === "MCQ") {
      type.textContent = "MCQ";
      mcq.style.display = "block";
      trueFalse.style.display = "none";
      numerical.style.display = "none";
      currentOptions.forEach(option => {
        const optionDiv = createOptionElement(option, `question${currentQuestionIndex}`); // Pass name to createOptionElement
        optionsContainer.appendChild(optionDiv);
      });
    } else if (questionType === "True/False") {
      type.textContent = "True/False";
      mcq.style.display = "none";
      trueFalse.style.display = "block";
      numerical.style.display = "none";
      // Create True/False options (outside of the conditional block)
      if (!trueOption) { 
        trueOption = createOptionElement({ optionid: 1, value: "True" }, `question${currentQuestionIndex}`);
        falseOption = createOptionElement({ optionid: 2, value: "False" }, `question${currentQuestionIndex}`);
        trueFalse.appendChild(trueOption);
        trueFalse.appendChild(falseOption);
      }

      // Reset the checked state of the True/False options
      trueOption.querySelector('input').checked = false; 
      falseOption.querySelector('input').checked = false;

      // Set the correct name for the radio buttons based on the current question
      trueOption.querySelector('input').name = `question${currentQuestionIndex}`;
      falseOption.querySelector('input').name = `question${currentQuestionIndex}`;
    } else if (questionType === "Numerical") {
      type.textContent = "Numerical";
      mcq.style.display = "none";
      trueFalse.style.display = "none";
      numerical.style.display = "block";

      const numericalInput = document.createElement('input');
      numericalInput.type = 'number';
      numericalInput.id = 'numericalInput';

      // Add event listener for the numerical input
      numericalInput.addEventListener('input', () => { // Trigger on input change
        const questionIndex = currentQuestionIndex;
        answeredQuestions.set(questionIndex, numericalInput.value);
        updateQuestionClusterColors();
      });
      numerical.appendChild(numericalInput);
    }
  }

  function createOptionElement(option, name) {
    const optionDiv = document.createElement('div');
    optionDiv.classList.add('option');

    const input = document.createElement('input');
    input.type = 'radio';
    input.name = name; // Use the consistent 'name' attribute
    input.id = `option${option.optionid}`;
    input.value = option.value;

    // Add event listener to handle option selection
    input.addEventListener('change', () => {
      const questionIndex = currentQuestionIndex;
      answeredQuestions.set(questionIndex, option.value);
      updateQuestionClusterColors();
    });

    const label = document.createElement('label');
    label.htmlFor = input.id;
    label.classList.add(`option${option.optionid}`);
    label.textContent = option.value;

    optionDiv.appendChild(input);
    optionDiv.appendChild(label);
    return optionDiv;
  }

  function handleQuestionClick(index) {
    // Remove blue background from previously clicked question
    const previousQuestionDiv = document.querySelector('.question-cluster .question.active');
    if (previousQuestionDiv) {
      previousQuestionDiv.style.background = "";
      previousQuestionDiv.style.color = "";
      previousQuestionDiv.classList.remove('active');
    }

    // Set blue background for the clicked question
    const questionDiv = document.querySelector(`.question-cluster .question:nth-child(${index + 1})`);
    if (questionDiv) {
      questionDiv.style.background = "blue";
      questionDiv.style.color = "white";
      questionDiv.classList.add('active');
    }

    displayQuestion(index);
  }

  // Function to display the question cluster
  function displayQuestionCluster(QuestionDTO, currentQuestionIndex) {
    const QuestionCluster = document.querySelector('.question-cluster');
    QuestionCluster.style.display = "flex";
    QuestionCluster.style.flexDirection = "row";
    QuestionCluster.style.margin = "10px";
    QuestionCluster.innerHTML = ''; // Clear previous content

    QuestionDTO.forEach((question, index) => {
      const questionDiv = document.createElement('div');
      questionDiv.classList.add('question');
      questionDiv.style.border = "2px solid #000";
      questionDiv.style.borderRadius = "50%";
      questionDiv.style.width = "30px";
      questionDiv.style.height = "30px";
      questionDiv.style.textAlign = "center";
      questionDiv.style.lineHeight = "30px";
      questionDiv.style.marginRight = "10px";
      questionDiv.textContent = index + 1;

      questionDiv.addEventListener('click', () => {
        handleQuestionClick(index);
      });

      QuestionCluster.appendChild(questionDiv);

      // Add horizontal row after each question div, except the last one
      if (index < QuestionDTO.length - 1) {
        const hr = document.createElement('hr');
        hr.style.border = "none";
        hr.style.borderTop = "2px solid #000";
        QuestionCluster.appendChild(hr);
      }
    });

    updateQuestionClusterColors(); // Initial color update
  }

  function updateQuestionClusterColors() {
    const questionDivs = document.querySelectorAll('.question-cluster .question');
    questionDivs.forEach((questionDiv, index) => {
      if (index === currentQuestionIndex) { 
        // Check current question first
        questionDiv.style.background = "blue";
        questionDiv.style.color = "white";
        questionDiv.classList.add('active'); 
      } else if (answeredQuestions.has(index)) {
        questionDiv.style.background = "green";
        questionDiv.style.color = "white";
      } else if (visitedQuestions.has(index)) {
        // Then check visited questions
        questionDiv.style.background = "red";
        questionDiv.style.color = "white";
      } else {
        questionDiv.style.background = "grey";
        questionDiv.style.color = "black";
      }
    });
  }
}

function ResumeTest(email) {
  const TestId = localStorage.getItem('customQuizzTestId');
  const AssignmentNumber = localStorage.getItem('customQuizzAssignmentNumber');
  fetch(`${common.apiUrl}/api/Test/GetTestQuestions`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    },
    body: JSON.stringify({
      AssignmentNumber: parseInt(AssignmentNumber),
      email
    })
  })
  .then(response => {
    if (response.ok) {
      return response.json();
    } else {
      throw new Error('Failed to get custom quizz');
    }
  })
  .then(data => {
    const { testQuestions, questionOptions } = data;
    console.log('Custom Quizz fetched successfully:', testQuestions);
    console.log('OptionsDTO:', questionOptions);
    LiveQuizz(testQuestions, questionOptions);
  })
  .catch(error => {
    console.error('Error fetching custom quizz:', error);
    // Handle error appropriately, e.g., display an error message to the user
  });
}
