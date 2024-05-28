﻿using AngleSharp.Html.Dom;

namespace Base.Tests.Utils;

public static class HttpClientExtensions
{
    public static Task<HttpResponseMessage> SendAsync(
        this HttpClient client,
        IHtmlFormElement form,
        IHtmlElement submitButton)
    {
        return client.SendAsync(form, submitButton, new Dictionary<string, string>());
    }

    public static Task<HttpResponseMessage> SendAsync(this HttpClient client, IHtmlFormElement form, IEnumerable<KeyValuePair<string, string>> formValues)
    {
        var submitElement = Assert.Single(form.QuerySelectorAll("[type=submit]"));
        var submitButton = Assert.IsAssignableFrom<IHtmlElement>(submitElement);

        return client.SendAsync(form, submitButton, formValues);
    }

    public static Task<HttpResponseMessage> SendAsync(
    this HttpClient client,
    IHtmlFormElement form, IHtmlElement submitButton, IEnumerable<KeyValuePair<string, string>> formValues)
{
    foreach (var (key, value) in formValues)
    {
        switch (form[key])
        {
            case IHtmlInputElement inputElement:
                inputElement.Value = value;
                if (inputElement.Type == "checkbox")
                {
                    inputElement.IsChecked = bool.Parse(value);
                }
                break;
            case IHtmlSelectElement selectElement:
                selectElement.Value = value;
                break;
            case IHtmlTextAreaElement textAreaElement:
                textAreaElement.Value = value;
                break;
            default:
                throw new InvalidOperationException($"Unsupported form element type for key: {key}");
        }
    }

    // if (formValues.Any(fv => fv.Key == "__RequestVerificationToken"))
    // {
    //     var tokenValue = formValues.First(fv => fv.Key == "__RequestVerificationToken").Value;
    //     var hiddenTokenInput = form.Owner.CreateElement("input") as IHtmlInputElement;
    //     hiddenTokenInput!.Name = "__RequestVerificationToken";
    //     hiddenTokenInput.Value = tokenValue;
    //     form.AppendChild(hiddenTokenInput);
    // }

    var submit = form.GetSubmission(submitButton);
    var target = (Uri)submit!.Target;
    if (submitButton.HasAttribute("formaction"))
    {
        var formaction = submitButton.GetAttribute("formaction");
        if (!string.IsNullOrEmpty(formaction))
        {
            target = new Uri(formaction, UriKind.Relative);
        }
    }

    var submission = new HttpRequestMessage(new HttpMethod(submit.Method.ToString()), target)
    {
        Content = new StreamContent(submit.Body)
    };

    foreach (var (key, value) in submit.Headers)
    {
        submission.Headers.TryAddWithoutValidation(key, value);
        submission.Content.Headers.TryAddWithoutValidation(key, value);
    }

    return client.SendAsync(submission);
}

}