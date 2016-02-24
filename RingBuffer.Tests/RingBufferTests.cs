using System;
using Xunit;

namespace RingBuffer.Tests
{
    public class RingBufferTests
    {
        [Fact]
        public void CorrectAddElementAndRemoveWhenNotExeeded()
        {
            var buffer = new RingBuffer<int>(3);
            buffer.Enqueue(10);
            buffer.Enqueue(11);

            Assert.Equal(10, buffer.Dequeue());
            Assert.Equal(11, buffer.Dequeue());
        }

        [Fact]
        public void OverIncrementStartPointerShouldOverrightFirstValue()
        {
            var buffer = new RingBuffer<int>(3);
            buffer.Enqueue(10);
            buffer.Enqueue(11);
            buffer.Enqueue(12);
            buffer.Enqueue(14);

            var element = buffer.Dequeue();

            Assert.Equal(element,12);
        }

        [Fact]
        public void GettingElementsFromEmptyCollectionShouldThrowsInvalidOperationException()
        {
            var buffer = new RingBuffer<int>(3);

            Assert.Throws<InvalidOperationException>(() => buffer.Dequeue());
        }


        [Fact]
        public void AddingMultipleElementsShouldNotRisedArgumentOutOfRangeException()
        {
            var buffer = new RingBuffer<int>(3);
            
            buffer.Enqueue(1);
            buffer.Enqueue(2);
            buffer.Enqueue(3);

            var ex = Record.Exception(() => buffer.Enqueue(4));

            Assert.Null(ex);
        }

        [Fact]
        public void PoliteReciveLastElementShouldNotRemoveItFromQueueAndMoveReadPointer()
        {
            var buffer = new RingBuffer<int>(3);
            
            buffer.Enqueue(1);
            buffer.Enqueue(2);

            var firstElement = buffer.LastElement;
            var secondElement = buffer.LastElement;

            Assert.Equal(firstElement, secondElement);
        }

        [Fact]
        public void LastElementExpressionBodyShouldThowExceptionWhenCollectionIsEmpty()
        {
            var buffer = new RingBuffer<int>(2);

            Assert.Throws<InvalidOperationException>(() => buffer.LastElement);
        }

        [Fact]
        public void WhenRingBufferHasBeenClearedIsEmptyFlagShouldBeSetToTrue()
        {
            var buffer = new RingBuffer<int>(2);
            buffer.Enqueue(2);
            buffer.Enqueue(1);

            buffer.Clear();

            Assert.Equal(buffer.IsEmpty, true);
        }
    }
}
