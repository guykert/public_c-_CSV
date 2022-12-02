
using scanner.csv.Data.logica;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;

namespace scanner.csv.framework
{
    class Evaluator_csv
    {

        /// <summary>
        /// Numero minimo de pixeles negros para tomarse como una respuesta elegida
        /// </summary>
        private int limit = 100;


        /// <summary>
        /// Margen de error para el minimo de pixeles
        /// </summary>
        private int limitMarginDiff = 8;


        /// <summary>
        /// Metodo principal para la evaluacion de un examen
        /// </summary>
        /// <param name="testID">identificador del examen a evaluar</param>
        /// <param name="alert">true/false si existen alertas generadas en la evaluacion</param>
        /// <param name="alertMessage">mensaje de alerta</param>
        /// <param name="action">accion que se utiliza para actualizar la barra de progreso</param>
        public void Evaluate_csv(int testID, int selectedCurso, ref List<PruebaAlumnoEnviarWeb> listPruebaAlumnoEnviarWeb, List<Bitmap> pruebasBitmaps, ref int errores_al_evaluar)
        {

            //var templateWebID = Convert.ToInt32(System.Windows.Application.Current.Resources["template_predeterminado"]);

            ////MessageBox.Show("templateID. " + templateID);

            //var template = TemplateDB.buscarIdWeb(templateWebID);

            ////MessageBox.Show("template.id : " + template.id);

            //var elements = RegionDB.dataObjetos(template.id);


            //foreach (var imagen in pruebasBitmaps)
            //{
            //    // este objeto guardará la información para la web de una hoja
            //    var pruebaAlumnoEnviarWeb = new PruebaAlumnoEnviarWeb();


            //    // asigno el id e la prueba

            //    pruebaAlumnoEnviarWeb.prueba_id = testID;

            //    pruebaAlumnoEnviarWeb.curso_id = selectedCurso;

            //    // Leo el rut 

            //    try
            //    {

            //        var prueba_alumno_id = leerRutNuevo(elements, imagen, template.limiteMinimoCirculo, template.id, template.cuadrados, ref pruebaAlumnoEnviarWeb, ref errores_al_evaluar);

            //        if (errores_al_evaluar != 1)
            //        {

            //            LeerRespuestasNuevo(elements, imagen, template.limiteMinimoCirculo, template.id, template.cuadrados, prueba_alumno_id, ref pruebaAlumnoEnviarWeb, ref errores_al_evaluar);

            //        }



            //    }
            //    catch (COMException ex)
            //    {
            //        MessageBox.Show("Problema al leer la hoja : " + ex.Message);
            //    }



            //    listPruebaAlumnoEnviarWeb.Add(pruebaAlumnoEnviarWeb);

            //}




            //pruebasBitmaps.Clear();

        }

        private void LeerRespuestasNuevo(IEnumerable<csvRegionModel> elements, Bitmap testImage, int limit, int templateID, int cuadrados, int prueba_alumno_id, ref PruebaAlumnoEnviarWeb pruebaAlumnoEnviarWeb, ref int errores_al_evaluar)
        {

            //var respuestas = new List<csvLecturaAlternativas2>();

            //var respuestasMayores = new List<csvLecturaAlternativas2>();

            //var CsvSubRegionModelWeb = new List<csvSubRegionModelWeb>();

            //// get all questions (TYPE 2)
            //foreach (var element in elements.Where(e => e.tipo_elemento_id == 2))
            //{

            //    // obtiene coordenadas de la region
            //    var x = Convert.ToInt32(element.x);
            //    var y = Convert.ToInt32(element.y);
            //    var width = Convert.ToInt32(element.width);
            //    var height = Convert.ToInt32(element.height);


            //    // obtiene todas las opciones del ractivo/identificador
            //    var opts = SubRegionDB.subRegiones(element.id);

            //    // margen de error
            //    var limitDiff = limitMarginDiff;

            //    // Busca la opcion que fue marcada
            //    var optList = new Dictionary<string, long>();
            //    var foundOption = false;
            //    var selectedOptions = new Dictionary<string, long>();

            //    //MessageBox.Show("element.nombre . " + element.nombre + "   Width . " + testImage.Width + "   Height . " + testImage.Height + "  x . " + x + "  y . " + y + "  xfinal . " + (x + width) + "  yfinal . " + (y + height) + "  Width_imagen . " + testImage.Width + "  Width_imagen . " + testImage.Height);


            //    if ((x + width) > testImage.Width || (y + height) > testImage.Height)
            //    {

            //        MessageBox.Show("Error Existe un problema ya uqe el template excede el tamaño de la imagen usada");

            //        errores_al_evaluar = 1;

            //        return;

            //    }

            //    Bitmap questionImage = testImage.Clone(new Rectangle(x, y, width, height), testImage.PixelFormat);

            //    foreach (var opt in opts)
            //    {



            //        var x1 = Convert.ToInt32(opt.x);
            //        var y1 = Convert.ToInt32(opt.y);
            //        var w1 = Convert.ToInt32(opt.width);
            //        var h1 = Convert.ToInt32(opt.height);

            //        if (cuadrados != 1)
            //        {
            //            // reducen aun mas el tamanho de la opcion
            //            if (w1 - 4 > 0 && h1 - 4 > 0)
            //            {
            //                x1 += 2;
            //                y1 += 2;
            //                w1 -= 2;
            //                h1 -= 2;
            //                limitDiff -= 2;
            //            }

            //            if (w1 > width) w1 = width;
            //            if (h1 > height) h1 = height;

            //            if (x1 + w1 > width && x1 - (x1 + w1 - width) >= 0)
            //            {
            //                var difx = x1 + w1 - width;
            //                x1 -= difx;
            //            }

            //            if (y1 + h1 > height && y1 - (y1 + h1 - height) >= 0)
            //            {
            //                var dify = y1 + h1 - height;
            //                y1 -= dify;
            //            }
            //        }



            //        var answerImage = questionImage.Clone(new Rectangle(x1, y1, w1, h1), questionImage.PixelFormat);
            //        var hashCurrent = encuentraLosPixelesMarcados(answerImage);

            //        //determine the number of equal pixel (x of 256)
            //        int blackElements = hashCurrent.Count(r => r == true);

            //        //MessageBox.Show("opt.nombre : " + opt.nombre + "    blackElements : " + blackElements);
            //        optList.Add(opt.nombre, blackElements);

            //        //MessageBox.Show("blackElements : " + blackElements + "    limit : " + limit + "    limitDiff : " + limitDiff + "    (blackElements > limit): " + (blackElements > limit) + "    ((limit - blackElements) < limitDiff || ((limit - blackElements - limitDiff) < 3)): " + ((limit - blackElements) < limitDiff || ((limit - blackElements - limitDiff) < 3)));

            //        // cuando es pregunta, debe validar si se toma como correcta
            //        var result = (blackElements > limit) || ((limit - blackElements) < limitDiff || ((limit - blackElements - limitDiff) < 3));



            //        if (result)
            //        {

            //            // Si se toma como correcta, se agrega a una lista de opciones 'marcadas'
            //            foundOption = true;
            //            selectedOptions.Add(opt.valor, blackElements);

            //            var un = new csvLecturaAlternativas2
            //            {
            //                valor = opt.valor.ToString(),
            //                nombre = element.nombre.ToString(),
            //                id = Convert.ToInt32(element.id),
            //                blackElements = Convert.ToInt32(blackElements),

            //            };


            //            respuestas.Add(un);
            //        }

            //        answerImage.Dispose();
            //    }


            //    questionImage.Dispose();


            //}



            //pruebaAlumnoEnviarWeb.regionesFinales = CsvSubRegionModelWeb;



            //var respuestasG = respuestas.GroupBy(rest => rest.nombre);

            //foreach (var respG in respuestasG)
            //{

            //    var itemMaxHeight = respG.Max(y => y.blackElements);


            //    var itemsMax = respG.First(number => number.blackElements == itemMaxHeight);



            //    var un = new csvLecturaAlternativas2
            //    {
            //        valor = itemsMax.valor.ToString(),
            //        nombre = itemsMax.nombre.ToString(),
            //        id = Convert.ToInt32(itemsMax.id),
            //        blackElements = Convert.ToInt32(itemsMax.blackElements),

            //    }; //Console.WriteLine(newP.ToString());

            //    //MessageBox.Show("itemsMax.valor.ToString() . " + itemsMax.valor.ToString() + "  itemsMax.nombre.ToString() . " + itemsMax.nombre.ToString());

            //    respuestasMayores.Add(itemsMax);


            //}


            //pruebaAlumnoEnviarWeb.respuestasMayores = respuestasMayores;

            //pruebaAlumnoEnviarWeb.regionesFinales = CsvSubRegionModelWeb;


        }



        private int leerRutNuevo(IEnumerable<csvRegionModel> elements, Bitmap testImage, int limit, int templateID, int cuadrados, ref PruebaAlumnoEnviarWeb pruebaAlumnoEnviarWeb, ref int errores_al_evaluar)
        {

            var respuestas = new List<csvLecturaAlternativas2>();

            var respuestasMayores = new List<csvLecturaAlternativas2>();

            var CsvSubRegionModelWeb = new List<csvSubRegionModelWeb>();

            var prueba_alumno_id = 0;




            //// get all questions (TYPE 2)
            //foreach (var element in elements.Where(e => e.tipo_elemento_id == 1))
            //{

            //    // obtiene coordenadas de la region
            //    var x = Convert.ToInt32(element.x);
            //    var y = Convert.ToInt32(element.y);
            //    var width = Convert.ToInt32(element.width);
            //    var height = Convert.ToInt32(element.height);

            //    // obtiene todas las opciones del ractivo/identificador
            //    var opts = SubRegionDB.subRegiones(element.id);

            //    // margen de error
            //    var limitDiff = limitMarginDiff;

            //    // Busca la opcion que fue marcada
            //    var optList = new Dictionary<string, long>();
            //    var foundOption = false;
            //    var selectedOptions = new Dictionary<string, long>();

            //    if ((x + width) > testImage.Width || (y + height) > testImage.Height)
            //    {

            //        MessageBox.Show("Error Existe un problema ya que el template excede el tamaño de la imagen usada");

            //        errores_al_evaluar = 1;

            //        return prueba_alumno_id;

            //    }

            //    var questionImage = testImage.Clone(new Rectangle(x, y, width, height), testImage.PixelFormat);

            //    //MessageBox.Show("element.nombre . " + element.nombre + "   Width . " + testImage.Width + "   Height . " + testImage.Height + "  x . " + x + "  y . " + y + "  xfinal . " +  (x + width) + "  yfinal . " + (y + height));

            //    foreach (var opt in opts)
            //    {



            //        var x1 = Convert.ToInt32(opt.x);
            //        var y1 = Convert.ToInt32(opt.y);
            //        var w1 = Convert.ToInt32(opt.width);
            //        var h1 = Convert.ToInt32(opt.height);

            //        if (cuadrados != 1)
            //        {
            //            // reducen aun mas el tamanho de la opcion
            //            if (w1 - 4 > 0 && h1 - 4 > 0)
            //            {
            //                x1 += 2;
            //                y1 += 2;
            //                w1 -= 2;
            //                h1 -= 2;
            //                limitDiff -= 2;
            //            }

            //            if (w1 > width) w1 = width;
            //            if (h1 > height) h1 = height;

            //            if (x1 + w1 > width && x1 - (x1 + w1 - width) >= 0)
            //            {
            //                var difx = x1 + w1 - width;
            //                x1 -= difx;
            //            }

            //            if (y1 + h1 > height && y1 - (y1 + h1 - height) >= 0)
            //            {
            //                var dify = y1 + h1 - height;
            //                y1 -= dify;
            //            }
            //        }

            //        var answerImage = questionImage.Clone(new Rectangle(x1, y1, w1, h1), questionImage.PixelFormat);
            //        var hashCurrent = encuentraLosPixelesMarcados(answerImage);

            //        //determine the number of equal pixel (x of 256)
            //        int blackElements = hashCurrent.Count(r => r == true);
            //        //MessageBox.Show("opt.nombre : " + opt.nombre + "    blackElements : " + blackElements);
            //        optList.Add(opt.nombre, blackElements);

            //        //MessageBox.Show("blackElements : " + blackElements + "    limit : " + limit + "    limitDiff : " + limitDiff + "    (blackElements > limit): " + (blackElements > limit) + "    ((limit - blackElements) < limitDiff || ((limit - blackElements - limitDiff) < 3)): " + ((limit - blackElements) < limitDiff || ((limit - blackElements - limitDiff) < 3)));

            //        // cuando es pregunta, debe validar si se toma como correcta
            //        var result = (blackElements > limit) || ((limit - blackElements) < limitDiff || ((limit - blackElements - limitDiff) < 3));



            //        if (result)
            //        {

            //            // Si se toma como correcta, se agrega a una lista de opciones 'marcadas'
            //            foundOption = true;
            //            selectedOptions.Add(opt.valor, blackElements);

            //            var un = new csvLecturaAlternativas2
            //            {
            //                valor = opt.valor.ToString(),
            //                nombre = element.nombre.ToString(),
            //                id = Convert.ToInt32(element.id),
            //                blackElements = Convert.ToInt32(blackElements),

            //            };


            //            respuestas.Add(un);
            //        }

            //        answerImage.Dispose();

            //    }


            //    questionImage.Dispose();


            //}



            //pruebaAlumnoEnviarWeb.regionesFinales = CsvSubRegionModelWeb;



            //var respuestasG = respuestas.GroupBy(rest => rest.nombre);

            //foreach (var respG in respuestasG)
            //{

            //    var itemMaxHeight = respG.Max(y => y.blackElements);


            //    var itemsMax = respG.First(number => number.blackElements == itemMaxHeight);



            //    var un = new csvLecturaAlternativas2
            //    {
            //        valor = itemsMax.valor.ToString(),
            //        nombre = itemsMax.nombre.ToString(),
            //        id = Convert.ToInt32(itemsMax.id),
            //        blackElements = Convert.ToInt32(itemsMax.blackElements),

            //    }; //Console.WriteLine(newP.ToString());

            //    respuestasMayores.Add(itemsMax);


            //}

            //var formulas_datos = this.GetFormulaValue(templateID, respuestasMayores);

            ////Conformo la cantidad de formulas

            //pruebaAlumnoEnviarWeb.formulas_datos = formulas_datos;

            ////MessageBox.Show("formulas_datos : " + formulas_datos.Count);

            //if (formulas_datos.Count > 0)
            //{

            //    var primera_formula = formulas_datos.First();

            //    Application.Current.Resources["MessageLog"] = "Datos Procesados : " + primera_formula.nombre;

            //    pruebaAlumnoEnviarWeb.primera_formula = primera_formula.nombre;

            //    //MessageBox.Show("formulas_datos : " + pruebaAlumnoEnviarWeb.primera_formula);


            //}

            return prueba_alumno_id;

        }



        public void Evaluate_csv_romper(int testID, string selectedPruebaNombre, ref List<PruebaAlumnoEnviarWeb> listPruebaAlumnoEnviarWeb)
        {


            //    //BuscarRespuestasPrueba(elements, testImage, selectedPruebaNombre, template.limiteMinimoCirculo, ref sum, templateID, prueba_alumno_id, ref pruebaAlumnoEnviarWeb, fileInfo.Name.ToString());


            //    // calcula el promedio
            //    var testResult = totalReactives > 0 ? (sum * 100) / totalReactives : 0;

            //}
            //if (exception != null) throw exception;



            //listPruebaAlumnoEnviarWeb.Add(pruebaAlumnoEnviarWeb);





        }




        public void Evaluate_csv_archivos_guardados(int testID, string selectedPruebaNombre, ref List<PruebaAlumnoEnviarWeb> listPruebaAlumnoEnviarWeb, out bool alert, out string alertMessage, Action<string> action)
        {



            alert = false;
            alertMessage = string.Empty;

            // obtiene examen
            //var test = DataHelper.GetTest(testID);

            //MessageBox.Show("testID . " + testID);

            //MessageBox.Show("listPruebaAlumnoEnviarWeb . " + listPruebaAlumnoEnviarWeb.Count());

            //var test = EscanearDB.buscarId(testID);

            //MessageBox.Show("Evaluate_csv 2. " + (test != null));

            //if (test != null)
            //{
            // obtiene la plantilla y sus regiones


            var templateID = Convert.ToInt32(System.Windows.Application.Current.Resources["template_predeterminado"]);

            //MessageBox.Show("templateID. " + templateID);

            var template = TemplateDB.buscarIdWeb(templateID);

            templateID = template.id;

            //MessageBox.Show("template.id : " + template.id);


            var elements = RegionDB.dataObjetos(template.id);



            var files = Directory.GetFiles(@"imagenes_escaneadas", "*.*");



            //MessageBox.Show("Evaluate_csv 2. " + (test != null));
            //MessageBox.Show("Error evaluando el examen: " + elements);

            //if (test.id > 0)
            //{
            //    files = Directory.GetFiles(test.carpeta, "*.*");
            //}

            // obtiene todos los archivos en la carpeta de entrada
            // var files = Directory.GetFiles(test.carpeta, "*.*");
            //if (!File.Exists(template.imagen))
            //{
            //    throw new Exception("Imagen principal de la plantilla no existe [" + template.imagen + "]");
            //}

            // itera cada imagen en el folder            
            //foreach (var file in files)
            //{

            //    //MessageBox.Show("Error evaluando el examen: " + elements);
            //    var fileInfo = new FileInfo(file);

            //    //var fileInfo = new FileInfo(file);
            //    //var testName = fileInfo.Name.Replace(fileInfo.Extension, "");
            //    //if (fileInfo.Extension.ToLowerInvariant().Equals(".txt")) continue;

            //    //MessageBox.Show("Error evaluando el examen: " + testName);

            //    //var resultsFile = fileInfo.Name.Replace(fileInfo.Extension, ".txt");
            //    //var textFile = Path.Combine(test.carpeta, resultsFile);
            //    //var text = File.CreateText(textFile);

            //    //action(testName);

            //    //MessageBox.Show("fileInfo: " + fileInfo.ToString());

            //    var pruebaAlumnoEnviarWeb = new PruebaAlumnoEnviarWeb();


            //    //var optListCirculosGuias = new Dictionary<string, long>();

            //    Exception exception = null;
            //    using (var testImage = ImageHelper_csv.GetImage(template, file))
            //    {
            //        //try
            //        //{
            //        //MessageBox.Show("tomo la imagen " );

            //        // total de reactivos
            //        var totalReactives = elements.Count(e => e.tipo_elemento_id == 2);

            //        //MessageBox.Show("totalReactives :  elements.Count : " + totalReactives);


            //        //primero confirmo si en la tabla template tenemos guardada la cantidad de pixeles promedio para los circulos vacios

            //        // veo el total de circulos guía que existen : 


            //        var circulosGuia = elements.Count(e => e.tipo_elemento_id == 3);

            //        if (elements.Count(e => e.tipo_elemento_id == 3) > 0)
            //        {
            //            //template.limiteMinimoCirculo = EvaluateCircle(elements, testImage, test, templateID);
            //        }

            //        var sum = 0;

            //        //MessageBox.Show("totalReactives: " + totalReactives);


            //        var alerts = string.Empty;
            //        //EvaluateIdentifier(elements, testImage, test, ref text, limit, ref alerts);
            //        var prueba_alumno_id = leerRut(elements, testImage, selectedPruebaNombre, template.limiteMinimoCirculo, ref sum, ref alerts, templateID, ref pruebaAlumnoEnviarWeb, fileInfo.Name.ToString());

            //        //MessageBox.Show("prueba_alumno_id : " + prueba_alumno_id);

            //        //BuscarRespuestasPrueba(elements, testImage, selectedPruebaNombre, template.limiteMinimoCirculo, ref sum, templateID, prueba_alumno_id, ref pruebaAlumnoEnviarWeb, fileInfo.Name.ToString());






            //        //if (!string.IsNullOrEmpty(alerts))
            //        //{
            //        //    alert = true;
            //        //    alertMessage += $"Examen {testName}:\r\n{alerts}\r\n";
            //        //}

            //        // calcula el promedio
            //        var testResult = totalReactives > 0 ? (sum * 100) / totalReactives : 0;
            //        //text.WriteLine("Result=" + testResult.ToString("000.00"));

            //        //MessageBox.Show("alerts: " + alerts);
            //        //text.Close();
            //        //}
            //        //catch (Exception err)
            //        //{
            //        //    exception = err;
            //        //}
            //    }
            //    if (exception != null) throw exception;



            //    listPruebaAlumnoEnviarWeb.Add(pruebaAlumnoEnviarWeb);
            //    // Obtiene las formulas que necesita calcular y las adjunta al archivo de revision
            //    //var formulaString = new List<string>();
            //    //var lines = GetLinesDictionary(textFile);
            //    //foreach (var formula in DataHelper.GetFormulas(templateID))
            //    //{
            //    //    var formulaResult = FormulaHelper.GetFormulaValue(formula, lines);
            //    //    formulaString.Add(formulaResult);
            //    //}
            //    //File.AppendAllLines(textFile, formulaString);




            //} // files      


            //string directoryPath = @"imagenes_escaneadas";

            //Directory.GetFiles(directoryPath).ToList().ForEach(File.Delete);
            //Directory.GetDirectories(directoryPath).ToList().ForEach(Directory.Delete);



            //}
        }


        /// <summary>
        /// Ejecuta la evaluacion de todas las preguntas de un examen
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="testImage"></param>
        /// <param name="test"></param>
        /// <param name="text"></param>
        /// <param name="limit"></param>
        /// <param name="sum"></param>
        /// <param name="alertMessage"></param>
        private void BuscarRespuestasPrueba(IEnumerable<csvRegionModel> elements, Bitmap testImage, string test, int limit, ref int sum, int templateID, int prueba_alumno_id, ref PruebaAlumnoEnviarWeb pruebaAlumnoEnviarWeb, string nombre_archivo_origen)
        {

            var respuestas = new List<csvLecturaAlternativas2>();

            var respuestasMayores = new List<csvLecturaAlternativas2>();

            var CsvSubRegionModelWeb = new List<csvSubRegionModelWeb>();


            limit = limit + 40;


            //// get all questions (TYPE 2)
            //foreach (var element in elements.Where(e => e.tipo_elemento_id == 2))
            //{



            //    // obtiene coordenadas de la region
            //    var x = Convert.ToInt32(element.x);
            //    var y = Convert.ToInt32(element.y);
            //    var width = Convert.ToInt32(element.width);
            //    var height = Convert.ToInt32(element.height);


            //    // obtiene todas las opciones del ractivo/identificador
            //    var opts = SubRegionDB.subRegiones(element.id);



            //    // margen de error
            //    var limitDiff = limitMarginDiff;

            //    #region Opcion Marcada

            //    // Busca la opcion que fue marcada
            //    var optList = new Dictionary<string, long>();
            //    var foundOption = false;
            //    var selectedOptions = new Dictionary<string, long>();



            //    var questionImage = testImage.Clone(new Rectangle(x, y, width, height), testImage.PixelFormat);

            //    //var questionImageSave = testImage.Clone(new Rectangle(x, y, width, height), testImage.PixelFormat);



            //    //MessageBox.Show("test.carpeta : " + test.carpeta + "\\temp" + "    {test.nombre}_{element.nombre}_Q.png : " + $"{test.nombre}_{element.nombre}_Q.png");

            //    //var hashCurrentquestion = GetHash(questionImage, Path.Combine(test.carpeta + "\\temp", $"{test.nombre}_{element.nombre}_Q.png"));




            //    //questionImage.Save(@"imagenes_escaneadas\\" + $"{test.nombre}_{element.nombre}_QR.jpg", ImageFormat.Jpeg);

            //    //MessageBox.Show("questionImage : " + questionImage);

            //    //MessageBox.Show("System.IO.File.Exists + .jpg) : " + System.IO.File.Exists(@"C:\Temp\" + $"{test.nombre}_{element.nombre}_Q" + ".jpg"));

            //    //var nombre_archivo = @"C:\Temp\" + $"{test.nombre}_{element.nombre}_Q_{nombre_archivo_origen}";


            //    //if (System.IO.File.Exists(nombre_archivo))
            //    //{
            //    //    //MessageBox.Show("SI : ");

            //    //    System.IO.File.Delete(nombre_archivo);
            //    //}


            //    //questionImageSave.Save(nombre_archivo, ImageFormat.Jpeg);


            //    //questionImageSave.Dispose();

            //    var csvsubRegionModelWeb2 = new csvSubRegionModelWeb
            //    {
            //        id = element.id,
            //        nombre = element.nombre,
            //        descripcion = element.descripcion,
            //        x = element.x,
            //        y = element.y,
            //        width = element.width,
            //        height = element.height,
            //        imagen_Local = "imagen_Local",
            //        imagen_nombre = "imagen_nombre",

            //    };


            //    //pruebaAlumnoEnviarWeb.regionesFinales.Add(csvsubRegionModelWeb);

            //    CsvSubRegionModelWeb.Add(csvsubRegionModelWeb2);


            //    foreach (var opt in opts)
            //    {

            //        //MessageBox.Show("element : " + element.nombre);

            //        //MessageBox.Show("opt.x : " + opt.x + "    opt.y : " + opt.y + "opt.width : " + opt.width + "    opt.height : " + opt.height);

            //        var x1 = Convert.ToInt32(opt.x);
            //        var y1 = Convert.ToInt32(opt.y);
            //        var w1 = Convert.ToInt32(opt.width);
            //        var h1 = Convert.ToInt32(opt.height);

            //        // reducen aun mas el tamanho de la opcion
            //        if (w1 - 4 > 0 && h1 - 4 > 0)
            //        {
            //            x1 += 2;
            //            y1 += 2;
            //            w1 -= 2;
            //            h1 -= 2;
            //            limitDiff -= 2;
            //        }

            //        if (w1 > width) w1 = width;
            //        if (h1 > height) h1 = height;

            //        if (x1 + w1 > width && x1 - (x1 + w1 - width) >= 0)
            //        {
            //            var difx = x1 + w1 - width;
            //            x1 -= difx;
            //        }

            //        if (y1 + h1 > height && y1 - (y1 + h1 - height) >= 0)
            //        {
            //            var dify = y1 + h1 - height;
            //            y1 -= dify;
            //        }

            //        var answerImage = questionImage.Clone(new Rectangle(x1, y1, w1, h1), questionImage.PixelFormat);
            //        var hashCurrent = GetHash(answerImage, Path.Combine(@"imagenes_escaneadas", $"{test}_{element.nombre}_Q.png"));



            //        //determine the number of equal pixel (x of 256)
            //        int blackElements = hashCurrent.Count(r => r == true);
            //        optList.Add(opt.nombre, blackElements);

            //        //MessageBox.Show("blackElements : " + blackElements + "    limit : " + limit + "    limitDiff : " + limitDiff + "    (blackElements > limit): " + (blackElements > limit) + "    ((limit - blackElements) < limitDiff || ((limit - blackElements - limitDiff) < 3)): " + ((limit - blackElements) < limitDiff || ((limit - blackElements - limitDiff) < 3)));

            //        // cuando es pregunta, debe validar si se toma como correcta
            //        var result = (blackElements > limit);

            //        //var answerImageSave = questionImage.Clone(new Rectangle(x1, y1, w1, h1), questionImage.PixelFormat);
            //        //var nombre_archivo_sr = @"imagenes_escaneadas\\" + $"{test.nombre}_{element.nombre}_Q_{opt.id}_SQ_{nombre_archivo_origen}";


            //        //if (System.IO.File.Exists(nombre_archivo_sr))
            //        //{
            //        //    //MessageBox.Show("SI : ");

            //        //    System.IO.File.Delete(nombre_archivo_sr);
            //        //}


            //        //answerImageSave.Save(nombre_archivo_sr, ImageFormat.Jpeg);


            //        //answerImageSave.Dispose();


            //        if (result)
            //        {

            //            //MessageBox.Show("element.nombre: " + element.nombre + "blackElements : " + blackElements + "   result : " + result + "   limit : " + limit + "   opt.nombre : " + opt.nombre + "   opt.valor : " + opt.valor);
            //            // Si se toma como correcta, se agrega a una lista de opciones 'marcadas'
            //            foundOption = true;
            //            selectedOptions.Add(opt.valor, blackElements);

            //            var un = new csvLecturaAlternativas2
            //            {
            //                valor = opt.valor.ToString(),
            //                nombre = element.nombre.ToString(),
            //                id = Convert.ToInt32(element.id),
            //                blackElements = Convert.ToInt32(blackElements),

            //            };


            //            respuestas.Add(un);
            //        }
            //    }



            //    //foreach (var selectedOption in selectedOptions)
            //    //{
            //    //    MessageBox.Show("selectedOption : " + selectedOption);
            //    //}

            //    // Proceso secundario para elegir la opcion marcada:
            //    // ordena descendiente y toma el de mayor numero
            //    //  condiciones:
            //    // -no haya opciones encontradas, foundOption = false
            //    //- la lista de opciones tenga elementos selectedOptions.Count > 0
            //    //- si la lista de opciones recorridas es igual al tamaño de la lista de opciones
            //    //    original, que la mitad no tenga un valor de cero
            //    //if (!foundOption &&             // no haya opciones encontradas
            //    //    optList.Count > 0 &&        // la lista de opciones tenga elementos
            //    //    (optList.Count == opts.Count() &&
            //    //    !(optList.Count(c => c.Value == 0) > (opts.Count() / 2))))
            //    //{
            //    //    var orderedOpts = optList.OrderByDescending(o => o.Value).ToArray();
            //    //    if (orderedOpts.Length > 1)
            //    //    {
            //    //        if (Math.Abs(orderedOpts[0].Value - orderedOpts[1].Value) > 20)
            //    //        {
            //    //            var option = orderedOpts[0];
            //    //            selectedOptions.Add(option.Key, option.Value);
            //    //            //respuestas.Add(option.Key, option.Value);

            //    //            //MessageBox.Show("option.Key : " + option.Key + "    option.Value : " + option.Value );

            //    //            var un = new csvLecturaAlternativas2
            //    //            {
            //    //                valor = option.Key.ToString(),
            //    //                blackElements = Convert.ToInt32(option.Value),
            //    //                id = Convert.ToInt32(element.id),

            //    //            };


            //    //            respuestas.Add(un);

            //    //        }
            //    //    }
            //    //}

            //    #endregion

            //    questionImage.Dispose();


            //}


            //var respuestasG = respuestas.GroupBy(rest => rest.nombre);

            //foreach (var respG in respuestasG)
            //{

            //    //foreach (var resG in respG)
            //    //{
            //    //    MessageBox.Show("resG.blackElements : " + resG.blackElements + "     resG.nombre : " + resG.nombre + "     resG.valor : " + resG.valor);
            //    //}


            //    //break;
            //    //var un = new csvLecturaAlternativas2
            //    //{
            //    //    valor = opt.valor.ToString(),
            //    //    nombre = element.nombre.ToString(),
            //    //    id = Convert.ToInt32(element.id),
            //    //    blackElements = Convert.ToInt32(blackElements),

            //    //};


            //    //respuestas.Add(un);


            //    var itemMaxHeight = respG.Max(y => y.blackElements);


            //    var itemsMax = respG.First(number => number.blackElements == itemMaxHeight);



            //    ////var un = new csvLecturaAlternativas2
            //    ////{
            //    ////    valor = itemsMax.valor.ToString(),
            //    ////    nombre = itemsMax.nombre.ToString(),
            //    ////    id = Convert.ToInt32(itemsMax.id),
            //    ////    blackElements = Convert.ToInt32(itemsMax.blackElements),

            //    ////}; Console.WriteLine(newP.ToString());

            //    respuestasMayores.Add(itemsMax);




            //    //MessageBox.Show("newP.ToString() : " + itemsMax.blackElements);

            //}

            //pruebaAlumnoEnviarWeb.respuestasMayores = respuestasMayores;

            //pruebaAlumnoEnviarWeb.regionesFinales = CsvSubRegionModelWeb;



            //this.pruebaAlumnoRespuestaDB.insertar_respuestas(respuestasMayores, prueba_alumno_id, 1);

        }


        /// <summary>
        /// Ejecuta la evaluacion de todas las preguntas de un examen
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="testImage"></param>
        /// <param name="test"></param>
        /// <param name="text"></param>
        /// <param name="limit"></param>
        /// <param name="sum"></param>
        /// <param name="alertMessage"></param>
        private int leerRut(IEnumerable<csvRegionModel> elements, Bitmap testImage, string test, int limit, ref int sum, ref string alertMessage, int templateID, ref PruebaAlumnoEnviarWeb pruebaAlumnoEnviarWeb, string nombre_archivo_origen)
        {

            //MessageBox.Show("EvaluateQuestions : ");

            var respuestas = new List<csvLecturaAlternativas2>();

            var respuestasMayores = new List<csvLecturaAlternativas2>();

            var CsvSubRegionModelWeb = new List<csvSubRegionModelWeb>();

            var prueba_alumno_id = 0;


            //limit = limit + 40;

            //// get all questions (TYPE 2)
            //foreach (var element in elements.Where(e => e.tipo_elemento_id == 1))
            //{

            //    var alert = false;


            //    //MessageBox.Show("element.nombre: " + element.nombre);

            //    //MessageBox.Show("element.id: " + element.id);

            //    //var templElement = element.TemplateElement;
            //    //var elemType = element.ElementType;

            //    // obtiene coordenadas de la region
            //    var x = Convert.ToInt32(element.x);
            //    var y = Convert.ToInt32(element.y);
            //    var width = Convert.ToInt32(element.width);
            //    var height = Convert.ToInt32(element.height);

            //    //MessageBox.Show("element.x : " + element.x + "    element.y : " + element.y + "element.width : " + element.width + "    element.height : " + element.height);

            //    //MessageBox.Show("element.id : " + element.id + "    element.tipo_elemento_id : " + element.tipo_elemento_id);

            //    // obtiene todas las opciones del ractivo/identificador
            //    var opts = SubRegionDB.subRegiones(element.id);



            //    // margen de error
            //    var limitDiff = limitMarginDiff;

            //    #region Opcion Marcada

            //    // Busca la opcion que fue marcada
            //    var optList = new Dictionary<string, long>();
            //    var foundOption = false;
            //    var selectedOptions = new Dictionary<string, long>();
            //    var questionImage = testImage.Clone(new Rectangle(x, y, width, height), testImage.PixelFormat);

            //    //var questionImageSave = testImage.Clone(new Rectangle(x, y, width, height), testImage.PixelFormat);



            //    //MessageBox.Show("test.carpeta : " + test.carpeta + "\\temp" + "    {test.nombre}_{element.nombre}_Q.png : " + $"{test.nombre}_{element.nombre}_Q.png");


            //    //var hashCurrentquestion = GetHash(questionImage, Path.Combine(@"imagenes_escaneadas", $"{test.nombre}_{element.nombre}_Q.png"));


            //    //questionImage.Save(@"imagenes_escaneadas\\" + $"{test.nombre}_{element.nombre}_Q.jpg", ImageFormat.Jpeg);

            //    //MessageBox.Show("questionImage : " + questionImage);

            //    //MessageBox.Show("System.IO.File.Exists + .jpg) : " + System.IO.File.Exists(@"C:\Temp\" + $"{test.nombre}_{element.nombre}_Q" + ".jpg"));



            //    //var nombre_archivo = @"C:\Temp\" + $"{test.nombre}_{element.nombre}_Q_{nombre_archivo_origen}";


            //    //if (System.IO.File.Exists(nombre_archivo))
            //    //{
            //    //    //MessageBox.Show("SI : ");

            //    //    System.IO.File.Delete(nombre_archivo);
            //    //}


            //    //questionImageSave.Save(nombre_archivo, ImageFormat.Jpeg);


            //    //questionImageSave.Dispose();

            //    //var csvsubRegionModelWeb2 = new csvSubRegionModelWeb
            //    //{
            //    //    id = element.id,
            //    //    nombre = element.nombre,
            //    //    descripcion = element.descripcion,
            //    //    x = element.x,
            //    //    y = element.y,
            //    //    width = element.width,
            //    //    height = element.height,
            //    //    imagen_Local = "imagen_Local",
            //    //    imagen_nombre = "imagen_nombre",

            //    //};



            //    //var CsvsubRegionModelWeb = new csvSubRegionModelWeb
            //    //{

            //    //    id = 1,

            //    //};


            //    //pruebaAlumnoEnviarWeb.regionesFinales.Add(csvsubRegionModelWeb2);




            //    //CsvSubRegionModelWeb.Add(csvsubRegionModelWeb2);

            //    //pruebaAlumnoEnviarWeb.alternativas.Add(alternativas);



            //    foreach (var opt in opts)
            //    {

            //        //MessageBox.Show("element : " + element.nombre);

            //        //MessageBox.Show("opt.x : " + opt.x + "    opt.y : " + opt.y + "opt.width : " + opt.width + "    opt.height : " + opt.height);

            //        var x1 = Convert.ToInt32(opt.x);
            //        var y1 = Convert.ToInt32(opt.y);
            //        var w1 = Convert.ToInt32(opt.width);
            //        var h1 = Convert.ToInt32(opt.height);

            //        // reducen aun mas el tamanho de la opcion
            //        if (w1 - 4 > 0 && h1 - 4 > 0)
            //        {
            //            x1 += 2;
            //            y1 += 2;
            //            w1 -= 2;
            //            h1 -= 2;
            //            limitDiff -= 2;
            //        }

            //        if (w1 > width) w1 = width;
            //        if (h1 > height) h1 = height;

            //        if (x1 + w1 > width && x1 - (x1 + w1 - width) >= 0)
            //        {
            //            var difx = x1 + w1 - width;
            //            x1 -= difx;
            //        }

            //        if (y1 + h1 > height && y1 - (y1 + h1 - height) >= 0)
            //        {
            //            var dify = y1 + h1 - height;
            //            y1 -= dify;
            //        }

            //        var answerImage = questionImage.Clone(new Rectangle(x1, y1, w1, h1), questionImage.PixelFormat);
            //        var hashCurrent = GetHash(answerImage, Path.Combine(@"imagenes_escaneadas", $"{test}_{element.nombre}_QA.png"));

            //        //determine the number of equal pixel (x of 256)
            //        int blackElements = hashCurrent.Count(r => r == true);
            //        optList.Add(opt.nombre, blackElements);

            //        //MessageBox.Show("blackElements : " + blackElements + "    limit : " + limit + "    limitDiff : " + limitDiff + "    (blackElements > limit): " + (blackElements > limit) + "    ((limit - blackElements) < limitDiff || ((limit - blackElements - limitDiff) < 3)): " + ((limit - blackElements) < limitDiff || ((limit - blackElements - limitDiff) < 3)));

            //        // cuando es pregunta, debe validar si se toma como correcta
            //        var result = (blackElements > limit) || ((limit - blackElements) < limitDiff || ((limit - blackElements - limitDiff) < 3));



            //        //var answerImageSave = questionImage.Clone(new Rectangle(x1, y1, w1, h1), questionImage.PixelFormat);
            //        //var nombre_archivo_sr = @"imagenes_escaneadas\\" + $"{test.nombre}_{element.nombre}_Q_{opt.id}_SQ_{nombre_archivo_origen}";


            //        //if (System.IO.File.Exists(nombre_archivo_sr))
            //        //{
            //        //    //MessageBox.Show("SI : ");

            //        //    System.IO.File.Delete(nombre_archivo_sr);
            //        //}




            //        //answerImageSave.Save(nombre_archivo_sr, ImageFormat.Jpeg);


            //        //answerImageSave.Dispose();

            //        if (result)
            //        {

            //            //MessageBox.Show("blackElements : " + blackElements + "    limit : " + limit + "    limitDiff : " + limitDiff + "    (blackElements > limit): " + (blackElements > limit) + "    ((limit - blackElements) < limitDiff || ((limit - blackElements - limitDiff) < 3)): " + ((limit - blackElements) < limitDiff || ((limit - blackElements - limitDiff) < 3)));

            //            //MessageBox.Show("element.nombre: " + element.nombre + "blackElements : " + blackElements + "   result : " + result + "   limit : " + limit + "   opt.nombre : " + opt.nombre + "   opt.valor : " + opt.valor);
            //            // Si se toma como correcta, se agrega a una lista de opciones 'marcadas'
            //            foundOption = true;
            //            selectedOptions.Add(opt.valor, blackElements);

            //            var un = new csvLecturaAlternativas2
            //            {
            //                valor = opt.valor.ToString(),
            //                nombre = element.nombre.ToString(),
            //                id = Convert.ToInt32(element.id),
            //                blackElements = Convert.ToInt32(blackElements),

            //            };


            //            respuestas.Add(un);
            //        }
            //    }



            //    //foreach (var selectedOption in selectedOptions)
            //    //{
            //    //    MessageBox.Show("selectedOption : " + selectedOption);
            //    //}

            //    // Proceso secundario para elegir la opcion marcada:
            //    // ordena descendiente y toma el de mayor numero
            //    //  condiciones:
            //    // -no haya opciones encontradas, foundOption = false
            //    //- la lista de opciones tenga elementos selectedOptions.Count > 0
            //    //- si la lista de opciones recorridas es igual al tamaño de la lista de opciones
            //    //    original, que la mitad no tenga un valor de cero
            //    //if (!foundOption &&             // no haya opciones encontradas
            //    //    optList.Count > 0 &&        // la lista de opciones tenga elementos
            //    //    (optList.Count == opts.Count() &&
            //    //    !(optList.Count(c => c.Value == 0) > (opts.Count() / 2))))
            //    //{
            //    //    var orderedOpts = optList.OrderByDescending(o => o.Value).ToArray();
            //    //    if (orderedOpts.Length > 1)
            //    //    {
            //    //        if (Math.Abs(orderedOpts[0].Value - orderedOpts[1].Value) > 20)
            //    //        {
            //    //            var option = orderedOpts[0];
            //    //            selectedOptions.Add(option.Key, option.Value);
            //    //            //respuestas.Add(option.Key, option.Value);

            //    //            //MessageBox.Show("option.Key : " + option.Key + "    option.Value : " + option.Value );

            //    //            var un = new csvLecturaAlternativas2
            //    //            {
            //    //                valor = option.Key.ToString(),
            //    //                blackElements = Convert.ToInt32(option.Value),
            //    //                id = Convert.ToInt32(element.id),

            //    //            };


            //    //            respuestasMayores.Add(un);

            //    //        }
            //    //    }
            //    //}

            //    #endregion

            //    questionImage.Dispose();


            //}

            //pruebaAlumnoEnviarWeb.regionesFinales = CsvSubRegionModelWeb;



            //var respuestasG = respuestas.GroupBy(rest => rest.nombre);

            //foreach (var respG in respuestasG)
            //{

            //    //foreach (var resG in respG)
            //    //{
            //    //    MessageBox.Show("resG.blackElements : " + resG.blackElements + "     resG.nombre : " + resG.nombre + "     resG.valor : " + resG.valor);
            //    //}


            //    //break;
            //    //var un = new csvLecturaAlternativas2
            //    //{
            //    //    valor = opt.valor.ToString(),
            //    //    nombre = element.nombre.ToString(),
            //    //    id = Convert.ToInt32(element.id),
            //    //    blackElements = Convert.ToInt32(blackElements),

            //    //};


            //    //respuestas.Add(un);


            //    var itemMaxHeight = respG.Max(y => y.blackElements);


            //    var itemsMax = respG.First(number => number.blackElements == itemMaxHeight);



            //    var un = new csvLecturaAlternativas2
            //    {
            //        valor = itemsMax.valor.ToString(),
            //        nombre = itemsMax.nombre.ToString(),
            //        id = Convert.ToInt32(itemsMax.id),
            //        blackElements = Convert.ToInt32(itemsMax.blackElements),

            //    }; //Console.WriteLine(newP.ToString());

            //    respuestasMayores.Add(itemsMax);




            //    //MessageBox.Show("newP.ToString() : " + itemsMax.blackElements);

            //}





            //var formulas_datos = this.GetFormulaValue(templateID, respuestasMayores);

            ////Conformo la cantidad de formulas

            //pruebaAlumnoEnviarWeb.formulas_datos = formulas_datos;

            ////MessageBox.Show("formulas_datos : " + formulas_datos);

            //if (formulas_datos.Count > 0)
            //{

            //    var primera_formula = formulas_datos.First();

            //    //MessageBox.Show("primera_formula.nombre : " + primera_formula.nombre);



            //    //entornoUsuarioFinal.Dispatcher.Invoke((Action)(() =>
            //    //{//this refer to form in WPF application 

            //    //    entornoUsuarioFinal.log.Text = "Iniciando Escaneo";

            //    //}));

            //    Application.Current.Resources["MessageLog"] = "Datos Procesados : " + primera_formula.nombre;





            //    pruebaAlumnoEnviarWeb.primera_formula = primera_formula.nombre;

            //    // consulto si este rut ya tiene creada una prueba SQLite


            //    //prueba_alumno_id = this.pruebaAlumnoDB.insert_prueba(primera_formula.nombre, 1);

            //    //this.pruebaAlumnoFormulaDB.insertar_formulas(formulas_datos, prueba_alumno_id, 1);



            //}

            return prueba_alumno_id;

        }



        private int EvaluateCircle(IEnumerable<csvRegionModel> elements, Bitmap testImage, csvEscanearModel test, int templateID)
        {

            var respuestas = new List<csvLecturaAlternativas2>();

            // get all questions (TYPE 2)
            //foreach (var element in elements.Where(e => e.tipo_elemento_id == 3))
            //{

            //    // obtiene coordenadas de la region
            //    var x = Convert.ToInt32(element.x);
            //    var y = Convert.ToInt32(element.y);
            //    var width = Convert.ToInt32(element.width);
            //    var height = Convert.ToInt32(element.height);

            //    // obtiene todas las opciones del ractivo/identificador
            //    var opts = SubRegionDB.subRegiones(element.id);



            //    // margen de error
            //    var limitDiff = limitMarginDiff;

            //    #region Opcion Marcada

            //    // Busca la opcion que fue marcada
            //    var optList = new Dictionary<string, long>();
            //    var foundOption = false;
            //    var selectedOptions = new Dictionary<string, long>();
            //    var questionImage = testImage.Clone(new Rectangle(x, y, width, height), testImage.PixelFormat);
            //    foreach (var opt in opts)
            //    {

            //        //MessageBox.Show("element : " + element.nombre);

            //        //MessageBox.Show("opt.x : " + opt.x + "    opt.y : " + opt.y + "opt.width : " + opt.width + "    opt.height : " + opt.height);

            //        var x1 = Convert.ToInt32(opt.x);
            //        var y1 = Convert.ToInt32(opt.y);
            //        var w1 = Convert.ToInt32(opt.width);
            //        var h1 = Convert.ToInt32(opt.height);

            //        // reducen aun mas el tamanho de la opcion
            //        if (w1 - 4 > 0 && h1 - 4 > 0)
            //        {
            //            x1 += 2;
            //            y1 += 2;
            //            w1 -= 2;
            //            h1 -= 2;
            //            limitDiff -= 2;
            //        }

            //        if (w1 > width) w1 = width;
            //        if (h1 > height) h1 = height;

            //        if (x1 + w1 > width && x1 - (x1 + w1 - width) >= 0)
            //        {
            //            var difx = x1 + w1 - width;
            //            x1 -= difx;
            //        }

            //        if (y1 + h1 > height && y1 - (y1 + h1 - height) >= 0)
            //        {
            //            var dify = y1 + h1 - height;
            //            y1 -= dify;
            //        }

            //        var answerImage = questionImage.Clone(new Rectangle(x1, y1, w1, h1), questionImage.PixelFormat);
            //        var hashCurrent = GetHash(answerImage, Path.Combine(test.carpeta, $"{test.nombre}_{element.nombre}_Q.png"));

            //        //determine the number of equal pixel (x of 256)
            //        int blackElements = hashCurrent.Count(r => r == true);
            //        optList.Add(opt.nombre, blackElements);

            //        //MessageBox.Show("blackElements : " + blackElements + "    limit : " + limit + "    limitDiff : " + limitDiff + "    (blackElements > limit): " + (blackElements > limit) + "    ((limit - blackElements) < limitDiff || ((limit - blackElements - limitDiff) < 3)): " + ((limit - blackElements) < limitDiff || ((limit - blackElements - limitDiff) < 3)));

            //        // cuando es pregunta, debe validar si se toma como correcta
            //        //var result = (blackElements > limit) || ((limit - blackElements) < limitDiff || ((limit - blackElements - limitDiff) < 3));




            //        //if (result)
            //        //{

            //        //MessageBox.Show("element.nombre: " + element.nombre + "blackElements : " + blackElements + "   result : " + result + "   limit : " + limit + "   opt.nombre : " + opt.nombre + "   opt.valor : " + opt.valor);
            //        // Si se toma como correcta, se agrega a una lista de opciones 'marcadas'
            //        //foundOption = true;
            //        //selectedOptions.Add(opt.valor, blackElements);

            //        var un = new csvLecturaAlternativas2
            //        {
            //            valor = opt.valor.ToString(),
            //            nombre = element.nombre.ToString(),
            //            id = Convert.ToInt32(element.id),
            //            blackElements = Convert.ToInt32(blackElements),

            //        };


            //        respuestas.Add(un);
            //        //}
            //    }





            //    #endregion




            //}





            return Convert.ToInt32(respuestas.Average(item => item.blackElements));

        }




        /// <summary>
        /// Lee un archivo e inserta todas sus lineas en un diccionario de strings
        /// </summary>
        /// <param name="textFile"></param>
        /// <returns></returns>
        private Dictionary<string, string> GetLinesDictionary(string textFile)
        {
            var dict = new Dictionary<string, string>();
            var allLines = File.ReadAllLines(textFile);
            foreach (var line in allLines)
            {
                var lineElements = line.Split('=');
                if (lineElements.Length > 1)
                {
                    dict.Add(lineElements[0].TrimStart(' ').TrimEnd(' '), lineElements[1].TrimStart(' ').TrimEnd(' '));
                }
            }
            return dict;
        }


        private void EvaluateIdentifier(IEnumerable<csvRegionModel> elements, Bitmap testImage, csvEscanearModel test, int limit, ref int sum, ref string alertMessage, int templateID, int prueba_alumno_id)
        {



            var respuestas = new List<csvLecturaAlternativas2>();







            // get all questions (TYPE 2)
            foreach (var element in elements.Where(e => e.tipo_elemento_id == 1))
            {




                var alert = false;


                //MessageBox.Show("element.nombre: " + element.nombre);

                //var templElement = element.TemplateElement;
                //var elemType = element.ElementType;

                // obtiene coordenadas de la region
                var x = Convert.ToInt32(element.x);
                var y = Convert.ToInt32(element.y);
                var width = Convert.ToInt32(element.width);
                var height = Convert.ToInt32(element.height);

                //MessageBox.Show("element.x : " + element.x + "    element.y : " + element.y + "element.width : " + element.width + "    element.height : " + element.height);

                //MessageBox.Show("element.id : " + element.id + "    element.tipo_elemento_id : " + element.tipo_elemento_id);

                // obtiene todas las opciones del ractivo/identificador
                var opts = SubRegionDB.subRegiones(element.id);



                // margen de error
                var limitDiff = limitMarginDiff;

                #region Opcion Marcada

                // Busca la opcion que fue marcada
                var optList = new Dictionary<string, long>();
                var foundOption = false;
                var selectedOptions = new Dictionary<string, long>();
                var questionImage = testImage.Clone(new Rectangle(x, y, width, height), testImage.PixelFormat);
                foreach (var opt in opts)
                {

                    //MessageBox.Show("element : " + element.nombre);

                    //MessageBox.Show("opt.x : " + opt.x + "    opt.y : " + opt.y + "opt.width : " + opt.width + "    opt.height : " + opt.height);

                    var x1 = Convert.ToInt32(opt.x);
                    var y1 = Convert.ToInt32(opt.y);
                    var w1 = Convert.ToInt32(opt.width);
                    var h1 = Convert.ToInt32(opt.height);

                    // reducen aun mas el tamanho de la opcion
                    if (w1 - 4 > 0 && h1 - 4 > 0)
                    {
                        x1 += 2;
                        y1 += 2;
                        w1 -= 2;
                        h1 -= 2;
                        limitDiff -= 2;
                    }

                    if (w1 > width) w1 = width;
                    if (h1 > height) h1 = height;

                    if (x1 + w1 > width && x1 - (x1 + w1 - width) >= 0)
                    {
                        var difx = x1 + w1 - width;
                        x1 -= difx;
                    }

                    if (y1 + h1 > height && y1 - (y1 + h1 - height) >= 0)
                    {
                        var dify = y1 + h1 - height;
                        y1 -= dify;
                    }

                    var answerImage = questionImage.Clone(new Rectangle(x1, y1, w1, h1), questionImage.PixelFormat);
                    var hashCurrent = GetHash(answerImage, Path.Combine(test.carpeta, $"{test.nombre}_{element.nombre}_Q.png"));

                    //determine the number of equal pixel (x of 256)
                    int blackElements = hashCurrent.Count(r => r == true);
                    optList.Add(opt.nombre, blackElements);

                    //MessageBox.Show("blackElements : " + blackElements + "    limit : " + limit + "    limitDiff : " + limitDiff + "    (blackElements > limit): " + (blackElements > limit) + "    ((limit - blackElements) < limitDiff || ((limit - blackElements - limitDiff) < 3)): " + ((limit - blackElements) < limitDiff || ((limit - blackElements - limitDiff) < 3)));

                    // cuando es pregunta, debe validar si se toma como correcta
                    var result = (blackElements > limit) || ((limit - blackElements) < limitDiff || ((limit - blackElements - limitDiff) < 3));




                    if (result)
                    {

                        //MessageBox.Show("element.nombre: " + element.nombre + "blackElements : " + blackElements + "   result : " + result + "   limit : " + limit + "   opt.nombre : " + opt.nombre + "   opt.valor : " + opt.valor);
                        // Si se toma como correcta, se agrega a una lista de opciones 'marcadas'
                        foundOption = true;
                        selectedOptions.Add(opt.valor, blackElements);

                        var un = new csvLecturaAlternativas2
                        {
                            valor = opt.valor.ToString(),
                            nombre = element.nombre.ToString(),
                            id = Convert.ToInt32(element.id),
                            blackElements = Convert.ToInt32(blackElements),

                        };


                        respuestas.Add(un);
                    }
                }



                //foreach (var selectedOption in selectedOptions)
                //{
                //    MessageBox.Show("selectedOption : " + selectedOption);
                //}

                // Proceso secundario para elegir la opcion marcada:
                // ordena descendiente y toma el de mayor numero
                //  condiciones:
                // -no haya opciones encontradas, foundOption = false
                //- la lista de opciones tenga elementos selectedOptions.Count > 0
                //- si la lista de opciones recorridas es igual al tamaño de la lista de opciones
                //    original, que la mitad no tenga un valor de cero
                if (!foundOption &&             // no haya opciones encontradas
                    optList.Count > 0 &&        // la lista de opciones tenga elementos
                    (optList.Count == opts.Count() &&
                    !(optList.Count(c => c.Value == 0) > (opts.Count() / 2))))
                {
                    var orderedOpts = optList.OrderByDescending(o => o.Value).ToArray();
                    if (orderedOpts.Length > 1)
                    {
                        if (Math.Abs(orderedOpts[0].Value - orderedOpts[1].Value) > 20)
                        {
                            var option = orderedOpts[0];
                            selectedOptions.Add(option.Key, option.Value);
                            //respuestas.Add(option.Key, option.Value);

                            //MessageBox.Show("option.Key : " + option.Key + "    option.Value : " + option.Value );

                            var un = new csvLecturaAlternativas2
                            {
                                valor = option.Key.ToString(),
                                blackElements = Convert.ToInt32(option.Value),
                                id = Convert.ToInt32(element.id),

                            };


                            respuestas.Add(un);

                        }
                    }
                }

                #endregion




            }





            var formulas_datos = this.GetFormulaValue(templateID, respuestas);

            //Conformo la cantidad de formulas

            if (formulas_datos.Count > 0)
            {

                var primera_formula = formulas_datos.First();

                //MessageBox.Show("primera_formula.nombre : " + primera_formula.nombre);

                // consulto si este rut ya tiene creada una prueba


                //var prueba_alumno_id = this.pruebaAlumnoDB.insert_prueba(primera_formula.nombre, 1);


                //this.pruebaAlumnoFormulaDB.insertar_formulas(formulas_datos, prueba_alumno_id, 1);

                ////MessageBox.Show("pruebaAlumno : ");

                //EvaluateIdentifier(elements, testImage, test, limit, ref sum, ref alertMessage, templateID, prueba_alumno_id);





            }







        }


        public List<csvformulasModel> GetFormulaValue(int templateID, List<csvLecturaAlternativas2> respuestas)
        {
            var result = string.Empty;


            //foreach (var respuesta in respuestas)
            //{

            //    MessageBox.Show("respuesta.id : " + respuesta.id + "respuesta.nombre : " + respuesta.nombre + "   respuesta.valor : " + respuesta.valor + "   respuesta.blackElements : " + respuesta.blackElements);

            //}

            var formulas_datos = new List<csvformulasModel>();

            //Busco la formula que le corresponde

            var opts = formulaDB.dataObjetos(templateID);

            foreach (var formula in opts)
            {
                result += formula.nombre + "=";
                var content = formula.formula;

                // todo lo que se va a concatenar
                // <key> + <key> + 'text' + "text"

                var elements = content.Split('+');
                var finalConcat = string.Empty;

                //MessageBox.Show("elements: " + elements);

                foreach (var elem in elements)
                {




                    var element = elem.TrimEnd(' ').TrimStart(' ');

                    //MessageBox.Show("element: " + element + "   (element.StartsWith() && element.EndsWith()) : " + (element.StartsWith("\'") && element.EndsWith("\'")));

                    // string
                    if (element.StartsWith("\'") && element.EndsWith("\'"))
                    {



                        finalConcat += element.TrimStart('\'').TrimEnd('\'');
                    }
                    else
                    {

                        var result_find = respuestas.Find(x => x.nombre == element);

                        if (result_find != null)
                        {


                            //MessageBox.Show("elem : " + elem + "element : " + element + "     result_find.id : " + result_find.id + "result_find.nombre : " + result_find.nombre + "   result_find.valor : " + result_find.valor + "   result_find.blackElements : " + result_find.blackElements);

                            finalConcat += result_find.valor.TrimStart(' ').TrimEnd(' ');


                        }



                        //// key
                        //var value = string.Empty;
                        //if (testResults.TryGetValue(element, out value))
                        //{
                        //    if (value.Contains(','))
                        //    {
                        //        var valueElements = value.Split(',');
                        //        finalConcat += valueElements[1].TrimStart(' ').TrimEnd(' ');
                        //    }
                        //    else
                        //        finalConcat += value.TrimStart(' ').TrimEnd(' ');
                        //}
                    }
                }
                result += finalConcat;



                var un = new csvformulasModel
                {
                    nombre = finalConcat,
                    formula_id = Convert.ToInt32(formula.id)
                };



                formulas_datos.Add(un);

                //MessageBox.Show("finalConcat : " + finalConcat);




            }




            return formulas_datos;


        }


        //private Bitmap GetSubImage(int _x, int _y, int _width, int _height, Bitmap source)
        //{
        //    // obtiene coordenadas de la region
        //    var width = Convert.ToInt32(source.Width);
        //    var height = Convert.ToInt32(source.Height);

        //    var x2 = Convert.ToInt32(_x);
        //    var y2 = Convert.ToInt32(_y);
        //    var w2 = Convert.ToInt32(_width);
        //    var h2 = Convert.ToInt32(_height);

        //    var limitDiff = 8;

        //    // get answer block
        //    // apply padding
        //    if (w2 - 4 > 0 && h2 - 4 > 0)
        //    {
        //        x2 += 2;
        //        y2 += 2;
        //        w2 -= 2;
        //        h2 -= 2;

        //        limitDiff -= 2;
        //    }

        //    if (w2 > width) w2 = width;
        //    if (h2 > height) h2 = height;

        //    if (x2 + w2 > width && x2 - (x2 + w2 - width) >= 0)
        //    {
        //        var difx = x2 + w2 - width;
        //        x2 -= difx;
        //    }
        //    if (y2 + h2 > height && y2 - (y2 + h2 - height) >= 0)
        //    {
        //        var dify = y2 + h2 - height;
        //        y2 -= dify;
        //    }

        //    var answerImage2 = source.Clone(new Rectangle(x2, y2, w2, h2), source.PixelFormat);
        //    var hashCurrent2 = GetHash(answerImage2, string.Empty);
        //    int blackElements2 = hashCurrent2.Count(r => r == true);
        //    var result2 = (blackElements2 > limit) || ((limit - blackElements2) < limitDiff || ((limit - blackElements2 - limitDiff) < 3));
        //}

        //private void EvaluateIdentifier(IEnumerable<csvRegionModel> elements, Bitmap testImage, csvEscanearModel test, int limit, ref int sum, ref string alertMessage, int templateID)
        //{


        //    // get identifyiers (TYPE 1)
        //    foreach (var element in elements.Where(e => e.tipo_elemento_id == 1))
        //    {

        //        MessageBox.Show("tipo_elemento_id : " + element.tipo_elemento_id);
        //        //var alert = false;

        //        //var templElement = element.TemplateElement;
        //        //var elemType = element.ElementType;

        //        //// obtiene coordenadas de la region
        //        //var x = Convert.ToInt32(templElement.X);
        //        //var y = Convert.ToInt32(templElement.Y);
        //        //var width = Convert.ToInt32(elemType.Width);
        //        //var height = Convert.ToInt32(elemType.Height);

        //        //// obtiene todas las opciones del ractivo/identificador
        //        //var opts = DataHelper.GetElementTypeOpts(elemType.ElementTypeID);

        //        //// reajusta region para mayor precision
        //        //if (x + width > testImage.Width)
        //        //{
        //        //    var difx = x + width - testImage.Width;
        //        //    x -= difx;
        //        //}
        //        //if (y + height > testImage.Height)
        //        //{
        //        //    var dify = y + height - testImage.Height;
        //        //    y -= dify;
        //        //}

        //        //#region Opcion Marcada

        //        //var selectedOptions = new Dictionary<string, long>();

        //        //// Busca la opcion que fue marcada
        //        //var optList = new Dictionary<string, long>();
        //        //var foundOption = false;
        //        //var questionImage = testImage.Clone(new Rectangle(x, y, width, height), testImage.PixelFormat);
        //        //foreach (var opt in opts)
        //        //{
        //        //    var x1 = Convert.ToInt32(opt.X);
        //        //    var y1 = Convert.ToInt32(opt.Y);
        //        //    var w1 = Convert.ToInt32(opt.Width);
        //        //    var h1 = Convert.ToInt32(opt.Height);
        //        //    var limitDiff = this.limitMarginDiff;

        //        //    // reajusta el recuadro para que sea mas chico
        //        //    if (w1 - 4 > 0 && h1 - 4 > 0)
        //        //    {
        //        //        x1 += 2;
        //        //        y1 += 2;
        //        //        w1 -= 2;
        //        //        h1 -= 2;
        //        //        limitDiff -= 2;
        //        //    }

        //        //    if (w1 > width) w1 = width;
        //        //    if (h1 > height) h1 = height;

        //        //    if (x1 + w1 > width && x1 - (x1 + w1 - width) >= 0)
        //        //    {
        //        //        var difx = x1 + w1 - width;
        //        //        x1 -= difx;
        //        //    }

        //        //    if (y1 + h1 > height && y1 - (y1 + h1 - height) >= 0)
        //        //    {
        //        //        var dify = y1 + h1 - height;
        //        //        y1 -= dify;
        //        //    }

        //        //    var answerImage = questionImage.Clone(new Rectangle(x1, y1, w1, h1), questionImage.PixelFormat);
        //        //    var hashCurrent = GetHash(answerImage, Path.Combine(test.InputFolder, $"{test.Name}_{templElement.Name}_Q.png"));

        //        //    int blackElements = hashCurrent.Count(r => r == true);
        //        //    optList.Add(opt.Name, blackElements);

        //        //    var result = (blackElements > limit) || ((limit - blackElements) < limitDiff || ((limit - blackElements - limitDiff) < 3));
        //        //    if (result)
        //        //    {
        //        //        foundOption = true;
        //        //        selectedOptions.Add(opt.Name, blackElements);
        //        //    }
        //        //}


        //        //// Proceso secundario para elegir la respuesta seleccionada:
        //        //// ordena descendiente y toma el de mayor numero
        //        //// condiciones:
        //        //// - no haya opciones encontradas
        //        //// - la lista de opciones tenga elementos
        //        //// - si la lista de opciones recorridas es igual al tamaño de la lista de opciones original, que la mitad no tenga un valor de cero
        //        //if (!foundOption && optList.Count > 0 && (optList.Count == opts.Count() && !(optList.Count(c => c.Value == 0) > (opts.Count() / 2))))
        //        //{
        //        //    var orderedOpts = optList.OrderByDescending(o => o.Value).ToArray();
        //        //    if (orderedOpts.Length > 1)
        //        //    {
        //        //        if (Math.Abs(orderedOpts[0].Value - orderedOpts[1].Value) > 20)
        //        //        {
        //        //            var option = orderedOpts[0];
        //        //            selectedOptions.Add(option.Key, option.Value);
        //        //        }
        //        //    }
        //        //}

        //        //var selectedOption = string.Empty;
        //        //foreach (var option in selectedOptions)
        //        //{
        //        //    if (!string.IsNullOrEmpty(selectedOption)) selectedOption += ",";
        //        //    selectedOption += option.Key;
        //        //}

        //        //// Alerta
        //        //if (selectedOptions.Count > 1)
        //        //{
        //        //    // Validar si la diferencia es pequenha entre 
        //        //    // las opciones seleccionadas
        //        //    var selectedOptionsArr = selectedOptions.OrderByDescending(o => o.Value).ToArray();
        //        //    var opt1 = selectedOptionsArr[0];
        //        //    var opt2 = selectedOptionsArr[1];
        //        //    var blackOpt1 = optList.FirstOrDefault(r => r.Key == opt1.Key);
        //        //    var blackOpt2 = optList.FirstOrDefault(r => r.Key == opt2.Key);
        //        //    if (Math.Abs(blackOpt1.Value - blackOpt2.Value) < 20)
        //        //    {
        //        //        var message = "Mas de una opción fue seleccionada en " + templElement.Name + " - " + selectedOption;
        //        //        text.WriteLine("ALERTA-" + templElement.Name + "=" + message);
        //        //        alert = true;
        //        //        alertMessage += message + Environment.NewLine;
        //        //    }
        //        //    else
        //        //    {
        //        //        // si no se sigue tomando la primera como la elegida
        //        //        selectedOption = blackOpt1.Key;
        //        //    }
        //        //}

        //        //// Lectura invalida
        //        //// - no hubo alertas
        //        //// - Si mas de la mitad de las opciones leidas es igual a cero
        //        //// - no hubo opcion seleccionada 
        //        //if (!alert &&
        //        //        (!foundOption &&
        //        //        string.IsNullOrEmpty(selectedOption) &&
        //        //        ((optList.Max(o => o.Value) >= (limit * .80)) ||
        //        //        (optList.Min(o => o.Value) < 20))))
        //        //{
        //        //    var message = "Se encontró una lectura incorrecta en " + templElement.Name;
        //        //    text.WriteLine("ALERTA-" + templElement.Name + "=" + message);
        //        //    alert = true;
        //        //    alertMessage += message + Environment.NewLine;
        //        //}

        //        //if (string.IsNullOrEmpty(selectedOption)) selectedOption = "-";
        //        //text.WriteLine(templElement.Name + "=" + selectedOption);

        //        //#endregion
        //    }
        //}

        public static List<bool> encuentraLosPixelesMarcados(Bitmap bmpSource)
        {
            List<bool> lResult = new List<bool>();
            //create new image with 16x16 pixel
            Bitmap bmpMin = new Bitmap(bmpSource, new Size(16, 16));
            //bmpMin = Accord.Imaging.Filters.Grayscale.CommonAlgorithms.BT709.Apply(bmpMin);
            //var b = new Bitmap(16, 16);
            for (int j = 0; j < bmpMin.Height; j++)
            {
                for (int i = 0; i < bmpMin.Width; i++)
                {
                    //reduce colors to true / false                

                    //MessageBox.Show("bmpMin.GetPixel(i, j).GetBrightness() : " + bmpMin.GetPixel(i, j).GetBrightness() + "   .9 " + .68f);
                    lResult.Add(bmpMin.GetPixel(i, j).GetBrightness() < .6);
                    //b.SetPixel(i, j, bmpMin.GetPixel(i, j).GetBrightness() < .6 ? Color.Black : Color.White);
                }
            }

            //b.Save(name);
            return lResult;
        }

        public static List<bool> GetHash(Bitmap bmpSource, string name = "")
        {
            List<bool> lResult = new List<bool>();
            //create new image with 16x16 pixel
            Bitmap bmpMin = new Bitmap(bmpSource, new Size(16, 16));
            //bmpMin = Accord.Imaging.Filters.Grayscale.CommonAlgorithms.BT709.Apply(bmpMin);
            var b = new Bitmap(16, 16);
            for (int j = 0; j < bmpMin.Height; j++)
            {
                for (int i = 0; i < bmpMin.Width; i++)
                {
                    //reduce colors to true / false                

                    //MessageBox.Show("bmpMin.GetPixel(i, j).GetBrightness() : " + bmpMin.GetPixel(i, j).GetBrightness() + "   .9 " + .68f);
                    lResult.Add(bmpMin.GetPixel(i, j).GetBrightness() < .6);
                    b.SetPixel(i, j, bmpMin.GetPixel(i, j).GetBrightness() < .6 ? Color.Black : Color.White);
                }
            }

            //b.Save(name);
            return lResult;
        }

    }
}
