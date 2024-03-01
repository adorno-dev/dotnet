using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BookStoreAPI.Models;

public sealed class Book
{
	[BsonId]
	[BsonRepresentation(BsonType.ObjectId)]
	public string? Id { get; set; }

	[BsonElement("Name")]
	[JsonPropertyName("Name")]
	public string? BookName { get; set; }
	public decimal Price { get; set; }
	public string? Category { get; set; }
	public string? Author { get; set; }
}
