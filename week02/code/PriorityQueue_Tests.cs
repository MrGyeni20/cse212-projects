using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Add three items with different priorities (1, 5, 10).
    // Dequeue should return the highest priority item first.
    // Expected Result: "High" is returned first
    // Defect(s) Found: Loop was using _queue.Count - 1 so last item was never checked.
    // Also item was never removed from queue after dequeue.
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue1();
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("Medium", 5);
        priorityQueue.Enqueue("High", 10);

        var result = priorityQueue.Dequeue();
        Assert.AreEqual("High", result);
    }

    [TestMethod]
    // Scenario: Add three items where two share the highest priority.
    // The one added first (closest to front) should be dequeued first (FIFO).
    // Expected Result: "First" is returned before "Second"
    // Defect(s) Found: Using >= instead of > caused the last duplicate
    // priority item to be returned instead of the first one, breaking FIFO.
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue1();
        priorityQueue.Enqueue("First", 5);
        priorityQueue.Enqueue("Second", 5);
        priorityQueue.Enqueue("Low", 1);

        var result = priorityQueue.Dequeue();
        Assert.AreEqual("First", result);
    }

    [TestMethod]
    // Scenario: Try to dequeue from an empty queue.
    // Expected Result: InvalidOperationException thrown with message "The queue is empty."
    // Defect(s) Found: No defect here, exception was implemented correctly.
    public void TestPriorityQueue_3()
    {
        var priorityQueue = new PriorityQueue1();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail(
                string.Format("Unexpected exception of type {0} caught: {1}",
                               e.GetType(), e.Message)
            );
        }
    }

    [TestMethod]
    // Scenario: Add multiple items and dequeue all of them.
    // Each dequeue should return the current highest priority item.
    // Expected Result: Items returned in order: "High", "Medium", "Low"
    // Defect(s) Found: Item was never removed after dequeue so the same
    // item kept being returned every time.
    public void TestPriorityQueue_4()
    {
        var priorityQueue = new PriorityQueue1();
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("High", 10);
        priorityQueue.Enqueue("Medium", 5);

        Assert.AreEqual("High", priorityQueue.Dequeue());
        Assert.AreEqual("Medium", priorityQueue.Dequeue());
        Assert.AreEqual("Low", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Add one item and dequeue it.
    // Expected Result: That one item "Only" is returned.
    // Defect(s) Found: No defect for single item scenario.
    public void TestPriorityQueue_5()
    {
        var priorityQueue = new PriorityQueue1();
        priorityQueue.Enqueue("Only", 3);

        var result = priorityQueue.Dequeue();
        Assert.AreEqual("Only", result);
    }
}