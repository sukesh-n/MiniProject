document.addEventListener('DOMContentLoaded', () => {
    const resultsContainer = document.querySelector('.results-container');
    const quizDataJSON = localStorage.getItem('quizResults');
    localStorage.removeItem('customQuizzAssignmentNumber');
    localStorage.removeItem('customQuizzTestId');
    if (!quizDataJSON) {
      resultsContainer.innerHTML = '<p>No quiz results found.</p>';
      return;
    }
  
    const quizData = JSON.parse(quizDataJSON);
    const questionDTO = quizData.questionDTO;
    const scoreDTO = quizData.scoreDTO;
    const solution = quizData.solution;
  
    // Create the score summary section
    const scoreSummary = `
      <h2>Score Summary</h2>
      <p><b>Test ID:</b> ${scoreDTO.testId}</p>
      <p><b>User ID:</b> ${scoreDTO.userId}</p>
      <p><b>Assignment Number:</b> ${scoreDTO.assignmentNumber}</p>
      <p><b>Total Questions:</b> ${scoreDTO.totalQuestions}</p>
      <p><b>Total Correct Answers:</b> ${scoreDTO.totalCorrectAnswers}</p>
      <p><b>Total Wrong Answers:</b> ${scoreDTO.totalWrongAnswers}</p>
      <p><b>Score:</b> ${scoreDTO.score}</p>
    `;
  
    // Create the results table
    const resultsTable = `
      <h2>Detailed Results</h2>
      <table>
        <thead>
          <tr>
            <th>Question ID</th>
            <th>Description</th>
            <th>Type</th>
            <th>Category ID</th>
            <th>Difficulty</th>
            <th>Your Answer</th>
            <th>Correct Answer</th>
          </tr>
        </thead>
        <tbody>
          ${questionDTO.map(question => {
            // Find matching solution for each question
            const solutionItem = solution.find(s => s.questionId === question.questionId);
            return `
              <tr>
                <td>${question.questionId}</td>
                <td>${question.questionDescription}</td>
                <td>${question.questionType}</td>
                <td>${question.categoryId}</td>
                <td>${question.difficultyLevel}</td>
                <td>${getFormattedAnswer(question)}</td>
                <td>${solutionItem ? getCorrectAnswer(solutionItem, question.questionType) : 'N/A'}</td>
              </tr>
            `;
          }).join('')}
        </tbody>
      </table>
    `;
  
    resultsContainer.innerHTML = scoreSummary + resultsTable;
  });
  
  // Function to get the correct answer for a question
  function getCorrectAnswer(solution, questionType) {
    if (questionType === "True/False") {
      return solution.trueFalseAnswer !== null
        ? solution.trueFalseAnswer
          ? 'True'
          : 'False'
        : 'N/A';
    } else if (questionType === "Numerical") {
      return solution.numericalAnswer || 'N/A';
    } else {
      return solution.correctOptionAnswer || 'N/A';
    }
  }
  
  // Function to format the user's answer
  function getFormattedAnswer(question) {
    if (question.questionType === 'True/False') {
      return question.trueFalseAnswer !== null
        ? question.trueFalseAnswer
          ? 'True'
          : 'False'
        : 'Not answered';
    } else if (question.questionType === 'Numerical') {
      return question.numericalAnswer || 'Not answered';
    } else {
      return question.correctOptionAnswer || 'Not answered';
    }
  }
  