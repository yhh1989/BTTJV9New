using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

public class RestClient
{
    public class RestClientHeader
    {
        public string name { get; set; }
        public string val { get; set; }
    }
    private string BaseUri;
    public RestClient(string baseUri)
    {
        this.BaseUri = baseUri;
    }
    public Dictionary<string, string> headers { get; set; } = new Dictionary<string, string>();
    //public List<RestClientHeader> headers { get; set; }=new List<RestClientHeader>();
    public void header(string name, string val)
    {

        if (headers.ContainsKey(name))
        {
            this.headers[name] = val;
        }
        else
        {
            this.headers.Add(name, val);
        }
    }
    #region Get请求
    public string Get(out bool success, string uri)
    {
        success = false;
        try
        {
            string serviceUrl = string.Format("{0}/{1}", this.BaseUri, uri);
            //创建Web访问对  象
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);
            foreach (var item in this.headers)
            {
                myRequest.Headers.Add(item.Key, item.Value);
            }
            //通过Web访问对象获取响应内容
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            //通过响应内容流创建StreamReader对象，因为StreamReader更高级更快
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            //string returnXml = HttpUtility.UrlDecode(reader.ReadToEnd());//如果有编码问题就用这个方法
            string returnXml = reader.ReadToEnd();//利用StreamReader就可以从响应内容从头读到尾
            reader.Close();
            myResponse.Close();
            success = true;
            return returnXml;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
        //先根据用户请求的uri构造请求地址
    }
    #endregion
    #region Post请求
    public string Post(out bool success, string data, string uri)
    {
        Console.WriteLine("入参"+data);
        success = false;
        try
        {
            //先根据用户请求的uri构造请求地址
            string serviceUrl = string.Format("{0}/{1}", this.BaseUri, uri);
            Console.WriteLine(serviceUrl);
            //创建Web访问对象
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);
            foreach (var item in this.headers)
            {
                myRequest.Headers.Add(item.Key, item.Value);

            }
            //把用户传过来的数据转成“UTF-8”的字节流
            byte[] buf = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(data);
            myRequest.Method = "POST";
            myRequest.ContentLength = buf.Length;
            myRequest.ContentType = "application/json";
            //myRequest.ContentType = "application/x-www-form-urlencoded";
            myRequest.MaximumAutomaticRedirections = 1;
            myRequest.AllowAutoRedirect = true;
            //发送请求
            Stream stream = myRequest.GetRequestStream();
            stream.Write(buf, 0, buf.Length);
            stream.Close();
            //获取接口返回值
            //通过Web访问对象获取响应内容
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            //通过响应内容流创建StreamReader对象，因为StreamReader更高级更快
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            //string returnXml = HttpUtility.UrlDecode(reader.ReadToEnd());//如果有编码问题就用这个方法
            string returnXml = reader.ReadToEnd();//利用StreamReader就可以从响应内容从头读到尾
            reader.Close();
            myResponse.Close();
            success = true;

            return returnXml;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
    #endregion
    #region Put请求
    public string Put(out bool success, string data, string uri)
    {
        success = false;
        try
        {
            //先根据用户请求的uri构造请求地址
            string serviceUrl = string.Format("{0}/{1}", this.BaseUri, uri);
            //创建Web访问对象
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);
            foreach (var item in this.headers)
            {
                myRequest.Headers.Add(item.Key, item.Value);

            }
            //把用户传过来的数据转成“UTF-8”的字节流
            byte[] buf = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(data);
            myRequest.Method = "PUT";
            myRequest.ContentLength = buf.Length;
            myRequest.ContentType = "application/json";
            myRequest.MaximumAutomaticRedirections = 1;
            myRequest.AllowAutoRedirect = true;
            //发送请求
            Stream stream = myRequest.GetRequestStream();
            stream.Write(buf, 0, buf.Length);
            stream.Close();
            //获取接口返回值
            //通过Web访问对象获取响应内容
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            //通过响应内容流创建StreamReader对象，因为StreamReader更高级更快
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            //string returnXml = HttpUtility.UrlDecode(reader.ReadToEnd());//如果有编码问题就用这个方法
            string returnXml = reader.ReadToEnd();//利用StreamReader就可以从响应内容从头读到尾
            reader.Close();
            myResponse.Close();
            success = true;

            return returnXml;
        }
        catch (Exception ex)
        {

            return ex.Message;

        }
    }
    #endregion
    #region Delete请求
    public string Delete(out bool success, string data, string uri)
    {

        success = false;

        try
        {
            //先根据用户请求的uri构造请求地址
            string serviceUrl = string.Format("{0}/{1}", this.BaseUri, uri);
            //创建Web访问对象
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);
            foreach (var item in this.headers)
            {
                myRequest.Headers.Add(item.Key, item.Value);
            }
            //把用户传过来的数据转成“UTF-8”的字节流
            byte[] buf = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(data);
            myRequest.Method = "DELETE";
            myRequest.ContentLength = buf.Length;
            myRequest.ContentType = "application/json";
            myRequest.MaximumAutomaticRedirections = 1;
            myRequest.AllowAutoRedirect = true;
            //发送请求
            Stream stream = myRequest.GetRequestStream();
            stream.Write(buf, 0, buf.Length);
            stream.Close();
            //获取接口返回值
            //通过Web访问对象获取响应内容
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            //通过响应内容流创建StreamReader对象，因为StreamReader更高级更快
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            //string returnXml = HttpUtility.UrlDecode(reader.ReadToEnd());//如果有编码问题就用这个方法
            string returnXml = reader.ReadToEnd();//利用StreamReader就可以从响应内容从头读到尾
            reader.Close();
            myResponse.Close();
            success = true;
            return returnXml;
        }
        catch (Exception ex)
        {

            return ex.Message;

        }


    }
    #endregion
}
public class HttpRequestClient
{
    #region //字段
    private ArrayList bytesArray;
    private Encoding encoding = Encoding.UTF8;
    private string boundary = String.Empty;
    #endregion

    #region //构造方法
    public HttpRequestClient()
    {
        bytesArray = new ArrayList();
        string flag = DateTime.Now.Ticks.ToString("x");
        boundary = "---------------------------" + flag;
    }
    #endregion

    #region //方法
    /// <summary>
    /// 合并请求数据
    /// </summary>
    /// <returns></returns>
    private byte[] MergeContent()
    {
        int length = 0;
        int readLength = 0;
        string endBoundary = "--" + boundary + "--\r\n";
        byte[] endBoundaryBytes = encoding.GetBytes(endBoundary);

        bytesArray.Add(endBoundaryBytes);

        foreach (byte[] b in bytesArray)
        {
            length += b.Length;
        }

        byte[] bytes = new byte[length];

        foreach (byte[] b in bytesArray)
        {
            b.CopyTo(bytes, readLength);
            readLength += b.Length;
        }

        return bytes;
    }
    /// <summary>
    /// 上传
    /// </summary>
    /// <param name="requestUrl">请求url</param>
    /// <param name="responseText">响应</param>
    /// <returns></returns>
    public string Upload(out bool success, string requestUrl, out string responseText, out string errormessage, string token)
    {
        success = false;
        errormessage = String.Empty;
        byte[] responseBytes;
        try
        {
            WebClient webClient = new WebClient();
            webClient.Headers.Set("Authorization", token);
            webClient.Headers.Add("Content-Type", "multipart/form-data; boundary=" + boundary);
            byte[] bytes = MergeContent();
            responseBytes = webClient.UploadData(requestUrl, bytes);
            responseText = System.Text.Encoding.UTF8.GetString(responseBytes);
            success = true;
            return responseText;
        }
        catch (Exception ex)
        {
            errormessage = ex.Message;
            responseText = ex.StackTrace;
            return responseText;
        }
    }
    /// <summary>
    /// 上传
    /// </summary>
    /// <param name="requestUrl">请求url</param>
    /// <param name="responseText">响应</param>
    /// <returns></returns>
    public string Upload(String requestUrl, out String responseText, string token)
    {
        WebClient webClient = new WebClient();
        webClient.Headers.Set("Authorization", token);
        webClient.Headers.Add("Content-Type", "multipart/form-data; boundary=" + boundary);

        byte[] responseBytes;
        byte[] bytes = MergeContent();

        try
        {
            responseBytes = webClient.UploadData(requestUrl, bytes);
            responseText = System.Text.Encoding.UTF8.GetString(responseBytes);

        }
        catch (WebException ex)
        {
            Stream responseStream = ex.Response.GetResponseStream();
            responseBytes = new byte[ex.Response.ContentLength];
            responseStream.Read(responseBytes, 0, responseBytes.Length);
        }
        responseText = System.Text.Encoding.UTF8.GetString(responseBytes);
        return responseText;
    }




    /// <summary>
    /// 设置表单数据字段
    /// </summary>
    /// <param name="fieldName">字段名</param>
    /// <param name="fieldValue">字段值</param>
    /// <returns></returns>
    public void SetFieldValue(String fieldName, String fieldValue)
    {
        string httpRow = "--" + boundary + "\r\nContent-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}\r\n";
        string httpRowData = String.Format(httpRow, fieldName, fieldValue);

        bytesArray.Add(encoding.GetBytes(httpRowData));
    }

    /// <summary>
    /// 设置表单文件数据
    /// </summary>
    /// <param name="fieldName">字段名</param>
    /// <param name="filename">字段值</param>
    /// <param name="contentType">内容内型</param>
    /// <param name="fileBytes">文件字节流</param>
    /// <returns></returns>
    public void SetFieldValue(String fieldName, String filename, String contentType, Byte[] fileBytes)
    {
        string end = "\r\n";
        string httpRow = "--" + boundary + "\r\nContent-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
        string httpRowData = String.Format(httpRow, fieldName, filename, contentType);

        byte[] headerBytes = encoding.GetBytes(httpRowData);
        byte[] endBytes = encoding.GetBytes(end);
        byte[] fileDataBytes = new byte[headerBytes.Length + fileBytes.Length + endBytes.Length];

        headerBytes.CopyTo(fileDataBytes, 0);
        fileBytes.CopyTo(fileDataBytes, headerBytes.Length);
        endBytes.CopyTo(fileDataBytes, headerBytes.Length + fileBytes.Length);

        bytesArray.Add(fileDataBytes);
    }

    #endregion
}