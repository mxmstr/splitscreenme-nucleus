using Nucleus.Gaming.Coop;
using System.IO;

namespace Nucleus.Gaming.Forms
{
    public static class CustomPromptRuntime
    {
        public static string[] customValue;

        public static void CustomUserGeneralPrompts(PlayerInfo player)
        {
            var handlerInstance = GenericGameHandler.Instance;

            if (handlerInstance.context.CustomUserGeneralValues == null || handlerInstance.context.CustomUserGeneralValues?.Length < 1)
            {
                handlerInstance.context.CustomUserGeneralValues = new string[handlerInstance.currentGameInfo.CustomUserGeneralPrompts.Length];
            }
            if (customValue == null || customValue?.Length < 1)
            {
                customValue = new string[handlerInstance.currentGameInfo.CustomUserGeneralPrompts.Length];
            }

            for (int c = 0; c < handlerInstance.context.CustomUserGeneralValues.Length; c++)
            {
                handlerInstance.context.CustomUserGeneralValues[c] = null;
            }

            string valueFile = Path.Combine(GameManager.Instance.GetJsScriptsPath(), Path.GetFileNameWithoutExtension(handlerInstance.currentGameInfo.JsFileName) + "\\custom_gen_values.txt");

            int counter = 0;
            if (handlerInstance.currentGameInfo.SaveCustomUserGeneralValues || handlerInstance.currentGameInfo.SaveAndEditCustomUserGeneralValues || player.PlayerID > 0)
            {
                handlerInstance.Log("Handler uses custom general values");
                if (File.Exists(valueFile))
                {
                    handlerInstance.Log("custom_gen_values.txt already exists for this handler, setting values accordingly");

                    string line;

                    StreamReader file = new StreamReader(valueFile);
                    while ((line = file.ReadLine()) != null)
                    {
                        handlerInstance.Log(string.Format("Custom value {0}: {1}", counter, line));
                        customValue[counter] = line;
                        handlerInstance.context.CustomUserGeneralValues[counter] = line;
                        counter++;
                    }

                    file.Close();

                    if (counter != handlerInstance.currentGameInfo.CustomUserGeneralPrompts.Length)
                    {
                        handlerInstance.Log("Number of lines in file do not match number of prompts. Overwriting file");
                    }
                }
                else if (player.PlayerID == 0)
                {
                    handlerInstance.Log("custom_gen_values.txt does not exist. Creating new file at " + valueFile);
                }
            }
            else if (File.Exists(valueFile) && player.PlayerID == 0 && !handlerInstance.currentGameInfo.SaveCustomUserGeneralValues && !handlerInstance.currentGameInfo.SaveAndEditCustomUserGeneralValues)
            {
                handlerInstance.Log("Deleting value file");
                File.Delete(valueFile);
            }

            if (player.PlayerID == 0 && (!File.Exists(valueFile) || !handlerInstance.currentGameInfo.SaveCustomUserGeneralValues || handlerInstance.currentGameInfo.SaveAndEditCustomUserGeneralValues || (File.Exists(valueFile) && counter != handlerInstance.currentGameInfo.CustomUserGeneralPrompts.Length)))
            {

                if (player.PlayerID == 0 && ((File.Exists(valueFile) && !handlerInstance.currentGameInfo.SaveAndEditCustomUserGeneralValues && !handlerInstance.currentGameInfo.SaveCustomUserGeneralValues) || (File.Exists(valueFile) && counter != handlerInstance.currentGameInfo.CustomUserGeneralPrompts.Length)))
                {
                    handlerInstance.Log("Deleting value file");
                    File.Delete(valueFile);
                }

                if (!Directory.Exists(Path.Combine(GameManager.Instance.GetJsScriptsPath(), Path.GetFileNameWithoutExtension(handlerInstance.currentGameInfo.JsFileName))))
                {
                    Directory.CreateDirectory(Path.Combine(GameManager.Instance.GetJsScriptsPath(), Path.GetFileNameWithoutExtension(handlerInstance.currentGameInfo.JsFileName)));
                }

                bool containsValue = false;
                for (int d = 0; d < handlerInstance.currentGameInfo.CustomUserGeneralPrompts.Length; d++)
                {
                    handlerInstance.Log(string.Format("Prompt {0}/{1}: {2}", (d + 1), handlerInstance.currentGameInfo.CustomUserGeneralPrompts.Length, handlerInstance.currentGameInfo.CustomUserGeneralPrompts[d]));
                    string prevAnswer = "";
                    if (d < customValue.Length && File.Exists(valueFile))
                    {
                        prevAnswer = customValue[d];
                    }
                    Forms.CustomPrompt prompt = new Forms.CustomPrompt(handlerInstance.currentGameInfo.CustomUserGeneralPrompts[d], prevAnswer, d);
                    prompt.ShowDialog();
                    if (customValue[d]?.Length > 0)
                    {
                        handlerInstance.context.CustomUserGeneralValues[d] = customValue[d];
                        handlerInstance.Log("User entered: " + customValue[d]);
                        if (!containsValue)
                        {
                            containsValue = true;
                        }
                    }
                    else
                    {
                        handlerInstance.Log("User did not enter a value for this prompt");
                        handlerInstance.context.CustomUserGeneralValues[d] = null;
                    }
                }

                if (containsValue)
                {
                    using (StreamWriter outputFile = new StreamWriter(valueFile))
                    {
                        foreach (string line in customValue)
                        {
                            outputFile.WriteLine(line);
                        }
                    }
                }
            }

        }

        public static void CustomUserPlayerPrompts(PlayerInfo player)
        {
            var handlerInstance = GenericGameHandler.Instance;

            if (handlerInstance.context.CustomUserPlayerValues == null || handlerInstance.context.CustomUserPlayerValues?.Length < 1)
            {
                handlerInstance.context.CustomUserPlayerValues = new string[handlerInstance.currentGameInfo.CustomUserPlayerPrompts.Length];
            }
            if (customValue == null || customValue?.Length < 1)
            {
                customValue = new string[handlerInstance.currentGameInfo.CustomUserPlayerPrompts.Length];
            }

            string valueFile = Path.Combine(GameManager.Instance.GetJsScriptsPath(), Path.GetFileNameWithoutExtension(handlerInstance.currentGameInfo.JsFileName) + "\\" + player.Nickname + "\\custom_plyr_values.txt");

            int counter = 0;
            if (handlerInstance.currentGameInfo.SaveCustomUserPlayerValues || handlerInstance.currentGameInfo.SaveAndEditCustomUserPlayerValues)
            {
                handlerInstance.Log("Handler uses custom player values");
                if (File.Exists(valueFile))
                {
                    handlerInstance.Log("custom_plyr_values.txt already exists for this player, setting values accordingly");

                    string line;

                    StreamReader file = new StreamReader(valueFile);
                    while ((line = file.ReadLine()) != null)
                    {
                        handlerInstance.Log(string.Format("Custom value {0}: {1}", counter, line));
                        customValue[counter] = line;
                        handlerInstance.context.CustomUserPlayerValues[counter] = line;
                        counter++;
                    }

                    file.Close();

                    if (counter != handlerInstance.currentGameInfo.CustomUserPlayerPrompts.Length)
                    {
                        handlerInstance.Log("Number of lines in file do not match number of prompts. Overwriting file");
                    }
                }
                else
                {
                    handlerInstance.Log("custom_plyr_values.txt does not exist for player " + player.Nickname + ". Creating new file at " + valueFile);
                }
            }

            if (!File.Exists(valueFile) || !handlerInstance.currentGameInfo.SaveCustomUserPlayerValues || handlerInstance.currentGameInfo.SaveAndEditCustomUserPlayerValues || (File.Exists(valueFile) && counter != handlerInstance.currentGameInfo.CustomUserPlayerPrompts.Length))
            {

                if ((File.Exists(valueFile) && !handlerInstance.currentGameInfo.SaveAndEditCustomUserPlayerValues && !handlerInstance.currentGameInfo.SaveCustomUserPlayerValues) || (File.Exists(valueFile) && counter != handlerInstance.currentGameInfo.CustomUserPlayerPrompts.Length))
                {
                    handlerInstance.Log("Deleting value file");
                    File.Delete(valueFile);
                }

                if (!Directory.Exists(Path.GetDirectoryName(valueFile)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(valueFile));
                }

                for (int d = 0; d < handlerInstance.currentGameInfo.CustomUserPlayerPrompts.Length; d++)
                {
                    handlerInstance.Log(string.Format("Prompt {0}/{1}: {2}", (d + 1), handlerInstance.currentGameInfo.CustomUserPlayerPrompts.Length, handlerInstance.currentGameInfo.CustomUserPlayerPrompts[d]));
                    string prevAnswer = "";
                    if (d < customValue.Length && File.Exists(valueFile))
                    {
                        prevAnswer = customValue[d];
                    }
                    Forms.CustomPrompt prompt = new Forms.CustomPrompt(handlerInstance.currentGameInfo.CustomUserPlayerPrompts[d], prevAnswer, d);
                    prompt.ShowDialog();
                    handlerInstance.context.CustomUserPlayerValues[d] = customValue[d];
                    handlerInstance.Log("User entered: " + customValue[d]);
                }

                using (StreamWriter outputFile = new StreamWriter(valueFile))
                {
                    foreach (string line in customValue)
                    {
                        outputFile.WriteLine(line);
                    }
                }
            }
        }

        public static void CustomUserInstancePrompts(PlayerInfo player)
        {
            var handlerInstance = GenericGameHandler.Instance;

            if (handlerInstance.context.CustomUserInstanceValues == null || handlerInstance.context.CustomUserInstanceValues?.Length < 1)
            {
                handlerInstance.context.CustomUserInstanceValues = new string[handlerInstance.currentGameInfo.CustomUserInstancePrompts.Length];
            }
            if (customValue == null || customValue?.Length < 1)
            {
                customValue = new string[handlerInstance.currentGameInfo.CustomUserInstancePrompts.Length];
            }

            string valueFile = Path.Combine(GameManager.Instance.GetJsScriptsPath(), Path.GetFileNameWithoutExtension(handlerInstance.currentGameInfo.JsFileName) + "\\instance " + player.PlayerID + "\\custom_inst_values.txt");

            int counter = 0;
            if (handlerInstance.currentGameInfo.SaveCustomUserInstanceValues || handlerInstance.currentGameInfo.SaveAndEditCustomUserInstanceValues)
            {
                handlerInstance.Log("Handler uses custom instance values");
                if (File.Exists(valueFile))
                {
                    handlerInstance.Log("custom_inst_values.txt already exists for this player, setting values accordingly");

                    string line;

                    StreamReader file = new StreamReader(valueFile);
                    while ((line = file.ReadLine()) != null)
                    {
                        handlerInstance.Log(string.Format("Custom value {0}: {1}", counter, line));
                        customValue[counter] = line;
                        handlerInstance.context.CustomUserInstanceValues[counter] = line;
                        counter++;
                    }

                    file.Close();

                    if (counter != handlerInstance.currentGameInfo.CustomUserInstancePrompts.Length)
                    {
                        handlerInstance.Log("Number of lines in file do not match number of prompts. Overwriting file");
                    }
                }
                else
                {
                    handlerInstance.Log("custom_inst_values.txt does not exist for player " + player.Nickname + ". Creating new file at " + valueFile);
                }
            }

            if (!File.Exists(valueFile) || !handlerInstance.currentGameInfo.SaveCustomUserInstanceValues || handlerInstance.currentGameInfo.SaveAndEditCustomUserInstanceValues || (File.Exists(valueFile) && counter != handlerInstance.currentGameInfo.CustomUserInstancePrompts.Length))
            {
                if ((File.Exists(valueFile) && !handlerInstance.currentGameInfo.SaveAndEditCustomUserInstanceValues && !handlerInstance.currentGameInfo.SaveCustomUserInstanceValues) || (File.Exists(valueFile) && counter != handlerInstance.currentGameInfo.CustomUserInstancePrompts.Length))
                {
                    handlerInstance.Log("Deleting value file");
                    File.Delete(valueFile);
                }

                if (!Directory.Exists(Path.GetDirectoryName(valueFile)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(valueFile));
                }

                for (int d = 0; d < handlerInstance.currentGameInfo.CustomUserInstancePrompts.Length; d++)
                {
                    handlerInstance.Log(string.Format("Prompt {0}/{1}: {2}", (d + 1), handlerInstance.currentGameInfo.CustomUserInstancePrompts.Length, handlerInstance.currentGameInfo.CustomUserInstancePrompts[d]));
                    string prevAnswer = "";
                    if (d < customValue.Length && File.Exists(valueFile))
                    {
                        prevAnswer = customValue[d];
                    }
                    Forms.CustomPrompt prompt = new Forms.CustomPrompt(handlerInstance.currentGameInfo.CustomUserInstancePrompts[d], prevAnswer, d);
                    prompt.ShowDialog();
                    handlerInstance.context.CustomUserInstanceValues[d] = customValue[d];
                    handlerInstance.Log("User entered: " + customValue[d]);
                }

                using (StreamWriter outputFile = new StreamWriter(valueFile))
                {
                    foreach (string line in customValue)
                    {
                        outputFile.WriteLine(line);
                    }
                }
            }
        }
    }
}
