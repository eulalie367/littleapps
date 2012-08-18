{{#page}}
	<article>
		<header>
			<h1>{{heading.title}}</h1>
			<p>{{heading.text}}</p>
		</header>
		{{#page.sections}}
			<section>
				<h2>{{title}}</h2>
				<p>{{text}}</p>
			</section>
		{{/page.sections}}
		<footer>
			<h3>{{footer.title}}</h3>
			<p>{{footer.text}}</p>
		</footer>
	</article>
	{{#aside}}<!-- This might be better as a partial -->
		<aside>
			<header>
				<h1>{{heading.title}}</h1>
				<p>{{heading.text}}</p>
			</header>
			{{#page.sections}}
				<section>
					<h2>{{title}}</h2>
					<p>{{text}}</p>
				</section>
			{{/page.sections}}
			<footer>
				<h3>{{footer.title}}</h3>
				<p>{{footer.text}}</p>
			</footer>
		</aside>
	{{/aside}}
{{/page}}
