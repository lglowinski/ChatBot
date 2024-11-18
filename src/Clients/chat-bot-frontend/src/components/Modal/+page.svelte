<script lang="ts">
  import LoginModal from "./LoginModal.svelte";

  let isModalOpen = false;

  function openModal() {
    isModalOpen = true;
  }

  function closeModal() {
    isModalOpen = false;
  }
</script>

<main>
  <LoginModal isOpen={isModalOpen} on:close={closeModal} />
</main>
