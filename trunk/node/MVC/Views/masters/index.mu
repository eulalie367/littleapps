<html>
	<head>
		<title>{{ Title }}</title>
	</head>
	<body>
		<h1>Master</h1>
		{{#Menu}}<!-- I would like to figure out recursion, but I haven't yet -->
		<menu>
			<ul>
				<li>
					<a href="{{path}}">{{name}}</a>
					<ul>
					  {{ #children }}
							<li>
								<a href="{{path}}">{{name}}</a>
								<ul>
								  {{ #children }}
										<li>
											<a href="{{path}}">{{name}}</a>
										</li>
								  {{ /children }}
								</ul>
							</li>
					  {{ /children }}
					</ul>
				</li>
			</ul>
		</menu>
		{{/Menu}}
		<article>
			<section>
				{{{ defaultHTML }}}
			</section>
		</article>
	</body>
</html>
