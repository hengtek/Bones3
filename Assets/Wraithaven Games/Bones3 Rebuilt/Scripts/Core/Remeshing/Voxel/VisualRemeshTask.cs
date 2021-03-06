namespace WraithavenGames.Bones3
{
    /// <summary>
    /// Generates a visual chunk mesh for a single submesh layer.
    /// </summary>
    internal class VisualRemeshTask : VoxelChunkMesher
    {
        /// <summary>
        /// Gets the material ID this remesh task is targeting.
        /// </summary>
        /// <value>The material ID.</value>
        public int MaterialID { get; }

        /// <inheritdoc cref="VoxelChunkMesher"/>
        internal VisualRemeshTask(ChunkProperties chunkProperties, int materialID, GreedyMesher mesher) :
            base(chunkProperties, mesher) => MaterialID = materialID;

        /// <inheritdoc cref="VoxelChunkMesher"/>
        protected override bool CanPlaceQuad(ChunkProperties chunkProperties, BlockPosition pos, int side)
        {
            var block = chunkProperties.GetBlock(pos);
            if (block.GetMaterialID(side) != MaterialID)
                return false;

            if (!block.IsVisible)
                return false;

            var nextBlock = pos.ShiftAlongDirection(side);
            var next = chunkProperties.GetBlock(nextBlock);

            if (!next.IsVisible)
                return true;

            if (!next.IsTransparent)
                return false;

            return block != next;
        }
    }
}
