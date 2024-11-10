using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BlazorRest;

public partial class Product
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public byte[]? Icon { get; set; }

    [JsonIgnore]
    public string? imageDataURL => Icon == null ? null : string.Format("data:image/jpg+xml;base64,{0}", Convert.ToBase64String(Icon));

    public virtual CrossProductUser? CrossProductUser { get; set; }
}
