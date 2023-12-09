using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;

public class QuizScript : MonoBehaviour
{
    public Canvas canvas; // Assign your canvas in the inspector
    public Font font; // Assign a font in the inspector

    private Text questionText;
    private Text feedbackText; // Text element to provide feedback
    private List<Button> optionButtons = new List<Button>();
    private Button submitButton;
    private Button startQuizButton; // Button to start the quiz
    private Button nextQuestionButton; // Button to load the next question
    private int selectedOption = -1;
    private int currentQuestionIndex = 0; // Index to keep track of the current question

    // Example question bank
    private List<(string Question, string[] Options, int CorrectAnswerIndex, string answer)> questionBank = new List<(string, string[], int, string)>
{
    ("What condition must be met for an object to be in static equilibrium?",
     new string[] { "Net force must be zero", "Net force must be constant", "Net torque must be increasing", "Net torque must be constant" },
     0, "Incorrect: Net force must be zero. An object is at rest with no acceleration, if an object had a non-zero force it will not be static"), // Correct answer: Net force must be zero

    ("What is Newton's second law?",
     new string[] { "F = ma", "F = mg", "F = m*sin(theta)*a", "F = 2mg" },
     0, "Incorrect: F = ma. Newton's 2nd Law states force applied to an object is directly proportional to its mass and acceleration."), // Correct answer: Archimedes' principle

    ("What is tension?",
     new string[] { "Two objects rubbing each other", "The rope's own pushing force", "The pulling force on a rope", "The downward force on a rope" },
     2, "Incorrect: The pulling force on a rope. When force is applied to one end of the rope, it transmits the force along its length, creating tension from pulling the other end"), // Correct answer: The volume of fluid pushed aside by an object

    ("For a basic atwood pulley system, what is the acceleration when both masses are 1?",
     new string[] { "1", "0", "3", "10" },
     1, "Incorrect: 0. In this system with equal masses, the forces are balanced, resulting in zero net force. Therefore, there's no acceleration, and the system remains at rest."), // Correct answer: Tension force

    ("What do you multiply the mass and gravity with if it is at an angle that is 30 degrees?",
     new string[] { "cos(30)", "sin(30)", "tan(30)", "You multiply nothing" },
     1, "Incorrect: sin(30). Multiplying by sin(30) accounts for the component of the weight acting along the inclined direction (30 degrees)."), // Correct answer: It remains constant

    ("If a pulley system is not moving, what can be said about the tension in the ropes?",
     new string[] { "Tension is only at the ends", "Tension is zero", "Tension varies with the angle of the rope", "Tension is the same throughout" },
     3, "Incorrect: Tension is the same throughout. When a pulley isn't moving tension is the same as there is no acceleration and forces are balanced."), // Correct answer: Tension is the same throughout

    ("You are pulling an object connected by two rope, but you are pulling one rope. How much is the force carrying the object multiplied?",
     new string[] { "2", "4", "0", "1" },
     0, "Incorrect: 2. The number of ropes supporting the user pulling one rope, is the amount of force that is multiplied. Two ropes multiplied with one rope is 2."), // Correct answer: Zero

    ("What is the primary function of a pulley? ",
     new string[] { "Transmitting electrical power", "Changing the direction of force", "Generating heat energy", "Filtering liquids" },
     1, "Incorrect: Changing the direction of force. Pulley systems allow to apply force in an effective direction while transmitting the same amount of force to move an object."), // Correct answer: Stays in the same position

    ("What is the relationship between the mechanical advantage of a pulley system and the number of ropes supporting the load?",
     new string[] { "Directly proportional", "Inversely proportional", "No relationship", "Exponential" },
     0, "Incorrect: Directly proportional. Each additional rope segment contributes to the overall mechanical advantage, making it easier to lift the load."), // Correct answer: Directly proportional

    ("What is the acceleration equation of one mass (m1) that is being pulled down by gravity, and another mass (m2) that is stationary at a block?",
     new string[] { "a = -m1g/(m1+m2)", "a = (m2g-m1g)/(m1+m2)", "a = -m1/(m1+m2)", "a = -m2g/(m1+m2)" },
     0, "Incorrect: a = -m1g/(m1+m2). Only m1 is pulled by gravitational force, so only m1 is included in the denominator. All masses added must always be in the denominator."), // Correct answer: They are equal
};



    private (string Question, string[] Options, int CorrectAnswerIndex, string answer) currentQuestion;

    void Start()
    {

        if (canvas == null)
        {
            canvas = FindObjectOfType<Canvas>();
            if (canvas == null)
            {
                Debug.LogError("No Canvas found in the scene.");
                return;
            }
        }

        if (font == null)
        {
            font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        }

        // Create the start quiz button
        startQuizButton = CreateButton("StartQuizButton", "Start Quiz", new Vector2(0, 0), new Vector2(200, 50));
        startQuizButton.transform.localScale = new Vector3(10, 10, 1);
        startQuizButton.onClick.AddListener(InitializeQuiz);
    }



    void InitializeQuiz()
    {
        // Select the first question from the question bank
        currentQuestionIndex = 0;
        LoadQuestion(currentQuestionIndex);

        // Disable the start quiz button after clicking
        startQuizButton.gameObject.SetActive(false);
    }

    void LoadQuestion(int index)
    {
        // Clear previous question and options
        if (questionText != null) Destroy(questionText.gameObject);
        foreach (var button in optionButtons) Destroy(button.gameObject);
        optionButtons.Clear();
        if (feedbackText != null) feedbackText.gameObject.SetActive(false);
        if (submitButton != null) submitButton.gameObject.SetActive(false);
        if (nextQuestionButton != null) nextQuestionButton.gameObject.SetActive(false);

        // Select a new question from the question bank
        currentQuestion = questionBank[index];

        // Generate the quiz UI for the new question
        GenerateUI();
    }

    void GenerateUI()
    {
        // Create the question text
        questionText = CreateTextElement("QuestionText", currentQuestion.Question, new Vector2(0, 160), new Vector2(600, 100));
        questionText.transform.localScale = new Vector3(10, 10, 1);
        questionText.transform.position = new Vector3(questionText.transform.position.x, questionText.transform.position.y + 9, questionText.transform.position.z);

        // Create the answer buttons
        for (int i = 0; i < currentQuestion.Options.Length; i++)
        {
            Button optionButton = CreateButton("Option" + (i + 1), currentQuestion.Options[i], new Vector2(0, 60 - (i * 60)), new Vector2(200, 50));
            int index = i; // Local copy for the closure below
            optionButton.onClick.AddListener(() => OnOptionSelected(index));
            optionButton.transform.localScale = new Vector3(10, 10, 1);
            optionButton.transform.position = new Vector3(optionButton.transform.position.x, optionButton.transform.position.y + i * 2 + 2, optionButton.transform.position.z);
            optionButtons.Add(optionButton);
        }

        // Create the submit button
        submitButton = CreateButton("SubmitButton", "Submit", new Vector2(0, 0), new Vector2(200, 50));// was 0 -200; 200 50
        submitButton.transform.localScale = new Vector3(10, 10, 1);
       // submitButton.transform.position = new Vector3(feedbackText.transform.position.x, feedbackText.transform.position.y + 2, feedbackText.transform.position.z);
        submitButton.onClick.AddListener(OnSubmit);
        submitButton.interactable = false; // Start with the submit button disabled

        // Create the feedback text, initially empty and not visible
        feedbackText = CreateTextElement("FeedbackText", "", new Vector2(0, 160), new Vector2(600, 100));
        feedbackText.transform.localScale = new Vector3(10, 10, 1);
        feedbackText.transform.position = new Vector3(feedbackText.transform.position.x, feedbackText.transform.position.y + 10, feedbackText.transform.position.z);
        feedbackText.gameObject.SetActive(false); // Hide until needed


        // Create the next question button, but don't show it yet
        nextQuestionButton = CreateButton("NextQuestionButton", "Next Question", new Vector2(0, 0), new Vector2(200, 50));
        nextQuestionButton.transform.localScale = new Vector3(10, 10, 1);
        nextQuestionButton.onClick.AddListener(OnNextQuestion);
        nextQuestionButton.gameObject.SetActive(false); // Hide until needed
    }


    Text CreateTextElement(string name, string text, Vector2 position, Vector2 size)
    {
        GameObject textObj = new GameObject(name);
        textObj.transform.SetParent(canvas.transform);
        Text textComponent = textObj.AddComponent<Text>();
        textComponent.font = font;
        textComponent.text = text;
        textComponent.alignment = TextAnchor.MiddleCenter;
        textComponent.color = Color.black;

        RectTransform rectTransform = textObj.GetComponent<RectTransform>();
        rectTransform.sizeDelta = size;
        rectTransform.anchoredPosition = position;

        return textComponent;
    }

    Button CreateButton(string name, string buttonText, Vector2 position, Vector2 size)
    {
        GameObject buttonObj = new GameObject(name, typeof(Image), typeof(Button));
        buttonObj.transform.SetParent(canvas.transform);
        Button buttonComponent = buttonObj.GetComponent<Button>();
        buttonComponent.GetComponent<Image>().color = Color.white;

        RectTransform btnRect = buttonObj.GetComponent<RectTransform>();
        btnRect.sizeDelta = size;
        btnRect.anchoredPosition = position;

        // Create and set up the button's text
        Text btnText = new GameObject("Text").AddComponent<Text>();
        btnText.transform.SetParent(buttonObj.transform);
        btnText.font = font;
        btnText.text = buttonText;
        btnText.alignment = TextAnchor.MiddleCenter;
        btnText.color = Color.black;

        RectTransform textRect = btnText.GetComponent<RectTransform>();
        textRect.sizeDelta = size;
        textRect.anchoredPosition = Vector2.zero;

        return buttonComponent;
    }

    void OnOptionSelected(int optionIndex)
    {
        selectedOption = optionIndex;
        submitButton.interactable = true; // Enable the submit button when an option is selected

        // Provide visual feedback for the selected option
        foreach (var button in optionButtons)
        {
            button.interactable = true;
        }
        optionButtons[optionIndex].interactable = false;
    }

    void OnSubmit()
    {
        if (selectedOption < 0)
        {
            Debug.LogError("No option selected!");
        }
        else
        {
            
            
            // Check if the selected option is the correct one
            if (selectedOption == currentQuestion.CorrectAnswerIndex)
            {
                feedbackText.text = "Correct!";
               
                feedbackText.color = Color.green;
            }
            else
            {

                feedbackText.text = currentQuestion.answer;

                
                feedbackText.color = Color.red;
            }

            feedbackText.gameObject.SetActive(true); // Show feedback text
            submitButton.interactable = false; // Optionally disable the submit button after answering
        }
        // Show the next question button if there are more questions
        if (currentQuestionIndex < questionBank.Count - 1)
        {
            nextQuestionButton.gameObject.SetActive(true);
        }
    }

    void OnNextQuestion()
    {
        // Increment the question index and load the next question
        if (currentQuestionIndex < questionBank.Count - 1)
        {
            currentQuestionIndex++;
            LoadQuestion(currentQuestionIndex);
        }
        else
        {
            // Optionally, handle the end of the quiz here
            Debug.Log("End of the quiz!");
        }
    }

}
