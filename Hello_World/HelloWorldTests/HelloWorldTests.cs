using NUnit.Framework;
using Hello_World.Models;
using Hello_World.Controllers;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace HelloWorldTests
{
    public class Tests
    {
        private MessagesController _messagesController;
        private MessageContext _messageContext;

        [SetUp]
        public void Setup()
        {
            _messageContext = DbContextMocker.GetMessageContext("MessagesList");
            _messagesController = new MessagesController(_messageContext);
        }

        [Test]
        public void TestGetMessages_Pass()
        {
            var messages = (List<MessageDto>)_messagesController.GetMessages().Result.Value;

            Assert.IsTrue(messages.Count == 1);
        }

        [Test]
        public void TestGetMessage_Pass()
        {
            var message = _messagesController.GetMessage(1).Result.Value;

            string value = "Hello World!";
            Assert.AreEqual(value, message.Value);
        }

        [Test]
        public void TestGetMessage_Fail()
        {
            var notFound = (NotFoundResult)_messagesController.GetMessage(10).Result.Result;

            int statusCode = 404;
            Assert.AreEqual(statusCode, notFound.StatusCode);
        }

        [Test]
        public void TestPutMessage_Pass()
        {
            Message message = new Message { Id = 1, Value = "Hello World!!" };
            var noContent = (NoContentResult)_messagesController.PutMessage(1, message).Result;

            int statusCode = 204;
            Assert.AreEqual(statusCode, noContent.StatusCode);
        }

        [Test]
        public void TestPostMessage_Pass()
        {
            MessageDto messageDto = new MessageDto { Value = "Goodbye!" };
            var createdAtAction = (CreatedAtActionResult)_messagesController.PostMessage(messageDto).Result.Result;

            Assert.AreEqual(messageDto, createdAtAction.Value);
        }

        [Test]
        public void TestDeleteMessage_Pass()
        {
            MessageDto messageDto = new MessageDto { Value = "Goodbye!" };
            var action = _messagesController.PostMessage(messageDto);
            var messages = (List<MessageDto>)_messagesController.GetMessages().Result.Value;

            Assert.IsTrue(messages.Count == 2);

            var delete = _messagesController.DeleteMessage(2).Result;
            messages = (List<MessageDto>)_messagesController.GetMessages().Result.Value;

            Assert.IsTrue(messages.Count == 1);
        }
    }
}