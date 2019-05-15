using System.IO;
using Microsoft.Xna.Framework.Content.Pipeline;
using Newtonsoft.Json.Linq;
using PipelineExtension.IntermediateFiles;
using PipelineExtension.Pipelines.Professions;

namespace PipelineExtension
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to import a file from disk into the specified type, TImport.
    ///
    /// This should be part of a Content Pipeline Extension Library project.
    ///
    /// TODO: change the ContentImporter attribute to specify the correct file
    /// extension, display name, and default processor for this importer.
    /// </summary>

    [ContentImporter(".prf", DisplayName = "Profession Importer", DefaultProcessor = "ProfessionProcessor")]
    public class ProfessionImporter : ContentImporter<ProfessionIntermediateText>
    {
        public override ProfessionIntermediateText Import(string filename, ContentImporterContext context)
        {
                return ((JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(File.ReadAllText(filename))).ToObject<ProfessionIntermediateText>();
        }
    }

}
