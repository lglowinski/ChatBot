<script>
	import '../app.css';
	import Header from '../components/Navbar/Navbar.svelte';
</script>

<div class="app bg-white dark:bg-[#121C24] text-black
dark:text-white">
	<Header />
	<main>
		<slot></slot>
	</main>
	<footer class="mb-1">
		<p class="text-[#8A9EBF]">Copyright &copy; 2024. Created by <a href="https://lglowinski.pl" target="_blank">Lukasz Glowinski Software</a> - All rights reserved</p>
	</footer>
</div>

<style>
	.app {
		display: flex;
		flex-direction: column;
		min-height: 100vh;
	}

	main {
		flex: 1;
		display: flex;
		flex-direction: column;
		padding: 1rem;
		width: 100%;
		margin: 0 auto;
		box-sizing: border-box;
	}

	footer {
		display: flex;
		flex-direction: column;
		justify-content: center;
		align-items: center;
		padding: 15px;
	}

	footer a {
		font-weight: bold;
	}

	@media (min-width: 480px) {
		footer {
			padding: 12px 0;
		}
	}
</style>
