<script>
import QuestionList from "../components/QuestionList/QuestionList.svelte";
import Banner from "../components/Banner/Banner.svelte";
import FaqSection from "../components/Section/FaqSection.svelte";
</script>

<svelte:head>
	<title>AvatarUI</title>
	<meta name="description" content="AvatarUI" />
</svelte:head>

<section class="dark:bg-[#121C24] dark:border-white ">
	<Banner/>
	<FaqSection/>
	<QuestionList/>
</section>

