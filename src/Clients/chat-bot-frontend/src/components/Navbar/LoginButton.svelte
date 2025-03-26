<script>
  import LoginModal from "../Modal/LoginModal.svelte";
  import { auth } from "../../store/auth.store";
  let isModalOpen = false;

  $: isLogged = $auth.isAuthenticated;

  function openModal() {
    isModalOpen = true;
  }

  function closeModal() {
    isModalOpen = false;
  }

  console.log(isLogged)

  function logout(){
    auth.logout();
  }
</script>

{#if !isLogged}
<button
  class="relative inset-y-0 flex items-center bg-[#F5C754] text-gray-900 text-sm font-bold px-4
                rounded shadow-md transition duration-300 ease-in-out transform hover:scale-10"
  on:click={openModal}
>
  Login
</button>
{:else}
<button
        class="relative inset-y-0 flex items-center bg-[#F5C754] text-gray-900 text-sm font-bold px-4
                rounded shadow-md transition duration-300 ease-in-out transform hover:scale-10"
        on:click={logout}
>
  Logout
</button>
{/if}
<LoginModal isOpen={isModalOpen} on:close={closeModal} />

