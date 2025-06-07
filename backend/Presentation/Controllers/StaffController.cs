using System;

public class StaffControllers
{
	public StaffControllers()
	{
        bloodBlocks = new List<BloodBlock>();
    }

    public bool CreateBlock(BloodBlock newBlock)
    {
        try
        {
            // Validate the new block
            if (newBlock == null || string.IsNullOrEmpty(newBlock.BlockId))
            {
                return false;
            }

            // Check if block already exists
            if (bloodBlocks.Any(b => b.BlockId == newBlock.BlockId))
            {
                return false;
            }

            bloodBlocks.Add(newBlock);
            return true;
        }
        catch (Exception ex)
        {
            // Log the error
            Console.WriteLine($"Error creating block: {ex.Message}");
            return false;
        }
    }

   
    public BloodBlock GetBlockById(string blockId)
    {
        return bloodBlocks.FirstOrDefault(b => b.BlockId == blockId);
    }

    public List<BloodBlock> GetAllBlocks()
    {
        return new List<BloodBlock>(bloodBlocks);
    }

    public bool UpdateBlock(BloodBlock updatedBlock)
    {
        try
        {
            var existingBlock = bloodBlocks.FirstOrDefault(b => b.BlockId == updatedBlock.BlockId);
            if (existingBlock == null)
            {
                return false;
            }
            existingBlock.BloodType = updatedBlock.BloodType;
            existingBlock.Quantity = updatedBlock.Quantity;
            existingBlock.ExpirationDate = updatedBlock.ExpirationDate;
            existingBlock.Status = updatedBlock.Status;

            return true;
        }
        catch (Exception ex)
        {
            // Log the error
            Console.WriteLine($"Error updating block: {ex.Message}");
            return false;
        }
    }

  
    public bool DeleteBlock(string blockId)
    {
        try
        {
            var blockToRemove = bloodBlocks.FirstOrDefault(b => b.BlockId == blockId);
            if (blockToRemove == null)
            {
                return false;
            }

            bloodBlocks.Remove(blockToRemove);
            return true;
        }
        catch (Exception ex)
        {
            // Log the error
            Console.WriteLine($"Error deleting block: {ex.Message}");
            return false;
        }
    }
}

// Sample BloodBlock class that would be managed by StaffControllers
public class BloodBlock
{
    public string BlockId { get; set; }
    public string BloodType { get; set; } // A, B, AB, O, etc.
    public double Quantity { get; set; } // in milliliters
    public DateTime CollectionDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string Status { get; set; } // Available, Reserved, Expired, etc.
    public string DonorId { get; set; } // Optional: link to donor if available
}
}
