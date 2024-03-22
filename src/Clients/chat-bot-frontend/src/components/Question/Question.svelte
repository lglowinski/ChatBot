<script lang="ts">
    export let question: { title: string; answer:string; upvotes:number; downvotes: number };

    let upvotes = question.upvotes;
    let downvotes = question.downvotes;

    let alreadyUpvoted = false;
    let alreadyDownvoted = false;

    let helpful = false;
    $: handleChange(helpful);


    function handleUpvote() {
        if(!alreadyUpvoted){
            upvotes += 1;
            alreadyUpvoted = true;
        }

        if(alreadyDownvoted){
            downvotes -= 1;
            alreadyDownvoted = false;
        }

    }

    function handleDownvote() {
        if(!alreadyDownvoted){
            downvotes += 1;
            alreadyDownvoted = true;
        }

        if(alreadyUpvoted){
            upvotes -= 1;
            alreadyUpvoted = false;
        }
    }

    function handleChange(helpful : boolean){
        console.log(helpful)
    }

</script>


<main>
    <slot/>
</main>

<aside>
    <article class="shadow rounded-lg p-4 max-w-2xl mx-auto">
        <a class="flex items-center  w-1/2 py-2
        transition-colors duration-200 rounded-lg gap-x-2 sm:w-auto d
        ark:hover:bg-gray-800 mb-8" href="/">
            <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                <path d="M12 18L6 12L12 6" stroke="#B6CCF7" stroke-width="2"/>
                <path d="M18 18L12 12L18 6" stroke="#B6CCF7" stroke-width="2"/>
            </svg>
        </a>
        <div class="flex justify-between items-center mb-4">
            <h2 class="text-xl font-bold">{question.title}</h2>
        </div>
        <p class="text-gray-600 dark:text-gray-200 mb-4">
            {question.answer}
        </p>
        <div class="flex items-center mb-4 space-x-4 sm:w-auto">
            <button class="flex items-center text-gray-500 dark:text-gray-400 mr-2" on:click={handleUpvote}>
                <svg width="22" height="20" viewBox="0 0 22 20" fill="none" xmlns="http://www.w3.org/2000/svg" class="mr-1">
                    <path fill-rule="evenodd" clip-rule="evenodd"
                          d="M20.9375 6.51125C20.5103 6.02713 19.8957 5.74987 19.25 5.75H14V4.25C14 2.17893 12.3211 0.5 10.25 0.5C9.96581
                          0.499797 9.70592 0.660232 9.57875 0.914375L6.03687 8H2C1.17157 8 0.5 8.67157 0.5 9.5V17.75C0.5
                           18.5784 1.17157 19.25 2 19.25H18.125C19.2592 19.2504 20.2164 18.4065 20.3581 17.2812L21.4831
                            8.28125C21.5638 7.64028 21.365 6.99556 20.9375 6.51125ZM2 9.5H5.75V17.75H2V9.5ZM19.9944
                             8.09375L18.8694 17.0938C18.8221 17.4688 18.5031 17.7501 18.125 17.75H7.25V8.92719L10.6916
                             2.04312C11.7434 2.25363 12.5003 3.17735 12.5 4.25V6.5C12.5 6.91421 12.8358 7.25 13.25
                              7.25H19.25C19.4653 7.24993 19.6702 7.34238 19.8127 7.50383C19.9551 7.66527 20.0213 7.88014 19.9944 8.09375Z" fill="#8A9EBF"/>
                </svg>
                {upvotes}
            </button>
            <button class="flex items-center text-gray-500 dark:text-gray-400" on:click={handleDownvote}>
                <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg" class="mr-1">
                    <g clip-path="url(#clip0_38_432)">
                        <path fill-rule="evenodd" clip-rule="evenodd" d="M22.4831 14.7188L21.3581 5.71875C21.2164 4.59346 20.2592 3.74961 19.125 3.75H3C2.17157 3.75 1.5 4.42157 1.5 5.25V13.5C1.5 14.3284 2.17157 15 3 15H7.03687L10.5787 22.0856C10.7059 22.3398 10.9658 22.5002 11.25 22.5C13.3211 22.5 15 20.8211 15 18.75V17.25H20.25C20.8959 17.2502 21.5107 16.9729 21.938 16.4885C22.3653 16.0042 22.5639 15.3596 22.4831 14.7188ZM6.75 13.5H3V5.25H6.75V13.5ZM20.8125 15.4959C20.6711 15.6586 20.4656 15.7514 20.25 15.75H14.25C13.8358 15.75 13.5 16.0858 13.5 16.5V18.75C13.5003 19.8227 12.7434 20.7464 11.6916 20.9569L8.25 14.0728V5.25H19.125C19.5031 5.24987 19.8221 5.53115 19.8694 5.90625L20.9944 14.9062C21.0228 15.1199 20.9564 15.3354 20.8125 15.4959Z" fill="#8A9EBF"/>
                    </g>
                    <defs>
                        <clipPath id="clip0_38_432">
                            <rect width="24" height="24" fill="white"/>
                        </clipPath>
                    </defs>
                </svg>
                {downvotes}
            </button>
        </div>
        <div class="border pt-3.5 pb-3.5 justify-center border-[#33405C] rounded-xl">
            <label class="max-w-screen-xl flex flex-wrap items-center justify-between mx-auto p-2 w-full md:grid-cols-2">
                <span class=" text-sm font-medium text-gray-900 dark:text-gray-300 flex md:flex md:w-auto md:order-0">Was this answer helpful?</span>
                <input type="checkbox" bind:checked={helpful} class="sr-only peer">
                <div class="relative w-11 h-6 bg-gray-200 peer-focus:outline-none peer-focus:ring-4
                peer-focus:ring-gray-300 dark:peer-focus:ring-gray-800 rounded-full peer dark:bg-[#787880] dark:opacity-16
                peer-checked:after:translate-x-full rtl:peer-checked:after:-translate-x-full
                peer-checked:after:border-white after:content-[''] after:absolute after:top-[2px] after:start-[2px]
                after:bg-[#121C24] after:rounded-full after:h-5 after:w-5 flex md:order-2
                after:transition-all dark:border-gray-600 peer-checked:bg-gray-600"></div>
            </label>
        </div>
    </article>
</aside>