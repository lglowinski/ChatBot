<script>
  import LoginModal from "../Modal/LoginModal.svelte";
  let isModalOpen = false;

  function openModal() {
    isModalOpen = true;
  }

  function closeModal() {
    isModalOpen = false;
  }
</script>

<button
  class="relative inset-y-0 flex items-center bg-[#F5C754] text-gray-900 text-sm font-bold px-4
                rounded shadow-md transition duration-300 ease-in-out transform hover:scale-10"
  on:click={openModal}
>
  Login
</button>
<LoginModal isOpen={isModalOpen} on:close={closeModal} />
