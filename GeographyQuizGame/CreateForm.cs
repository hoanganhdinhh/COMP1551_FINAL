using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace GeographyQuizGame
{
    public partial class CreateForm : Form
    {
        private int editingIndex = -1;
        private GameManager gameManager;
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GeographyQuizGameDB;Integrated Security=True;";


        public CreateForm(GameManager gm)
        {
            InitializeComponent();

            txtQuestion.Text = "Enter question text";
            txtQuestion.ForeColor = Color.Gray;

            txtQuestion.GotFocus += (s, e) =>
            {
                if (txtQuestion.Text == "Enter question text")
                {
                    txtQuestion.Text = "";
                    txtQuestion.ForeColor = Color.Black;
                }
            };

            txtQuestion.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtQuestion.Text))
                {
                    txtQuestion.Text = "Enter question text";
                    txtQuestion.ForeColor = Color.Gray;
                }
            };
            txtOpenAnswers.Text = "e.g. UK, United Kingdom";
            txtOpenAnswers.ForeColor = Color.Gray;

            txtOpenAnswers.GotFocus += (s, e) =>
            {
                if (txtOpenAnswers.Text == "e.g. UK, United Kingdom")
                {
                    txtOpenAnswers.Text = "";
                    txtOpenAnswers.ForeColor = Color.Black;
                }
            };

            txtOpenAnswers.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtOpenAnswers.Text))
                {
                    txtOpenAnswers.Text = "e.g. UK, United Kingdom";
                    txtOpenAnswers.ForeColor = Color.Gray;
                }
            };
            gameManager = gm;
            comboType.Items.AddRange(new[] { "Multiple Choice", "Open Ended", "True/False" });
            comboType.SelectedIndex = 0;
            RefreshList();
        }

        private void RefreshList()
        {
            listBoxQuestions.Items.Clear();
            listBoxQuestions.Items.AddRange(gameManager.Questions.Select(q => q.Text).ToArray());
            var loadedQuestions = LoadQuestionsFromDatabase(); // đọc từ DB
            gameManager.Questions.Clear();                      // xoá dữ liệu cũ
            gameManager.Questions.AddRange(loadedQuestions);    // thêm từng phần tử
            listBoxQuestions.Items.Clear();
            listBoxQuestions.Items.AddRange(gameManager.Questions.Select(q => q.Text).ToArray());
        }
        private List<Question> LoadQuestionsFromDatabase()
        {
            var questions = new List<Question>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT QuestionText, QuestionType, CorrectAnswer, AnswerOptions, Category FROM Questions";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string text = reader["QuestionText"].ToString();
                        string type = reader["QuestionType"].ToString();
                        string correct = reader["CorrectAnswer"].ToString();
                        string options = reader["AnswerOptions"].ToString();

                        if (type == nameof(MultipleChoiceQuestion))
                        {
                            var mcq = new MultipleChoiceQuestion
                            {
                                Text = text,
                                Options = options.Split(','),
                                CorrectIndex = Array.IndexOf(options.Split(','), correct)
                            };
                            questions.Add(mcq);
                        }
                        else if (type == nameof(OpenEndedQuestion))
                        {
                            var oe = new OpenEndedQuestion
                            {
                                Text = text,
                                AcceptableAnswers = options.Split(',').Select(s => s.Trim()).ToList()
                            };
                            questions.Add(oe);
                        }
                        else if (type == nameof(TrueFalseQuestion))
                        {
                            var tf = new TrueFalseQuestion
                            {
                                Text = text,
                                IsTrue = correct == "True"
                            };
                            questions.Add(tf);
                        }
                    }
                }
            }

            return questions;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var text = txtQuestion.Text.Trim();
            if (string.IsNullOrEmpty(text)) return;

            Question q = null;
            switch (comboType.SelectedItem.ToString())
            {
                case "Multiple Choice":
                    q = new MultipleChoiceQuestion
                    {
                        Text = text,
                        Options = new[]
                        {
                            txtOptA.Text, txtOptB.Text, txtOptC.Text, txtOptD.Text
                        },
                        CorrectIndex = comboCorrect.SelectedIndex
                    };
                    break;

                case "Open Ended":
                    var answers = txtOpenAnswers.Text
                        .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(s => s.Trim()).ToList();
                    q = new OpenEndedQuestion
                    {
                        Text = text,
                        AcceptableAnswers = answers
                    };
                    break;
                case "True/False":
                    q = new TrueFalseQuestion
                    {
                        Text = text,
                        IsTrue = radioTrue.Checked // ✅ key line
                    };
                    break;
            }

            if (q != null)
            {
                if (q != null)
                {
                    SaveQuestionToDatabase(q);
                    gameManager.Questions.Add(q); 
                    RefreshList(); 
                    ClearInputs();
                    MessageBox.Show("✅ Question added!");
                }

            }
        }

        private void SaveQuestionToDatabase(Question q)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Questions (QuestionText, QuestionType, CorrectAnswer, AnswerOptions, Category) " +
                                   "VALUES (@text, @type, @correct, @options, @category)";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@text", q.Text);
                    cmd.Parameters.AddWithValue("@type", q.GetType().Name);

                    if (q is MultipleChoiceQuestion mcq)
                    {
                        cmd.Parameters.AddWithValue("@correct", mcq.Options[mcq.CorrectIndex]);
                        cmd.Parameters.AddWithValue("@options", string.Join(",", mcq.Options));
                        cmd.Parameters.AddWithValue("@category", "Multiple Choice");
                    }
                    else if (q is OpenEndedQuestion oq)
                    {
                        cmd.Parameters.AddWithValue("@correct", oq.AcceptableAnswers[0]);
                        cmd.Parameters.AddWithValue("@options", string.Join(",", oq.AcceptableAnswers));
                        cmd.Parameters.AddWithValue("@category", "Open Ended");
                    }
                    else if (q is TrueFalseQuestion tfq)
                    {
                        cmd.Parameters.AddWithValue("@correct", tfq.IsTrue ? "True" : "False");
                        cmd.Parameters.AddWithValue("@options", "True,False");
                        cmd.Parameters.AddWithValue("@category", "True/False");
                    }

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error saving to database: " + ex.Message);
            }
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            var idx = listBoxQuestions.SelectedIndex;
            if (idx >= 0)
            {
                gameManager.Questions.RemoveAt(idx);
                RefreshList();
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (listBoxQuestions.SelectedIndex < 0)
            {
                MessageBox.Show("Select a question to edit.");
                return;
            }

            editingIndex = listBoxQuestions.SelectedIndex;
            var q = gameManager.Questions[editingIndex];

            txtQuestion.Text = q.Text;

            if (q is MultipleChoiceQuestion mcq)
            {
                comboType.SelectedItem = "Multiple Choice";
                txtOptA.Text = mcq.Options[0];
                txtOptB.Text = mcq.Options[1];
                txtOptC.Text = mcq.Options[2];
                txtOptD.Text = mcq.Options[3];
                comboCorrect.SelectedIndex = mcq.CorrectIndex;
            }
            else if (q is TrueFalseQuestion tfq)
            {
                comboType.SelectedItem = "True/False";
                radioTrue.Checked = tfq.IsTrue;
                radioFalse.Checked = !tfq.IsTrue;
            }
            else if (q is OpenEndedQuestion oq)
            {
                comboType.SelectedItem = "Open Ended";
                txtOpenAnswers.Text = string.Join(", ", oq.AcceptableAnswers);
            }

            btnAdd.Enabled = false;
            btnUpdate.Enabled = true;
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (editingIndex < 0)
            {
                MessageBox.Show("No question is selected to update.");
                return;
            }

            var updated = BuildQuestionFromInputs();
            if (updated == null) return;

            gameManager.Questions[editingIndex] = updated;
            listBoxQuestions.Items[editingIndex] = updated.Text;

            ClearInputs();
            editingIndex = -1;
            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
        }
        private Question BuildQuestionFromInputs()
        {
            string text = txtQuestion.Text.Trim();

            if (string.IsNullOrWhiteSpace(text) || comboType.SelectedItem == null)
            {
                MessageBox.Show("Please enter the question text and select a question type.");
                return null;
            }

            switch (comboType.SelectedItem.ToString())
            {
                case "Multiple Choice":
                    if (comboCorrect.SelectedIndex < 0)
                    {
                        MessageBox.Show("Please select the correct option (A–D).");
                        return null;
                    }

                    return new MultipleChoiceQuestion
                    {
                        Text = text,
                        Options = new[]
                        {
                    txtOptA.Text.Trim(),
                    txtOptB.Text.Trim(),
                    txtOptC.Text.Trim(),
                    txtOptD.Text.Trim()
                },
                        CorrectIndex = comboCorrect.SelectedIndex
                    };

                case "True/False":
                    if (!radioTrue.Checked && !radioFalse.Checked)
                    {
                        MessageBox.Show("Please select whether the correct answer is True or False.");
                        return null;
                    }

                    return new TrueFalseQuestion
                    {
                        Text = text,
                        IsTrue = radioTrue.Checked
                    };

                case "Open Ended":
                    var answerInput = txtOpenAnswers.Text.Trim();
                    if (string.IsNullOrWhiteSpace(answerInput))
                    {
                        MessageBox.Show("Please enter at least one acceptable answer.");
                        return null;
                    }

                    var acceptableAnswers = answerInput
                        .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(a => a.Trim())
                        .Where(a => a.Length > 0)
                        .ToList();

                    return new OpenEndedQuestion
                    {
                        Text = text,
                        AcceptableAnswers = acceptableAnswers
                    };

                default:
                    MessageBox.Show("Invalid question type selected.");
                    return null;
            }
        }

        private void comboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Show/hide panels for each question type
            panelMC.Visible = comboType.SelectedIndex == 0;
            panelOE.Visible = comboType.SelectedIndex == 1;
            panelTF.Visible = comboType.SelectedIndex == 2;
        }

        private void ClearInputs()
        {
            txtQuestion.Clear();
            txtOptA.Clear(); txtOptB.Clear(); txtOptC.Clear(); txtOptD.Clear();
            comboCorrect.SelectedIndex = -1;
            txtOpenAnswers.Clear();
            radioTrue.Checked = false; radioFalse.Checked = false;
        }
        private List<Question> GetRandomQuestions(int count)
        {
            var questions = LoadQuestionsFromDatabase(); // lấy toàn bộ
            var random = new Random();
            return questions.OrderBy(x => random.Next()).Take(count).ToList(); // random
        }

        private void listBoxQuestions_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void CreateForm_Load(object sender, EventArgs e)
        {
            RefreshList();
            // Any initialization code you need here
        }

    }
}