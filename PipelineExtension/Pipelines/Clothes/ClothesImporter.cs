using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Microsoft.Xna.Framework.Content.Pipeline;
using Newtonsoft.Json.Linq;
using PipelineExtension.Characters;
using PipelineExtension.IntermediateFiles;
using TOutput = System.String;

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

    [ContentImporter(".cloth", DisplayName = "Cloth Importer", DefaultProcessor = "ClothProcessor")]
    public class ClothImporter : ContentImporter<ClothingText>
    {
        public override ClothingText Import(string filename, ContentImporterContext context)
        {
                return ((JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(File.ReadAllText(filename))).ToObject<ClothingText>();
        }
    }

}
