let userEmail = null;
function LiveQuizz(QuestionDto, OptionsDTO) {
  console.log('LiveQuizz:', QuestionDto);
  const quizzLiveContainer = document.querySelector('.custom-quizz-live-container');
  const customQuizzGenerator = document.querySelector('.custom-quizz-generator');
  if(customQuizzGenerator){
    customQuizzGenerator.style.display = "none";
  }
  
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
  const numericalInput = document.createElement('input');
  numericalInput.type = 'number';
  numericalInput.id = 'numericalInput';
  numerical.appendChild(numericalInput);
  numerical.style.display = "none";
  let next = document.querySelector('.next');
  let previous = document.querySelector('.previous');
  let submit = document.querySelector('.submit');
  let totalTime = Math.floor(0.833333 * 60);
  let TimeForQuestion = totalTime / QuestionDto.length;
  let currentQuestionIndex = 0;
  let visitedQuestions = new Set();
  let answeredQuestions = []; 
  let trueOption, falseOption;

  // Function to display the timer
  function displayTimer() {
    if (currentQuestionIndex === 0) {
      previous.style.display = "none";
    } else {
      previous.style.display = "block";
    }
    if (currentQuestionIndex === QuestionDto.length - 1) {
      next.style.display = "none";
    } else {
      next.style.display = "block";
    }

    // Show submit button only when all questions are answered
    if (answeredQuestions.size === QuestionDto.length) {
      submit.style.display = "block";
    } else {
      submit.style.display = "none";
    }
    if (answeredQuestions.length === QuestionDto.length) { // Change to .length instead of .size
      submit.style.display = "block";
  } else {
      submit.style.display = "none";
  }
    const hours = Math.floor(totalTime / 3600);
    const minutes = Math.floor((totalTime % 3600) / 60);
    const remainingSeconds = totalTime % 60;

    timerContainer.textContent =
      `${String(hours).padStart(2, '0')}:${String(minutes).padStart(2, '0')}:${String(remainingSeconds).padStart(2, '0')}`;

    if (totalTime > 0) {
      totalTime--;
      setTimeout(displayTimer, 1000);
    } else {
      handleQuizTimeout(); 
    }
  }

  function handleQuizTimeout() {
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

    document.body.appendChild(masterSubmit); 

    // Add event listener to handle the master submit button click
    masterSubmit.addEventListener('click', handleSubmit); 
  }

  function handleSubmit() {
    const allAnswers = QuestionDto.map(question => {
      const existingAnswer = answeredQuestions.find(a => a.questionId === question.questionId);
  
      if (existingAnswer) {
        return existingAnswer; 
      } else {
        return {
          "questionId": question.questionId,
          "questionDescription": question.questionDescription,
          "questionType": question.questionType,
          "categoryId": question.categoryId,
          "difficultyLevel": question.difficultyLevel,
          "correctOptionAnswer": "",
          "numericalAnswer": 0,
          "trueFalseAnswer": false
        };
      }
    });
  
    console.log("All Answers (including unanswered with null):", allAnswers);

    let AssignmentNumber= localStorage.getItem('customQuizzAssignmentNumber');
    let email= userEmail;
    const testSubmitEndPoint = `http://localhost:5246/api/Test/AttendTest`; // Updated endpoint

    // Create a request object to match the server-side DTO:
    const requestData = {
      QuestionDTO: allAnswers, 
      AssignmentNumber: parseInt(AssignmentNumber),
      Email: email
    };
    fetch(testSubmitEndPoint, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(requestData) 
    })
    .then(response => {
      if (response.ok) {
        return response.json();
      } else {
        throw new Error('Failed to get custom quiz');
      }
    })
    .then(data => {
      console.log('Quiz submitted successfully:', data);
  
      // Store data in localStorage
      localStorage.setItem('quizResults', JSON.stringify(data));

      const viewResultsButton = document.createElement('button');
      viewResultsButton.textContent = 'View Results';
      viewResultsButton.onclick = function() {
        window.open('../Users/results.html', '_blank');
      };
      document.body.appendChild(viewResultsButton);
      
    })
    .catch(error => {
      console.error("Error submitting quiz:", error);
      // ... handle submission error (e.g., display an error message to the user)
    });
  }

  // Event listeners for navigation buttons
  previous.addEventListener('click', () => {
    if (currentQuestionIndex > 0) {
      handleQuestionClick(currentQuestionIndex - 1);
    }
  });

  next.addEventListener('click', () => {
    if (currentQuestionIndex < QuestionDto.length - 1) {
      handleQuestionClick(currentQuestionIndex + 1);
    }
  });

  // Event listener for submit button
  submit.addEventListener('click', handleSubmit); 

  // Start the timer and display the first question
  displayTimer();
  displayQuestionCluster(QuestionDto);
  displayQuestion(currentQuestionIndex);

  // Function to display a specific question
  function displayQuestion(i) {
    currentQuestionIndex = i;
    if (i < QuestionDto.length) {
      const question = QuestionDto[i];
      console.log('QuestionDto:', i);
      questionId.textContent = i + 1;
      questionDescription.textContent = question.questionDescription;
      QuestionDifficulty.textContent = question.difficultyLevel;
  
      const currentOptions = OptionsDTO.filter(option => option.questionId === question.questionId);
      renderOptions(question.questionType, currentOptions);
  
      // Restore saved answer if available
      const savedAnswer = answeredQuestions.find(q => q.questionId === QuestionDto[i].questionId);
      if (savedAnswer) {
        if (question.questionType === "MCQ" || question.questionType === "True/False") {
          const optionInput = document.querySelector(`input[name="question${i}"][value="${savedAnswer.correctOptionAnswer}"]`);
          if (optionInput) {
            optionInput.checked = true;
          }
        } else if (question.questionType === "Numerical") {
          numericalInput.value = savedAnswer.numericalAnswer;
        }
      } else {
        // Clear the input field if no answer is saved for this question
        numericalInput.value = "";
      }
  
      visitedQuestions.add(i);
      updateQuestionClusterColors();
    }
  }

  // Function to render options based on question type
  function renderOptions(questionType, currentOptions) {
    optionsContainer.innerHTML = '';
    if (questionType === "MCQ") {
      type.textContent = "MCQ";
      mcq.style.display = "block";
      trueFalse.style.display = "none";
      numerical.style.display = "none";

      currentOptions.forEach(option => {
        const optionDiv = createOptionElement(option, `question${currentQuestionIndex}`);
        optionsContainer.appendChild(optionDiv);
      });
    } else if (questionType === "True/False") {
      type.textContent = "True/False";
      mcq.style.display = "none";
      trueFalse.style.display = "block";
      numerical.style.display = "none";

      // Create True/False options only once
      if (!trueOption) {
        trueOption = createOptionElement({ optionid: 1, value: "True" }, `question${currentQuestionIndex}`);
        falseOption = createOptionElement({ optionid: 2, value: "False" }, `question${currentQuestionIndex}`);
        trueFalse.appendChild(trueOption);
        trueFalse.appendChild(falseOption);
      }

      // Reset the checked state of True/False options
      trueOption.querySelector('input').checked = false;
      falseOption.querySelector('input').checked = false;

      // Set the correct name attribute for radio buttons
      trueOption.querySelector('input').name = `question${currentQuestionIndex}`;
      falseOption.querySelector('input').name = `question${currentQuestionIndex}`;
    } else if (questionType === "Numerical") {
      type.textContent = "Numerical";
      mcq.style.display = "none";
      trueFalse.style.display = "none";
      numerical.style.display = "block";
      const savedAnswer = answeredQuestions.find(question => question.questionId === QuestionDto[currentQuestionIndex].questionId);
      // Clear the numerical input only if no answer is stored
      if (savedAnswer) {
        numericalInput.value = "";
      }

      // Add an event listener to the numerical input field
  numericalInput.addEventListener('input', () => {
    const currentQuestion = QuestionDto[currentQuestionIndex];
    answeredQuestions = answeredQuestions.filter(question => question.questionId !== currentQuestion.questionId);
    answeredQuestions.push({
        "questionId": currentQuestion.questionId,
        "questionDescription": currentQuestion.questionDescription,
        "questionType": currentQuestion.questionType,
        "categoryId": currentQuestion.categoryId,
        "difficultyLevel": currentQuestion.difficultyLevel,
        "correctOptionAnswer": numericalInput.value.toString(), // Update correctOptionAnswer 
        "numericalAnswer": parseInt(numericalInput.value, 10), // Update numericalAnswer 
        "trueFalseAnswer": currentQuestion.trueFalseAnswer.toLowerCase() // trueFalseAnswer remains unchanged for numerical
    });
    updateQuestionClusterColors();
  });
    }
  }

  // Function to create an option element (for MCQ and True/False)
  function createOptionElement(option, name) {
    const optionDiv = document.createElement('div');
    optionDiv.classList.add('option');

    const input = document.createElement('input');
    input.type = 'radio';
    input.name = name;
    input.id = `option${option.optionid}`;
    input.value = option.value;

input.addEventListener('change', () => {
    const currentQuestion = QuestionDto[currentQuestionIndex];
    answeredQuestions = answeredQuestions.filter(question => question.questionId !== currentQuestion.questionId);
    answeredQuestions.push({
        "questionId": currentQuestion.questionId,
        "questionDescription": currentQuestion.questionDescription,
        "questionType": currentQuestion.questionType,
        "categoryId": currentQuestion.categoryId,
        "difficultyLevel": currentQuestion.difficultyLevel,
        "correctOptionAnswer": option.value.toString(), // Update correctOptionAnswer
        "numericalAnswer": currentQuestion.questionType === "Numerical" ? parseInt(option.value, 10) : 0, // Update numericalAnswer if applicable
        "trueFalseAnswer": currentQuestion.questionType === "True/False" ? (option.value.toLowerCase() === 'true') : currentQuestion.trueFalseAnswer // Update trueFalseAnswer if applicable
    });
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

  // Function to handle clicks on question cluster elements
  function handleQuestionClick(index) {
    const previousQuestionDiv = document.querySelector('.question-cluster .question.active');
    if (previousQuestionDiv) {
      previousQuestionDiv.style.background = "";
      previousQuestionDiv.style.color = "";
      previousQuestionDiv.classList.remove('active');
    }

    const questionDiv = document.querySelector(`.question-cluster .question:nth-child(${index + 1})`);
    if (questionDiv) {
      questionDiv.style.background = "blue";
      questionDiv.style.color = "white";
      questionDiv.classList.add('active');
    }

    displayQuestion(index);
  }

  // Function to display the question cluster
  function displayQuestionCluster(QuestionDTO) {
    const QuestionCluster = document.querySelector('.question-cluster');
    QuestionCluster.style.display = "flex";
    QuestionCluster.style.flexDirection = "row";
    QuestionCluster.style.margin = "10px";
    QuestionCluster.innerHTML = ''; 

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

      if (index < QuestionDTO.length - 1) {
        const hr = document.createElement('hr');
        hr.style.border = "none";
        hr.style.borderTop = "2px solid #000";
        QuestionCluster.appendChild(hr);
      }
    });

    updateQuestionClusterColors();
  }

  // Function to update question cluster colors based on answer status
  function updateQuestionClusterColors() {
    const questionDivs = document.querySelectorAll('.question-cluster .question');
  
    questionDivs.forEach((questionDiv, index) => {
      const isQuestionAnswered = answeredQuestions.some(question => question.questionId === QuestionDto[index].questionId);
  
      if (index === currentQuestionIndex) {
        questionDiv.style.background = "blue";
        questionDiv.style.color = "white";
        questionDiv.classList.add('active');
      } else if (isQuestionAnswered) { 
        questionDiv.style.background = "green";
        questionDiv.style.color = "white";
      } else if (visitedQuestions.has(index)) {
        questionDiv.style.background = "red";
        questionDiv.style.color = "white";
      } else {
        questionDiv.style.background = "grey";
        questionDiv.style.color = "black";
      }
    });
  }
}


// Example usage with ResumeTest function
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
      throw new Error('Failed to get custom quiz');
    }
  })
  .then(data => {
    const { testQuestions, questionOptions } = data;
    console.log('Custom Quiz fetched successfully:', testQuestions);
    console.log('OptionsDTO:', questionOptions);
    userEmail=email;
    LiveQuizz(testQuestions, questionOptions);
  })
  .catch(error => {
    console.error('Error fetching custom quiz:', error);
  });
}