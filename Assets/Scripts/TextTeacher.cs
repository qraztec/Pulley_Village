using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;

public class TextTeacher : MonoBehaviour
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
    private List<(string Question, int CorrectAnswerIndex)> questionBank = new List<(string, int)>
{
    ("What condition must be met for an object to be in static equilibrium?",
     
     0), // Correct answer: Net force must be zero

    ("What is the principle that describes why ships float?",
     
     0), // Correct answer: Archimedes' principle

    ("In the context of buoyancy, what does the term 'displacement' refer to?",
     0), // Correct answer: The volume of fluid pushed aside by an object

    ("Which force is responsible for keeping a pulley system in equilibrium?",
     
     0), // Correct answer: Tension force

    ("What happens to the buoyant force as an object is submerged deeper into a fluid?",
    
     0), // Correct answer: It remains constant

    ("If a pulley system is not moving, what can be said about the tension in the ropes?",
    
     0), // Correct answer: Tension is the same throughout

    ("What is the net torque on an object that is in rotational equilibrium?",
    
     0), // Correct answer: Zero

    ("How does the center of buoyancy change when a floating object tilts?",
    
     1), // Correct answer: Stays in the same position

    ("What is the relationship between the mechanical advantage of a pulley system and the number of ropes supporting the load?",
    
     0), // Correct answer: Directly proportional

    ("When an object is floating in water, what can be said about the weight of the object and the buoyant force?",
   
     0), // Correct answer: They are equal
};



    private (string Question, int CorrectAnswerIndex) currentQuestion;

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
        
        startQuizButton = CreateButton("StartQuizButton", "Start Quiz", new Vector2(0, 0), new Vector2(200, 50)); //size was 200, 50
        startQuizButton.transform.localScale = new Vector2(10, 10);
      //  startQuizButton.transform.position = new Vector3(0, 0, s);
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
        /*
        for (int i = 0; i < currentQuestion.Options.Length; i++)
        {
            Button optionButton = CreateButton("Option" + (i + 1), currentQuestion.Options[i], new Vector2(0, 60 - (i * 60)), new Vector2(200, 50));
            int index = i; // Local copy for the closure below
            optionButton.onClick.AddListener(() => OnOptionSelected(index));
            optionButton.transform.localScale = new Vector3(10, 10, 1);
            optionButton.transform.position = new Vector3(optionButton.transform.position.x, optionButton.transform.position.y + i * 2 + 2, optionButton.transform.position.z);
            optionButtons.Add(optionButton);
        }
        */

        // Create the submit button
        submitButton = CreateButton("SubmitButton", "Submit", new Vector2(0, -200), new Vector2(200, 50));
        submitButton.transform.localScale = new Vector3(10, 10, 1);
        submitButton.onClick.AddListener(OnSubmit);
        submitButton.interactable = false; // Start with the submit button disabled

        // Create the feedback text, initially empty and not visible
        /*
        feedbackText = CreateTextElement("FeedbackText", "", new Vector2(0, 160), new Vector2(600, 100));
        feedbackText.transform.localScale = new Vector3(10, 10, 1);
        feedbackText.transform.position = new Vector3(feedbackText.transform.position.x, feedbackText.transform.position.y + 10, feedbackText.transform.position.z);
        feedbackText.gameObject.SetActive(false); // Hide until needed
        */

        // Create the next question button, but don't show it yet
        nextQuestionButton = CreateButton("NextQuestionButton", "Next Question", new Vector2(0, -200), new Vector2(200, 50));
        nextQuestionButton.transform.localScale = new Vector3(10, 10, 1);
        nextQuestionButton.onClick.AddListener(OnNextQuestion);
        nextQuestionButton.gameObject.SetActive(true); // Hide until needed
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
                //feedbackText.text = "Incorrect. The correct answer is: " + currentQuestion.Options[currentQuestion.CorrectAnswerIndex];


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
