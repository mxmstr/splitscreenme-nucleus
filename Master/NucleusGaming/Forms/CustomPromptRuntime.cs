using Nucleus.Gaming.Coop;
using System.IO;

namespace Nucleus.Gaming.Forms
{
    public static class CustomPromptRuntime
    {
        public static string[] customValue;

        public static void CustomUserGeneralPrompts(GenericGameHandler genericGameHandler, GenericGameInfo gen, GenericContext context, PlayerInfo player)
        {
            if (context.CustomUserGeneralValues == null || context.CustomUserGeneralValues?.Length < 1)
            {
                context.CustomUserGeneralValues = new string[gen.CustomUserGeneralPrompts.Length];
            }
            if (customValue == null || customValue?.Length < 1)
            {
                customValue = new string[gen.CustomUserGeneralPrompts.Length];
            }

            for (int c = 0; c < context.CustomUserGeneralValues.Length; c++)
            {
                context.CustomUserGeneralValues[c] = null;
            }

            string valueFile = Path.Combine(GameManager.Instance.GetJsScriptsPath(), Path.GetFileNameWithoutExtension(gen.JsFileName) + "\\custom_gen_values.txt");

            int counter = 0;
            if (gen.SaveCustomUserGeneralValues || gen.SaveAndEditCustomUserGeneralValues || player.PlayerID > 0)
            {
                genericGameHandler.Log("Handler uses custom general values");
                if (File.Exists(valueFile))
                {
                    genericGameHandler.Log("custom_gen_values.txt already exists for this handler, setting values accordingly");

                    string line;

                    StreamReader file = new StreamReader(valueFile);
                    while ((line = file.ReadLine()) != null)
                    {
                        genericGameHandler.Log(string.Format("Custom value {0}: {1}", counter, line));
                        customValue[counter] = line;
                        context.CustomUserGeneralValues[counter] = line;
                        counter++;
                    }

                    file.Close();

                    if (counter != gen.CustomUserGeneralPrompts.Length)
                    {
                        genericGameHandler.Log("Number of lines in file do not match number of prompts. Overwriting file");
                    }
                }
                else if (player.PlayerID == 0)
                {
                    genericGameHandler.Log("custom_gen_values.txt does not exist. Creating new file at " + valueFile);
                }
            }
            else if (File.Exists(valueFile) && player.PlayerID == 0 && !gen.SaveCustomUserGeneralValues && !gen.SaveAndEditCustomUserGeneralValues)
            {
                genericGameHandler.Log("Deleting value file");
                File.Delete(valueFile);
            }

            if (player.PlayerID == 0 && (!File.Exists(valueFile) || !gen.SaveCustomUserGeneralValues || gen.SaveAndEditCustomUserGeneralValues || (File.Exists(valueFile) && counter != gen.CustomUserGeneralPrompts.Length)))
            {

                if (player.PlayerID == 0 && ((File.Exists(valueFile) && !gen.SaveAndEditCustomUserGeneralValues && !gen.SaveCustomUserGeneralValues) || (File.Exists(valueFile) && counter != gen.CustomUserGeneralPrompts.Length)))
                {
                    genericGameHandler.Log("Deleting value file");
                    File.Delete(valueFile);
                }

                if (!Directory.Exists(Path.Combine(GameManager.Instance.GetJsScriptsPath(), Path.GetFileNameWithoutExtension(gen.JsFileName))))
                {
                    Directory.CreateDirectory(Path.Combine(GameManager.Instance.GetJsScriptsPath(), Path.GetFileNameWithoutExtension(gen.JsFileName)));
                }

                bool containsValue = false;
                for (int d = 0; d < gen.CustomUserGeneralPrompts.Length; d++)
                {
                    genericGameHandler.Log(string.Format("Prompt {0}/{1}: {2}", (d + 1), gen.CustomUserGeneralPrompts.Length, gen.CustomUserGeneralPrompts[d]));
                    string prevAnswer = "";
                    if (d < customValue.Length && File.Exists(valueFile))
                    {
                        prevAnswer = customValue[d];
                    }
                    Forms.CustomPrompt prompt = new Forms.CustomPrompt(gen.CustomUserGeneralPrompts[d], prevAnswer, d);
                    prompt.ShowDialog();
                    if (customValue[d]?.Length > 0)
                    {
                        context.CustomUserGeneralValues[d] = customValue[d];
                        genericGameHandler.Log("User entered: " + customValue[d]);
                        if (!containsValue)
                        {
                            containsValue = true;
                        }
                    }
                    else
                    {
                        genericGameHandler.Log("User did not enter a value for this prompt");
                        context.CustomUserGeneralValues[d] = null;
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

        public static void CustomUserPlayerPrompts(GenericGameHandler genericGameHandler, GenericGameInfo gen, GenericContext context, PlayerInfo player)
        {
            if (context.CustomUserPlayerValues == null || context.CustomUserPlayerValues?.Length < 1)
            {
                context.CustomUserPlayerValues = new string[gen.CustomUserPlayerPrompts.Length];
            }
            if (customValue == null || customValue?.Length < 1)
            {
                customValue = new string[gen.CustomUserPlayerPrompts.Length];
            }

            string valueFile = Path.Combine(GameManager.Instance.GetJsScriptsPath(), Path.GetFileNameWithoutExtension(gen.JsFileName) + "\\" + player.Nickname + "\\custom_plyr_values.txt");

            int counter = 0;
            if (gen.SaveCustomUserPlayerValues || gen.SaveAndEditCustomUserPlayerValues)
            {
                genericGameHandler.Log("Handler uses custom player values");
                if (File.Exists(valueFile))
                {
                    genericGameHandler.Log("custom_plyr_values.txt already exists for this player, setting values accordingly");

                    string line;

                    StreamReader file = new StreamReader(valueFile);
                    while ((line = file.ReadLine()) != null)
                    {
                        genericGameHandler.Log(string.Format("Custom value {0}: {1}", counter, line));
                        customValue[counter] = line;
                        context.CustomUserPlayerValues[counter] = line;
                        counter++;
                    }

                    file.Close();

                    if (counter != gen.CustomUserPlayerPrompts.Length)
                    {
                        genericGameHandler.Log("Number of lines in file do not match number of prompts. Overwriting file");
                    }
                }
                else
                {
                    genericGameHandler.Log("custom_plyr_values.txt does not exist for player " + player.Nickname + ". Creating new file at " + valueFile);
                }
            }

            if (!File.Exists(valueFile) || !gen.SaveCustomUserPlayerValues || gen.SaveAndEditCustomUserPlayerValues || (File.Exists(valueFile) && counter != gen.CustomUserPlayerPrompts.Length))
            {

                if ((File.Exists(valueFile) && !gen.SaveAndEditCustomUserPlayerValues && !gen.SaveCustomUserPlayerValues) || (File.Exists(valueFile) && counter != gen.CustomUserPlayerPrompts.Length))
                {
                    genericGameHandler.Log("Deleting value file");
                    File.Delete(valueFile);
                }

                if (!Directory.Exists(Path.GetDirectoryName(valueFile)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(valueFile));
                }

                for (int d = 0; d < gen.CustomUserPlayerPrompts.Length; d++)
                {
                    genericGameHandler.Log(string.Format("Prompt {0}/{1}: {2}", (d + 1), gen.CustomUserPlayerPrompts.Length, gen.CustomUserPlayerPrompts[d]));
                    string prevAnswer = "";
                    if (d < customValue.Length && File.Exists(valueFile))
                    {
                        prevAnswer = customValue[d];
                    }
                    Forms.CustomPrompt prompt = new Forms.CustomPrompt(gen.CustomUserPlayerPrompts[d], prevAnswer, d);
                    prompt.ShowDialog();
                    context.CustomUserPlayerValues[d] = customValue[d];
                    genericGameHandler.Log("User entered: " + customValue[d]);
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

        public static void CustomUserInstancePrompts(GenericGameHandler genericGameHandler, GenericGameInfo gen, GenericContext context, PlayerInfo player)
        {

            if (context.CustomUserInstanceValues == null || context.CustomUserInstanceValues?.Length < 1)
            {
                context.CustomUserInstanceValues = new string[gen.CustomUserInstancePrompts.Length];
            }
            if (customValue == null || customValue?.Length < 1)
            {
                customValue = new string[gen.CustomUserInstancePrompts.Length];
            }

            string valueFile = Path.Combine(GameManager.Instance.GetJsScriptsPath(), Path.GetFileNameWithoutExtension(gen.JsFileName) + "\\instance " + player.PlayerID + "\\custom_inst_values.txt");

            int counter = 0;
            if (gen.SaveCustomUserInstanceValues || gen.SaveAndEditCustomUserInstanceValues)
            {
                genericGameHandler.Log("Handler uses custom instance values");
                if (File.Exists(valueFile))
                {
                    genericGameHandler.Log("custom_inst_values.txt already exists for this player, setting values accordingly");

                    string line;

                    StreamReader file = new StreamReader(valueFile);
                    while ((line = file.ReadLine()) != null)
                    {
                        genericGameHandler.Log(string.Format("Custom value {0}: {1}", counter, line));
                        customValue[counter] = line;
                        context.CustomUserInstanceValues[counter] = line;
                        counter++;
                    }

                    file.Close();

                    if (counter != gen.CustomUserInstancePrompts.Length)
                    {
                        genericGameHandler.Log("Number of lines in file do not match number of prompts. Overwriting file");
                    }
                }
                else
                {
                    genericGameHandler.Log("custom_inst_values.txt does not exist for player " + player.Nickname + ". Creating new file at " + valueFile);
                }
            }

            if (!File.Exists(valueFile) || !gen.SaveCustomUserInstanceValues || gen.SaveAndEditCustomUserInstanceValues || (File.Exists(valueFile) && counter != gen.CustomUserInstancePrompts.Length))
            {
                if ((File.Exists(valueFile) && !gen.SaveAndEditCustomUserInstanceValues && !gen.SaveCustomUserInstanceValues) || (File.Exists(valueFile) && counter != gen.CustomUserInstancePrompts.Length))
                {
                    genericGameHandler.Log("Deleting value file");
                    File.Delete(valueFile);
                }

                if (!Directory.Exists(Path.GetDirectoryName(valueFile)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(valueFile));
                }

                for (int d = 0; d < gen.CustomUserInstancePrompts.Length; d++)
                {
                    genericGameHandler.Log(string.Format("Prompt {0}/{1}: {2}", (d + 1), gen.CustomUserInstancePrompts.Length, gen.CustomUserInstancePrompts[d]));
                    string prevAnswer = "";
                    if (d < customValue.Length && File.Exists(valueFile))
                    {
                        prevAnswer = customValue[d];
                    }
                    Forms.CustomPrompt prompt = new Forms.CustomPrompt(gen.CustomUserInstancePrompts[d], prevAnswer, d);
                    prompt.ShowDialog();
                    context.CustomUserInstanceValues[d] = customValue[d];
                    genericGameHandler.Log("User entered: " + customValue[d]);
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
